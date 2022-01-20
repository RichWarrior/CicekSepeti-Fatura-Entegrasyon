using Core.Enums;
using Core.Extensions;
using Core.Interfaces;
using Core.Utilities;
using Core.Utilities.Result;
using EArchiveClient.Commands;
using EArchiveClient.DTO.Request;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Service;
using Service.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EArchive.Consumer
{
    public class DownloadInvoiceConsumer : BaseConsumer
    {
        DownloadPDFCommand downloadPdfCommand;

        string currentPath = Path.Combine(Directory.GetCurrentDirectory(), "invoices");

        string companyName = string.Empty;

        IMinioService _minioService;

        public DownloadInvoiceConsumer()
        {
            ConnectionInfo connectionInfo = ConnectionInfo.Instance;
            companyName = connectionInfo.CompanyName;

            downloadPdfCommand = new DownloadPDFCommand();

            _minioService = new MinioService();

            try
            {
                using (IUnitOfWork uow = new UnitOfWork())
                {
                    var connection = uow.RabbitMQ.GetRabbitMQConnection();
                    var channel = connection.CreateModel();
                    var queue = $"{companyName}_{EnumQueue.PDF.ToString()}";
                    channel.QueueDeclare(queue: queue,
                                      durable: false,
                                      exclusive: false,
                                      autoDelete: false,
                                      arguments: null);
                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (ch, ea) =>
                    {
                        Console.WriteLine("PDF İndirme İsteği Geldi");
                        var body = ea.Body.ToArray();
                        Download(body);
                    };
                    channel.BasicConsume(queue, true, consumer);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        private async void Download(byte[] body)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<List<int>>(Encoding.UTF8.GetString(body));
                if (data.Any())
                {
                    IUnitOfWork uow = new UnitOfWork();
                    var invoicesResult = uow.Invoice.List(data);
                    if (invoicesResult.Success)
                    {
                        var invoices = invoicesResult.Data;
                        invoices.RemoveAll(f => f.InvoiceStatusId != (int)EnumInvoiceStatus.CreatedDraft && f.InvoiceStatusId != (int)EnumInvoiceStatus.NotDownloadedPDF && f.InvoiceStatusId != (int)EnumInvoiceStatus.DownloadedPDF && f.InvoiceStatusId != (int)EnumInvoiceStatus.DownloadingPDF);
                        if (invoices.Any())
                        {
                            Console.WriteLine($"{invoices.Count} adet fatura var");
                            List<Core.Entities.Invoice> downloadedInvoices = new List<Core.Entities.Invoice>();
                            List<Core.Entities.Invoice> notDownloadedInvoices = new List<Core.Entities.Invoice>();
                            foreach (var invoice in invoices)
                            {
                                invoice.InvoiceStatusId = (int)EnumInvoiceStatus.DownloadingPDF;
                                uow.Invoice.Update(invoice);
                            }

                            if (uow.SaveChanges() && uow.Commit())
                            {

                                if (!Directory.Exists(currentPath))
                                    Directory.CreateDirectory(currentPath);

                                var files = Directory.GetFiles(currentPath);
                                foreach (var item in files)
                                {
                                    FileInfo f = new FileInfo(item);
                                    if (f.Attributes == FileAttributes.Directory)
                                        Directory.Delete(item, true);
                                    else
                                        File.Delete(item);
                                }

                                IDataResult<Core.Entities.File> file = null;
                                foreach (var invoice in invoices)
                                {
                                    try
                                    {
                                        uow.Dispose();
                                        uow = new UnitOfWork();
                                        if (file == null)
                                            file = uow.File.Find(invoice.FileId);
                                        if (file.Success)
                                        {
                                            file.Data.HasFile = false;
                                            uow.File.Update(file.Data);
                                            uow.SaveChanges();
                                            uow.Commit();
                                        }
                                        var token = tokenCommand.Execute(tokenRequest);
                                        invoice.Message = token.Message;
                                        if (token.StatusCode != HttpStatusCode.OK || String.IsNullOrEmpty(token.Data.Token))
                                        {
                                            invoice.InvoiceStatusId = (int)EnumInvoiceStatus.NotDownloadedPDF;                                            
                                            uow.Invoice.Update(invoice);
                                            uow.SaveChanges();
                                            uow.Commit();
                                            continue;
                                        }

                                        var response = downloadPdfCommand.Execute(new DownloadPDFRequest
                                        {
                                            Token = token.Data.Token,
                                            InvoiceUniqueKey = invoice.InvoiceUniqueKey
                                        });
                                        if (response.Data.Data.Any())
                                        {
                                            string zipFilename = Path.Combine(currentPath, string.Format("{0}.zip", invoice.SubOrderId));
                                            File.WriteAllBytes(zipFilename, response.Data.Data);
                                            var extractDirectory = Path.Combine(currentPath, invoice.SubOrderId);
                                            System.IO.Compression.ZipFile.ExtractToDirectory(zipFilename, extractDirectory);
                                            File.Delete(zipFilename);

                                            var htmlFile = Directory.GetFiles(extractDirectory).ToList().FirstOrDefault(f => Path.GetExtension(f) == ".html");

                                            string pdfPath = Path.Combine(currentPath, string.Format("{0}_{1}.pdf", invoice.SubOrderId, invoice.CustomerName.NormalizePDFName()));

                                            if (string.IsNullOrEmpty(htmlFile))
                                            {
                                                notDownloadedInvoices.Add(invoice);
                                                invoice.InvoiceStatusId = (int)EnumInvoiceStatus.NotDownloadedPDF;
                                                uow.Invoice.Update(invoice);
                                                uow.SaveChanges();
                                                uow.Commit();
                                            }
                                            else
                                            {

                                                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                                                {
                                                    //var argss = @"C:\Program Files\wkhtmltopdf\bin\wkhtmltopdf.exe "+htmlFile+" "+pdfPath;
                                                    //var processInfo = new ProcessStartInfo();
                                                    //processInfo.UseShellExecute = false;
                                                    //processInfo.RedirectStandardOutput = true;
                                                    //processInfo.FileName = @"C:\Program Files\wkhtmltopdf\bin\wkhtmltopdf.exe";
                                                    //processInfo.Arguments = argss;

                                                    //var process = Process.Start(processInfo);
                                                    //var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                                                    //var cancellationToken = cancellationTokenSource.Token;
                                                    //while (true && !cancellationTokenSource.Token.IsCancellationRequested)
                                                    //{
                                                    //    if (process.StandardOutput.EndOfStream)
                                                    //        break;
                                                    //    if (process.StandardOutput.ReadLine().Contains("Done"))
                                                    //        break;
                                                    //}
                                                }
                                                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                                                {
                                                    var command = "sh";
                                                    var argss = $"{Path.Combine(Directory.GetCurrentDirectory(), "wkhtmltopdf.sh")} {htmlFile} {pdfPath}";
                                                    var processInfo = new ProcessStartInfo();
                                                    processInfo.UseShellExecute = false;
                                                    processInfo.RedirectStandardOutput = true;
                                                    processInfo.FileName = command;
                                                    processInfo.Arguments = argss;

                                                    var process = Process.Start(processInfo);
                                                    var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                                                    var cancellationToken = cancellationTokenSource.Token;
                                                    while (true && !cancellationTokenSource.Token.IsCancellationRequested)
                                                    {
                                                        if (process.StandardOutput.EndOfStream)
                                                            break;
                                                        if (process.StandardOutput.ReadLine().Contains("Done"))
                                                            break;
                                                    }
                                                }

                                                Directory.Delete(extractDirectory, true);

                                                downloadedInvoices.Add(invoice);
                                                invoice.InvoiceStatusId = (int)EnumInvoiceStatus.DownloadedPDF;
                                                uow.Invoice.Update(invoice);
                                                uow.SaveChanges();
                                                uow.Commit();

                                            }
                                        }
                                        else
                                        {
                                            notDownloadedInvoices.Add(invoice);
                                            invoice.InvoiceStatusId = (int)EnumInvoiceStatus.NotDownloadedPDF;
                                            uow.Invoice.Update(invoice);
                                        }

                                        uow.SaveChanges();
                                        uow.Commit();
                                    }
                                    catch (Exception ex)
                                    {
                                        invoice.InvoiceStatusId = (int)EnumInvoiceStatus.NotDownloadedPDF;
                                        uow.Invoice.Update(invoice);
                                        uow.SaveChanges();
                                        uow.Commit();
                                        Console.WriteLine(ex.Message);
                                    }
                                }
                                //InfoMail = "ofaruksahin@outlook.com.tr";
                                if (file.Success)
                                {
                                    var fileId = file.Data.Id;
                                    var newZipFile = Path.Combine(Directory.GetCurrentDirectory(), $"{fileId}.zip");
                                    if (File.Exists(newZipFile))
                                        File.Delete(newZipFile);
                                    if (Directory.GetFiles(currentPath).Any())
                                    {
                                        System.IO.Compression.ZipFile.CreateFromDirectory(currentPath, newZipFile);

                                        file.Data.HasFile = await _minioService.Upload($"{fileId}.zip", newZipFile, "application/zip");

                                        MailHelper.Send(new MailItem()
                                        {
                                            SmtpHost = smtpHost,
                                            SmtpPort = smtpPort,
                                            SmtpSender = smtpSender,
                                            SmtpPassword = smtpPassword,
                                            InfoMail = InfoMail,
                                            Subject = "FATURA ÇIKTISI",
                                            Message = file.Data.HasFile ?  "PDF Dosyasınız Arayüzden İndirilebilir!" : "Üzgünüz, PDF Çıktısını Oluşturamadık!",
                                        });

                                        uow.File.Update(file.Data);
                                        uow.SaveChanges();
                                        uow.Commit();

                                        File.Delete(newZipFile);
                                    }
                                    else
                                    {
                                        MailHelper.Send(new MailItem()
                                        {
                                            SmtpHost = smtpHost,
                                            SmtpPort = smtpPort,
                                            SmtpSender = smtpSender,
                                            SmtpPassword = smtpPassword,
                                            InfoMail = InfoMail,
                                            Subject = "FATURA ÇIKTISI",
                                            Message = "Üzgünüz, PDF Çıktısını Oluşturamadık!",
                                        });
                                    }
                                }
                                Directory.Delete(currentPath, true);
                            }
                        }
                        else
                        {
                            Console.WriteLine("İndirilecek Fatura bulunamadı");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

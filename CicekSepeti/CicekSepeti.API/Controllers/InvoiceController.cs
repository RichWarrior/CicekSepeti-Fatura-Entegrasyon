using AutoMapper;
using Core.Dtos.Request.Invoice;
using Core.Dtos.Response.Invoice;
using Core.Enums;
using Core.Interfaces;
using Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;

namespace CicekSepeti.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class InvoiceController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        readonly IStringLocalizer<InvoiceResource> l;

        /// <summary>
        /// 
        /// </summary>
        readonly IStringLocalizer<FileResource> f;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_uow"></param>
        /// <param name="_mapper"></param>
        /// <param name="_localizer"></param>
        /// <param name="_l"></param>
        /// <param name="_f"></param>
        public InvoiceController(
            IUnitOfWork _uow,
            IMapper _mapper,
            IStringLocalizer<BaseResource> _localizer,
            IStringLocalizer<InvoiceResource> _l,
            IStringLocalizer<FileResource> _f
            )
            : base(_uow, _mapper, _localizer)
        {
            l = _l;
            f = _f;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("list/{id}")]
        public IActionResult List(int id)
        {
            ListInvoiceResponse response = new ListInvoiceResponse();
            var file = uow.File.Find(id);

            if (!file.Success)
                return NotFound(response, f[file.Message]);

            var invoices = uow.Invoice.List(id);

            if (!invoices.Success)
                return NotFound(response);

            response.Invoices = invoices.Data.Select(f => mapper.Map<ListInvoiceResponse.Invoice>(f)).ToList();

            return Ok(response);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("processInvoice")]
        public IActionResult ProcessInvoce([FromBody] ProcessInvoiceRequestDTO dto)
        {
            try
            {
                EnumInvoiceStatus invoiceStatus = (EnumInvoiceStatus)Enum.Parse(typeof(EnumInvoiceStatus), dto.InvoiceStatusId.ToString());
                using (var connection = uow.RabbitMQ.GetRabbitMQConnection())
                {
                    string message = JsonConvert.SerializeObject(dto.Invoices);
                    var body = Encoding.UTF8.GetBytes(message);
                    var companyName = ConnectionInfo.Instance.CompanyName;
                    string queue = string.Empty;
                    using (var channel = connection.CreateModel())
                    {
                        switch (invoiceStatus)
                        {
                            case EnumInvoiceStatus.WaitingDraft:
                            case EnumInvoiceStatus.NotCreatedDraft:
                                queue = $"{companyName}_{EnumQueue.Invoice.ToString()}";
                                channel.QueueDeclare(queue: queue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                                channel.BasicPublish(exchange: "",
                                    routingKey: queue,
                                    mandatory: false,
                                    basicProperties: null,
                                    body: body);
                                break;
                            case EnumInvoiceStatus.CreatedDraft:
                            case EnumInvoiceStatus.DownloadedPDF:
                            case EnumInvoiceStatus.NotDownloadedPDF:
                                queue = $"{companyName}_{ EnumQueue.PDF.ToString()}";
                                channel.QueueDeclare(queue: queue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                                channel.BasicPublish(exchange: "",
                                    routingKey: queue,
                                    mandatory: false,
                                    basicProperties: null,
                                    body: body);
                                break;
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Ok(new { });
        }
    }
}

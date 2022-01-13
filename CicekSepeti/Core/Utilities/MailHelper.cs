using MailKit.Net.Smtp;
using MimeKit;
using System;

namespace Core.Utilities
{
    public static class MailHelper
    {
        public static bool Send(MailItem mailItem)
        {
            bool rtn = false;

            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(mailItem.SmtpSender, mailItem.SmtpSender));
                message.To.Add(new MailboxAddress(mailItem.InfoMail, mailItem.InfoMail));
                message.Subject = mailItem.Subject;
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = mailItem.Message;

                if (!string.IsNullOrEmpty(mailItem.AttachmentFile))
                {
                    bodyBuilder.Attachments.Add(mailItem.AttachmentFile);
                }

                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect(mailItem.SmtpHost, mailItem.SmtpPort);

                    client.Authenticate(mailItem.SmtpSender, mailItem.SmtpPassword);

                    client.Send(message);

                    client.Disconnect(true);
                }
                rtn = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return rtn;
        }
    }

    public class MailItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string SmtpHost { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SmtpPort { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SmtpSender { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SmtpPassword { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string InfoMail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AttachmentFile { get; set; }
    }

}

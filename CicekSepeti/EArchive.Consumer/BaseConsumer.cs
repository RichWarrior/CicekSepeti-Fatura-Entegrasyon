using Core.Interfaces;
using Core.Utilities;
using EArchiveClient.Commands;
using EArchiveClient.DTO.Request;
using Service;
using System;
using System.Linq;

namespace EArchive.Consumer
{
    public class BaseConsumer
    {

        string eArchiveUserName = string.Empty;
        string eArchivePassword = string.Empty;

        public string smtpHost = string.Empty;
        public string smtpSender = string.Empty;
        public string smtpPassword = string.Empty;
        public int smtpPort = 0;
        public string InfoMail = string.Empty;
        public string PDFPath = string.Empty;

        public GetTokenCommand tokenCommand;
        public GetTokenRequest tokenRequest;

        public BaseConsumer()
        {
            GetParameters();

            tokenCommand = new GetTokenCommand();
            tokenRequest = new GetTokenRequest()
            {
                UserId = eArchiveUserName,
                Password = eArchivePassword
            };
        }

        private void GetParameters()
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                var parameters = uow.Parameter.List().Data;

                var userNameParameter = parameters.FirstOrDefault(f => f.Name == ParameterConstants.EArchiveUserName);
                if (userNameParameter == null)
                    throw new ArgumentNullException(nameof(eArchiveUserName));

                eArchiveUserName = userNameParameter.Value;

                var passwordParameter = parameters.FirstOrDefault(f => f.Name == ParameterConstants.EArchivePassword);

                if (passwordParameter == null)
                    throw new ArgumentNullException(nameof(eArchivePassword));

                eArchivePassword = passwordParameter.Value;

                var smtpHostParameter = parameters.FirstOrDefault(f => f.Name == ParameterConstants.SmtpHost);
                if (smtpHostParameter == null)
                    throw new ArgumentNullException(nameof(smtpHost));

                smtpHost = smtpHostParameter.Value;

                var smtpSenderParameter = parameters.FirstOrDefault(f => f.Name == ParameterConstants.SmtpSender);
                if (smtpSenderParameter == null)
                    throw new ArgumentNullException(nameof(smtpSender));

                smtpSender = smtpSenderParameter.Value;

                var smtpPasswordParameter = parameters.FirstOrDefault(f => f.Name == ParameterConstants.SmtpPassword);

                if (smtpPasswordParameter == null)
                    throw new ArgumentNullException(nameof(smtpPassword));

                smtpPassword = smtpPasswordParameter.Value;

                var smtpPortParameter = parameters.FirstOrDefault(f => f.Name == ParameterConstants.SmtpPort);

                if (smtpPortParameter == null)
                    throw new ArgumentNullException(nameof(smtpPort));

                int.TryParse(smtpPortParameter.Value, out smtpPort);

                var infoMailParameter = parameters.FirstOrDefault(f=>f.Name == ParameterConstants.InfoMail);

                if (infoMailParameter == null)
                    throw new ArgumentNullException(nameof(infoMailParameter));

                InfoMail = infoMailParameter.Value;

                //var pdfPathParameter = parameters.FirstOrDefault(f => f.Name == ParameterConstants.PDFPath);
                //if (pdfPathParameter == null)
                //    throw new ArgumentNullException(nameof(pdfPathParameter));

                //PDFPath = pdfPathParameter.Value;

                Console.WriteLine("Parametreler Okundu.\nUygulama Başlatıldı!");
            }
        }
    }
}

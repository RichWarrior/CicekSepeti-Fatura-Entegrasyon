using Core.Utilities;
using EArchiveClient.DTO.Request;
using EArchiveClient.DTO.Response;
using RestSharp;
using System;

namespace EArchiveClient.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DownloadPDFCommand
    {
        /// <summary>
        /// 
        /// </summary>
        public BaseResponse<DownloadPDFResponse> Execute(DownloadPDFRequest dto)
        {
            BaseResponse<DownloadPDFResponse> baseResponse = new BaseResponse<DownloadPDFResponse>();
            try
            {
                ConnectionInfo connectionInfo = ConnectionInfo.Instance;
                var client = new RestClient(connectionInfo.EArchiveURL + "earsiv-services/download?token=" + dto.Token + "&ettn=" + dto.InvoiceUniqueKey + "&belgeTip=FATURA&onayDurumu=" + System.Web.HttpUtility.UrlEncode(connectionInfo.DownloadInvoiceType) + "&cmd=EARSIV_PORTAL_BELGE_INDIR&");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Connection", "keep-alive");
                request.AddHeader("sec-ch-ua", "\" Not;A Brand\";v=\"99\", \"Google Chrome\";v=\"91\", \"Chromium\";v=\"91\"");
                request.AddHeader("sec-ch-ua-mobile", "?0");
                request.AddHeader("Upgrade-Insecure-Requests", "1");
                request.AddHeader("DNT", "1");
                client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36";
                request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.AddHeader("Sec-Fetch-Site", "same-origin");
                request.AddHeader("Sec-Fetch-Mode", "navigate");
                request.AddHeader("Sec-Fetch-User", "?1");
                request.AddHeader("Sec-Fetch-Dest", "document");
                request.AddHeader("Referer", connectionInfo.EArchiveURL + "index.jsp?token=" + dto.Token + "&v=1624464892887");
                request.AddHeader("Accept-Language", "tr-TR,tr;q=0.9,en-US;q=0.8,en;q=0.7");
                baseResponse.Data.Data = client.DownloadData(request);                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return baseResponse;
        }
    }
}

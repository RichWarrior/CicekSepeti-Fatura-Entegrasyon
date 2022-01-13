using Core.Utilities;
using EArchiveClient.DTO.Request;
using EArchiveClient.DTO.Response;
using Newtonsoft.Json;
using RestSharp;

namespace EArchiveClient.Commands
{
    public class GetTokenCommand
    {
        public BaseResponse<GetTokenResponse> Execute(GetTokenRequest dto)
        {
            BaseResponse<GetTokenResponse> baseResponse = new BaseResponse<GetTokenResponse>();
            Environment.Environment env = Environment.Environment.Instance;
            ConnectionInfo connectionInfo = ConnectionInfo.Instance;
            try
            {
                var client = new RestClient(connectionInfo.EArchiveURL+"earsiv-services/assos-login");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
                request.AddHeader("Credentials", "omit");
                request.AddHeader("Accept", "*/*");
                request.AddHeader("Accept-Language", "tr,en-US;q=0.9,en;q=0.8");
                request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("Pragma", "no-cache");
                request.AddHeader("sec-fetch-mode", "cors");
                request.AddHeader("sec-fetch-site", "same-origin");
                request.AddHeader("Referer", "https://earsivportal.efatura.gov.tr/intragiris.html");
                request.AddHeader("mode", "cors");
                //request.AddParameter("assoscmd", env.IsProduction ? "anologin" : "login");
                request.AddParameter("assoscmd", "anologin");
                request.AddParameter("rtype", "json");
                request.AddParameter("userid", dto.UserId);
                request.AddParameter("sifre", dto.Password);
                request.AddParameter("sifre2", dto.Password);
                request.AddParameter("parola", "1");
                IRestResponse response = client.Execute(request);                
                baseResponse.StatusCode = response.StatusCode;
                baseResponse.Message = response.Content.ToString();
                baseResponse.Data = JsonConvert.DeserializeObject<GetTokenResponse>(response.Content);                
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            return baseResponse;
        }
    }
}

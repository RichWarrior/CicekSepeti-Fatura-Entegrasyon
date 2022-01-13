namespace EArchiveClient.DTO.Request
{
    public class DownloadPDFRequest
    {
        public string Token { get; set; }

        public string InvoiceUniqueKey { get; set; }
    }
}

using System.ComponentModel;

namespace Core.Enums
{
    public enum EnumInvoiceStatus
    {
        [Description("Taslak Oluşturmayı Bekleniyor")]
        WaitingDraft = 1,
        [Description("Taslak Oluşturuluyor")]
        CreatingDraft = 2,
        [Description("Taslak Oluşturuldu")]
        CreatedDraft = 3,
        [Description("Taslak Oluşturulamadı")]
        NotCreatedDraft = 4,
        [Description("PDF İşleniyor")]
        DownloadingPDF = 5,
        [Description("PDF İşlendi")]
        DownloadedPDF = 6,
        [Description("PDF İndirilemedi")]
        NotDownloadedPDF = 7,
        [Description("Mail Gönderildi")]
        SendedMail = 8,
        [Description("Fatura Adı Hatalı")]
        IncorrectInvoiceName = 9
    }
}

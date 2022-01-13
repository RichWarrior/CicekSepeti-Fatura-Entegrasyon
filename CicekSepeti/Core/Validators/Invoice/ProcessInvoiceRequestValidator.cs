using Core.Dtos.Request.Invoice;
using FluentValidation;
using System.Linq;

namespace Core.Validators.Invoice
{
    public class ProcessInvoiceRequestValidator : AbstractValidator<ProcessInvoiceRequestDTO>
    {
        public ProcessInvoiceRequestValidator()
        {
            RuleFor(f => f.Invoices)
                .Must((f) =>
                {
                    if (f.Any())
                        return true;
                    return false;
                }).WithMessage("Lütfen Fatura Bilgisi Veriniz");

            RuleFor(f => f.InvoiceStatusId)
                .Must((f) =>
                {
                    if (f == 1 || f == 3)
                        return true;
                    return false;
                }).WithMessage("Lütfen Geçerli İşlem Tipi Giriniz");

        }
    }
}

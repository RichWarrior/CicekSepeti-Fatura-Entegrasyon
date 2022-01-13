using Core.Dtos.Request.File;
using FluentValidation;

namespace Core.Validators.File
{
    public class UploadFileRequestValidator : AbstractValidator<UploadFileRequestDTO>
    {
        public UploadFileRequestValidator()
        {
            RuleFor(f => f.File).Must(f =>
            {
                if (f != null && f.Count == 1)
                    return true;
                return false;
            }).WithMessage("Lütfen 1 Adet Dosya Yükleyiniz");

            RuleFor(f => f.TermStartDate).NotNull().WithMessage("Lütfen Başlangıç Tarihi Giriniz");

            RuleFor(f => f.TermEndDate).NotNull().WithMessage("Lütfen Bitiş Tarihi Giriniz");

            //RuleFor(f => f.TermEndDate).Must((rootObject, list, context) =>
            //{
            //    if (rootObject.TermStartDate <= rootObject.TermEndDate)
            //    {
            //        var totalMonths = Math.Abs(((rootObject.TermStartDate.Year - rootObject.TermEndDate.Year) * 12) + rootObject.TermStartDate.Month - rootObject.TermEndDate.Month);
            //        return totalMonths == 0;
            //    }
            //    return false;
            //}).WithMessage("Lütfen 1 Dönem Giriniz");
        }
    }
}

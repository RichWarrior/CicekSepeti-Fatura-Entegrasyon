using Core.Dtos.Request.Auth;
using FluentValidation;

namespace Core.Validators.Auth
{
    public class LogInRequestValidator :AbstractValidator<LogInRequestDTO>
    {
        public LogInRequestValidator()
        {
            RuleFor(f => f.Email).NotEmpty().WithMessage("Email Adresi Boş Olamaz");
            RuleFor(f => f.Email).EmailAddress().WithMessage("Geçerli Bir Email Adresi Giriniz");
            RuleFor(f => f.Password).NotEmpty().WithMessage("Şifre Boş Olamaz");
        }
    }
}

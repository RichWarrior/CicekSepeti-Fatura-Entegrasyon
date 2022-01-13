using Core.Validators.Auth;
using Core.Validators.File;
using Core.Validators.Invoice;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace CicekSepeti.API.Bootstrapper
{
    /// <summary>
    /// 
    /// </summary>
    public static class FluentValidatorBootstrapper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IMvcBuilder AddFluentValidatorBootstrapper(this IMvcBuilder @this)
        {
            @this.AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining(typeof(LogInRequestValidator));
                fv.RegisterValidatorsFromAssemblyContaining(typeof(UploadFileRequestValidator));
                fv.RegisterValidatorsFromAssemblyContaining(typeof(ProcessInvoiceRequestValidator));
            });
            return @this;
        }
    }
}

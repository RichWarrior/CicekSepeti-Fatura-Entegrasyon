using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace CicekSepeti.API.Bootstrapper
{
    /// <summary>
    /// 
    /// </summary>
    public static class LocalizationBootstrapper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IServiceCollection AddLocalizationMessage(this IServiceCollection @this)
        {
            @this.AddLocalization(options =>
            {
                options.ResourcesPath = "";
            });
            return @this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseLocalizationMessage(this IApplicationBuilder @this)
        {
            var supportedCulture = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("tr")
            };

            @this.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("tr"),
                SupportedCultures = supportedCulture,
                SupportedUICultures = supportedCulture
            });

            return @this;
        }
    }
}

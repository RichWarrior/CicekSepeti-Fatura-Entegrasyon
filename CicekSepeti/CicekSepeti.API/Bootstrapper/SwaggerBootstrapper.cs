using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CicekSepeti.API.Bootstrapper
{
    /// <summary>
    /// 
    /// </summary>
    public static class SwaggerBootstrapper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection @this)
        {
            @this.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Swagger Title",
                    Version = "v1"
                });
            });

            return @this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerGen(this IApplicationBuilder @this)
        {
            @this.UseSwagger();
            @this.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Title"));
            return @this;
        }

    }
}

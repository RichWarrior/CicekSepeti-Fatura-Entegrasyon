using Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Service;
using Service.Repositories;

namespace CicekSepeti.API.Bootstrapper
{
    /// <summary>
    /// 
    /// </summary>
    public static class DependencyInjectionBootstrapper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IServiceCollection AddDependencyInjection(this IServiceCollection @this)
        {
            @this.AddScoped<IUnitOfWork, UnitOfWork>();
            @this.AddScoped<IFileRepository, FileRepository>();
            @this.AddScoped<IInvoiceRepository, InvoiceRepository>();
            @this.AddScoped<IInvoiceStatusRepository, InvoiceStatusRepository>();
            @this.AddScoped<IParameterRepository, ParameterRepository>();

            @this.AddScoped<IRabbitMQService, RabbitMQService>();
            @this.AddScoped<IMinioService, MinioService>();

            return @this;
        }
    }
}

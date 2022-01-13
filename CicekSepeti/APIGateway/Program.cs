using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace APIGateway
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            environment = Environment.Environment.Instance;
            CreateHostBuilder(args).Build().Run();
        }

        static Environment.Environment environment = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args).ConfigureServices(services =>
          {
              services.AddOcelot();
          }).ConfigureAppConfiguration((host, config) =>
          {
              if (environment.IsDevelopment)
                  config.AddJsonFile("ocelot.development.json");
              else if (environment.IsStaging)
                  config.AddJsonFile("ocelot.staging.json");
              else if (environment.IsProduction)
                  config.AddJsonFile("ocelot.production.json");
          })
             .ConfigureWebHostDefaults(webBuilder =>
             {
                 webBuilder.UseStartup<Startup>()
                     .Configure(async app => await app.UseOcelot())
                     .UseUrls("http://localhost:5000");
             });
    }
}

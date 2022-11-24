using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductManager.Services;
using ProductManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ProcessData, ProcessData>();
            services.AddSingleton<IWebService, WebService>();
            services.AddScoped<IFileService, FileService>();
            /*services.AddSingleton(new LoggerFactory()
                .AddConsole(Configuration.GetSection("Logging"))
                .AddSerilog()
                .AddDebug());
            services.AddLogging();*/
        }

        public void Configure(IHost consoleApp, IHostEnvironment env)
        {
            consoleApp.Services.GetService<ProcessData>()?.Run();
        }
    }
}

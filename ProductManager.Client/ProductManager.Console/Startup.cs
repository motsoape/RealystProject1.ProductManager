using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductManager.Models;
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
            var baseURL = Configuration["ProductManagerAPI:BaseURL"];
            var appSettings = new AppSettings
            {
                ApplicationBaseURL = baseURL
            };

            services.AddSingleton(appSettings);
            services.AddSingleton<ProcessData, ProcessData>();
            services.AddSingleton<IWebService, WebService>();
            services.AddScoped<IFileService, FileService>();
        }

        public void Configure(IHost consoleApp, IHostEnvironment env)
        {
            consoleApp.Services.GetService<ProcessData>()?.Run();
        }
    }
}

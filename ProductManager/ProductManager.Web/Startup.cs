using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductManager.Repositories;
using ProductManager.Repositories.Entities;
using ProductManager.Repositories.Interfaces;
using ProductManager.Repositories.Models;
using ProductManager.Services;
using ProductManager.Services.Interfaces;
using System.Text.Json.Serialization;

namespace ProductManager.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //Register all services into the container
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["ConnectionString:ProductManagerDB"];

            services.AddControllersWithViews();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Product Manager API", Version= "v1" });
            });

            //Repositories
            services.AddDbContext<ProductManagerDbContext>(opts => opts.UseSqlServer(connectionString));
            services.AddScoped<IDataRepository<ProductModel>, ProductRepository>();
            services.AddScoped<IDataRepository<CommentModel>, CommentRepository>();

            //Services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IStatsService, StatsService>();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (app.Environment.IsDevelopment())
            {  
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllerRoute(
            name: "default",
            pattern: "{controller=ProductsPage}/{action=Index}");
            app.MigrateDatabase();
            app.Run();
        }
    }

    public static class MigrationManager
    {
        public static WebApplication MigrateDatabase(this WebApplication webApp)
        {
            using (var scope = webApp.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<ProductManagerDbContext>())
                {
                    try
                    {
                        //appContext.Database.Migrate();
                        appContext.Database.EnsureCreated();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            return webApp;
        }
    }
}

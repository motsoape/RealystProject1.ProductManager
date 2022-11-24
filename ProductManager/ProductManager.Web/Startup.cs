using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductManager.Repositories;
using ProductManager.Repositories.Entities;
using ProductManager.Repositories.Interfaces;
using ProductManager.Services;
using ProductManager.Services.Interfaces;

namespace ProductManager.Web
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
            var connectionString = Configuration["ConnectionString:ProductManagerDB"];

            services.AddControllersWithViews();

            //Repositories
            services.AddDbContext<ProductManagerDbContext>(opts => opts.UseSqlServer(connectionString));
            services.AddScoped<IDataRepository<Product>, ProductRepository>();
            services.AddScoped<IDataRepository<Comment>, CommentRepository>();

            //Services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICommentService, CommentService>();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
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

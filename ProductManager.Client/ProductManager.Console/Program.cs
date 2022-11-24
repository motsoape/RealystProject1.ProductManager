
using Microsoft.Extensions.Hosting;
using ProductManager;

var builder = Host.CreateApplicationBuilder(args);
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);
var consoleApp = builder.Build();

startup.Configure(consoleApp, builder.Environment);
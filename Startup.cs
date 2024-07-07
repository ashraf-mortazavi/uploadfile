using System.Reflection;
using CsvFileUploadApp.Api.Extensions.Middleware;
using CsvFileUploadApp.Infrastructure.Extentions.DependencyInjections;

namespace CsvFileUploadApp;

public class Startup(IConfiguration configuration)
{
    private IConfiguration Configuration { get; } = configuration;

    public  void ConfigureServices(IServiceCollection services)
    {
        services.AddFilePersistence(configuration);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddControllers();
    }

    public  void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        
        app.UseConfiguredMigration();
    }
}
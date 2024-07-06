using CsvFileUploadApp.Consumers;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace CsvFileUploadApp;

public static class CsvFilePublishExtention
{
    public static void AddCsvFilePublishServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddMassTransit(configure =>
        {
            var brokerConfig = configuration.GetSection(BrokerOptions.SectionName)
                .Get<BrokerOptions>();

            if (brokerConfig is null)
            {
                throw new ArgumentNullException(nameof(BrokerOptions));
            }

            configure.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(brokerConfig.Host, hostConfigure =>
                {
                    hostConfigure.Username(brokerConfig.Username);
                    hostConfigure.Password(brokerConfig.Password);
                });

                cfg.ConfigureEndpoints(context);
            });

            configure.AddConsumer<FileCsvUploadEventConsumer>();
        });

        services.AddOptions<ContentFileOptions>()
            .BindConfiguration(nameof(ContentFileOptions));
    }
}
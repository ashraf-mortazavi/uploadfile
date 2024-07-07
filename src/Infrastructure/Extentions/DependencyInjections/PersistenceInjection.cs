using CsvFileUploadApp.Application.Configurations;
using CsvFileUploadApp.Infrastructure.Consumers;
using CsvFileUploadApp.Infrastructure.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace CsvFileUploadApp.Infrastructure.Extentions.DependencyInjections;

public static class PersistenceInjection
{
    public static void AddFilePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddMassTransit(configure =>
        {
            var brokerConfig = configuration.GetSection(RabbitOptions.SectionName)
                .Get<RabbitOptions>();

            if (brokerConfig is null)
            {
                throw new ArgumentNullException(nameof(RabbitOptions));
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

            configure.AddConsumer<CreateFileEventConsumer>();
            configure.AddConsumer<PublishUploadedFileFromConsumer>();
            configure.AddConsumer<CreateFileEventFromPublishConsumer>();

        });

        services.AddOptions<RabbitConfig>()
            .BindConfiguration(nameof(RabbitConfig));
    }
}
using CsvFileUploadApp.Infrastructure.IntegrationEvents;
using CsvFileUploadApp.Infrastructure.Persistence;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace CsvFileUploadApp.Infrastructure.Consumers;

public class PublishUploadedFileFromConsumer(AppDbContext dbContext, [FromServices] IPublishEndpoint publishEndpoint)
    : IConsumer<CreateFileEventFromFirstQueue>
{
    public async Task Consume(ConsumeContext<CreateFileEventFromFirstQueue> context)
    {
        var message = context.Message;

        if (message is null) return;
    
        try
        {
            if (context.Message.isReady)
            {
                using (var memoryStream = new MemoryStream(message.Content))
                {
                    await publishEndpoint.Publish(new CreateFileEventFromFirstQueue
                    {
                        FileName = message.FileName,
                        ContentType =message.ContentType,
                        Content = memoryStream.ToArray(),
                        isReady =context.Message.isReady= false
                    });
                }

            }
            Console.WriteLine("File published From First Queue successfully!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}
using CsvFileUploadApp.Infrastructure.IntegrationEvents;
using CsvFileUploadApp.Infrastructure.Persistence;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace CsvFileUploadApp.Infrastructure.Consumers;

public class PublishUploadedFileFromConsumer(AppDbContext dbContext, [FromServices] IPublishEndpoint publishEndpoint) : IConsumer<CreateFileEvent>
{

    public async Task Consume(ConsumeContext<CreateFileEvent> context)
    {
        var message = context.Message;

        if (message is null) return;
        
        try
        {
            using (var memoryStream = new MemoryStream(message.Content))
            {
                await publishEndpoint.Publish(new CreateFileEvent(
                    FileName: message.FileName,
                    ContentType: message.ContentType,
                    Content: memoryStream.ToArray()));
            }

            Console.WriteLine("File published successfully!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}
using System.Globalization;
using CsvFileUploadApp.Infrastructure.IntegrationEvents;
using CsvFileUploadApp.Infrastructure.Persistence;
using CsvHelper;
using CsvHelper.Configuration;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using File = CsvFileUploadApp.Domain.CsvFile.File;

namespace CsvFileUploadApp.Infrastructure.Consumers;

public class CreateFileEventConsumer(AppDbContext dbContext) : IConsumer<CreateFileEvent>
{
    private readonly string _csvDirectory = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

    public async Task Consume(ConsumeContext<CreateFileEvent> context)
    {
        try
        {
            var message = context.Message;

            if (message is null) return;

            if (!Directory.Exists(_csvDirectory))
            {
                Directory.CreateDirectory(_csvDirectory);
            }

            var filePath = Path.Combine(_csvDirectory, message.FileName);
            var files = new List<File>();
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, configuration))
            {
                files.AddRange(csv.GetRecords<File>().ToList());
            }

            dbContext.Files.AddRange(files);
            await dbContext.SaveChangesAsync();
            Console.WriteLine("Data Insert successfully!");
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine("Code can not be duplicated!");
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}
using System.Globalization;
using CsvFileUploadApp.IntegrationEvents;
using CsvHelper;
using CsvHelper.Configuration;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace CsvFileUploadApp.Consumers;

public class FileCsvUploadEventConsumer(AppDbContext dbContext) : IConsumer<FileCsvUploadedEvent>
{
    private readonly string _csvDirectory = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

    public async Task Consume(ConsumeContext<FileCsvUploadedEvent> context)
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
            var users = new List<User>();
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, configuration))
            {
                users.AddRange(csv.GetRecords<User>().ToList());
            }

            dbContext.Users.AddRange(users);
            await dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine("Data is duplicated!" + e.Message);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error occured!");
            Console.WriteLine(e.Message);
            throw;
        }
    }
}
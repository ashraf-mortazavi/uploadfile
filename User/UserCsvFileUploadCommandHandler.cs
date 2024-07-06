using System.Globalization;
using CsvFileUploadApp.Operations;
using CsvHelper;
using CsvHelper.Configuration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CsvParser = CsvFileUploadApp.Utilities.Csv.CsvParser;

namespace CsvFileUploadApp;

public sealed class UserCsvFileUploadCommandHandler(AppDbContext dbContext)
    : IRequestHandler<UserCsvFileUploadCommand, OperationResult>
{
    private readonly string _csvDirectory = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

    public async Task<OperationResult> Handle(UserCsvFileUploadCommand request, CancellationToken cancellationToken)
    {
        if (request.File.Length == 0)
        {
            return new OperationResult(OperationResultStatus.InvalidRequest,
                value: "No CsvFile Uploaded!");
        }

        if (!Directory.Exists(_csvDirectory))
        {
            Directory.CreateDirectory(_csvDirectory);
        }

        var filePath = Path.Combine(_csvDirectory, request.File.FileName);
        var users = new List<User>();
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
        };
            
        try
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, configuration))
            {
                users.AddRange(csv.GetRecords<User>().ToList());
            }
        }
        catch (Exception e) 
        {
            Console.WriteLine(e.Message);
        }

        dbContext.Users.AddRange(users);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new OperationResult(OperationResultStatus.Ok, value: users);
    }
}
    
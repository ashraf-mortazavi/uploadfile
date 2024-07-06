using System.Globalization;
using CsvFileUploadApp.Application.Operations;
using CsvFileUploadApp.Infrastructure.Persistence;
using CsvHelper;
using CsvHelper.Configuration;
using MediatR;
using File = CsvFileUploadApp.Domain.CsvFile.File;

namespace CsvFileUploadApp.Application.CsvFiles.CreateFile;

public sealed class CreateFileCommandHandler(AppDbContext dbContext)
    : IRequestHandler<CreateFileCommand, OperationResult>
{
    private readonly string _csvDirectory = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

    public async Task<OperationResult> Handle(CreateFileCommand request, CancellationToken cancellationToken)
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
        var files = new List<File>();
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
        };
            
        try
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, configuration))
            {
                files.AddRange(csv.GetRecords<File>().ToList());
            }
            
            dbContext.Files.AddRange(files);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e) 
        {
            Console.WriteLine(e.Message);
        }
        
        return new OperationResult(OperationResultStatus.Ok, value: files);
    }
}
    
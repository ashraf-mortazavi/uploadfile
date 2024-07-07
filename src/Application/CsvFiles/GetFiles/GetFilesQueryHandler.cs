using CsvFileUploadApp.Application.Operations;
using CsvFileUploadApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CsvFileUploadApp.Application.CsvFiles.GetFiles;

public class GetFilesQueryHandler(AppDbContext dbContext) : IRequestHandler<GetFilesQuery, OperationResult>
{
    public async Task<OperationResult> Handle(GetFilesQuery request, CancellationToken cancellationToken)
    {
        var files = await dbContext.Files.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
        
        return files.Count < 1 ? new OperationResult(OperationResultStatus.NotFound, value: "Not Found Any File!")
            : new OperationResult(OperationResultStatus.Ok, value: files);
    }
}
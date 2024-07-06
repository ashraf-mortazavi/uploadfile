using CsvFileUploadApp.Application.Operations;
using CsvFileUploadApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CsvFileUploadApp;

public sealed class DeleteFileCommandHandler(AppDbContext dbContext)
    : IRequestHandler<DeleteFileCommand, OperationResult>
{
    
    public async Task<OperationResult> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        var files = await dbContext.Files.ToListAsync(cancellationToken: cancellationToken);
        
        foreach (var file in files)
        {
            file.IsDeleted = true;
            file.UpdatedAt = DateTime.Now;
        }
        dbContext.Files.UpdateRange(files);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new OperationResult(OperationResultStatus.Ok, value: files);
    }
}
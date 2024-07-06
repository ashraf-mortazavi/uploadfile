using CsvFileUploadApp.Application.Operations;
using CsvFileUploadApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CsvFileUploadApp.Application.CsvFiles.GetFiles;

public class GetFileByCodeQueryHandler(AppDbContext appDbContext)
    : IRequestHandler<GetFileByCodeQuery, OperationResult>
{
    
    public async Task<OperationResult> Handle(GetFileByCodeQuery request, CancellationToken cancellationToken)
    {
        var file = await appDbContext.Files
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Code == request.Code, cancellationToken: cancellationToken);

        if (file is null)
        {
            return new OperationResult(OperationResultStatus.NotFound, value: "File Not Found");
        }

        return new OperationResult(OperationResultStatus.Ok, value: file);
    }
}
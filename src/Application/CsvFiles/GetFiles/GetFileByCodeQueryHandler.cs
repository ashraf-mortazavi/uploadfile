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
            .FirstOrDefaultAsync(x => x.Code == request.Code, cancellationToken: cancellationToken);

        return file is null ? new OperationResult(OperationResultStatus.NotFound, value: "File Not Found")
            : new OperationResult(OperationResultStatus.Ok, value: file);
    }
}
using CsvFileUploadApp.Operations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CsvFileUploadApp;

public class UserCsvFileByCodeQueryHandler(AppDbContext appDbContext)
    : IRequestHandler<UserCsvFileByCodeQuery, OperationResult>
{
    
    public async Task<OperationResult> Handle(UserCsvFileByCodeQuery request, CancellationToken cancellationToken)
    {
        var user = await appDbContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Code == request.Code, cancellationToken: cancellationToken);

        if (user is null)
        {
            return new OperationResult(OperationResultStatus.NotFound, value: "User Not Found");
        }

        return new OperationResult(OperationResultStatus.Ok, value: user);
    }
}
using CsvFileUploadApp.Operations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CsvFileUploadApp;

public class UserCsvFilesQueryHandler(AppDbContext dbContext) : IRequestHandler<UserCsvFilesQuery, OperationResult>
{
    
    public async Task<OperationResult> Handle(UserCsvFilesQuery request, CancellationToken cancellationToken)
    {
        var users = await dbContext.Users.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
        if (users.Count < 1 )
        {
            return new OperationResult(OperationResultStatus.NotFound, value: "Not User Found");
        }

        return new OperationResult(OperationResultStatus.Ok, value: users);
    }
}
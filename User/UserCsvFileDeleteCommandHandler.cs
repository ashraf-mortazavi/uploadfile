using CsvFileUploadApp.Operations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CsvFileUploadApp;

public sealed class UserCsvFileDeleteCommandHandler(AppDbContext dbContext)
    : IRequestHandler<UserCsvFileDeleteCommand, OperationResult>
{
    
    public async Task<OperationResult> Handle(UserCsvFileDeleteCommand request, CancellationToken cancellationToken)
    {
        var users = await dbContext.Users.ToListAsync(cancellationToken: cancellationToken);
        
        foreach (var user in users)
        {
            user.IsDeleted = true;
            user.UpdatedAt = DateTime.Now;
        }
        dbContext.Users.UpdateRange(users);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new OperationResult(OperationResultStatus.Ok, value: users);
    }
}
using CsvFileUploadApp.Api.Extensions.Endpoint;
using CsvFileUploadApp.Application.CsvFiles.DeleteFile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CsvFileUploadApp.Api.Endpoints.CsvFiles;

[Route("api")]
[ApiController]
public class DeleteUploadedFileEndpoint(IMediator mediator) : ControllerBase
{
    
    [HttpDelete]
    public async Task<IActionResult> DeleteUploadedFiles()
    {
        var operation = await mediator.Send(new DeleteFileCommand());

        return this.InternalReturnResponse(operation);
    }
}
using CsvFileUploadApp.Api.Extensions.Endpoint;
using CsvFileUploadApp.Application.CsvFiles.CreateFile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CsvFileUploadApp.Api.Endpoints.CsvFiles;

[ApiController]
[Route("api")]
public class CreateUploadedFileEndpoint(IMediator mediator) : ControllerBase
{
    
    [HttpPost("uploadfile")]
    public async Task<IActionResult> CreateUploadedFile([FromForm] IFormFile file)
    {
        var operation = await mediator.Send(new
            CreateFileCommand(File: file));

        return this.InternalReturnResponse(operation);
    }
}
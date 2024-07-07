using CsvFileUploadApp.Api.Extensions.Endpoint;
using CsvFileUploadApp.Application.CsvFiles.GetFiles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CsvFileUploadApp.Api.Endpoints.CsvFiles;

[Route("api")]
[ApiController]
public class GetUploadedFilesEndpoint(IMediator mediator) : ControllerBase
{
    
    [HttpGet( "{code?}")]
    public async Task<IActionResult> GetUploadedFiles([FromRoute] long? code)
    {
        if (code.HasValue)
        {
            var operation = await mediator.Send(new
                GetFileByCodeQuery(Code: code));

            return this.InternalReturnResponse(operation);
        }

        var operations = await mediator.Send(new GetFilesQuery());

        return this.InternalReturnResponse(operations);
    }
}
using CsvFileUploadApp.IntegrationEvents;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CsvFileUploadApp.Controllers;

[Route("api")]
[ApiController]
public class CsvController(IMediator mediator) : ControllerBase
{
    Random random = new();
    [HttpPost("uploadCsv")]
    public async Task<IActionResult> UploadCsv([FromForm] IFormFile file)
    {
        var operation = await mediator.Send(new
            UserCsvFileUploadCommand(File: file));

        return this.InternalReturnResponse(operation);
    }

    [HttpGet("{code?}")]
    public async Task<IActionResult> GetFileDatas([FromRoute] long? code)
    {
        if (code.HasValue)
        {
            var operation = await mediator.Send(new
                UserCsvFileByCodeQuery(Code: code));

            return this.InternalReturnResponse(operation);
        }

        var operations = await mediator.Send(new UserCsvFilesQuery());

        return this.InternalReturnResponse(operations);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteDatas()
    {
        var operation = await mediator.Send(new UserCsvFileDeleteCommand());

        return this.InternalReturnResponse(operation);
    }

    [HttpPost]
    public async Task<IActionResult> PublishUploadCsvFile([FromForm] IFormFile file,
        [FromServices] IPublishEndpoint publish)
    {
        if (file.Length == 0)
        {
            return BadRequest("Invalid file.");
        }
        
        var trackingCode = random.Next(1000, 10000);
        
        try
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                
               
                await publish.Publish(new FileCsvUploadedEvent(
                    FileName: file.FileName,
                    ContentType: file.ContentType,
                    Content: memoryStream.ToArray(),
                    "UPLOAD_FILE"+trackingCode));
            }

            return Ok("File published successfully! Keep this: UPLOAD_FILE"+trackingCode);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}
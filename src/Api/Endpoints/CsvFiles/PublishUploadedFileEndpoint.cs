using CsvFileUploadApp.Infrastructure.IntegrationEvents;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace CsvFileUploadApp.Api.Endpoints.CsvFiles;

[Route("api")]
[ApiController]
public class PublishUploadedFileEndpoint : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PublishUploadFile([FromForm] IFormFile file,
        [FromServices] IPublishEndpoint publish)
    {
        if (file.Length == 0)
            return BadRequest("Invalid file.");
        
        try
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                
               
                await publish.Publish(new CreateFileEvent(
                    FileName: file.FileName,
                    ContentType: file.ContentType,
                    Content: memoryStream.ToArray()));
            }

            return Ok("File published successfully! Keep this: UPLOAD_FILE");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}
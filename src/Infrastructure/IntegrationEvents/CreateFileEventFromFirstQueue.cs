namespace CsvFileUploadApp.Infrastructure.IntegrationEvents;

public class CreateFileEventFromFirstQueue
{
   public string FileName { get; init; }
   public string ContentType { get; init; }
   public byte[] Content { get; init; }
   public bool isReady { get; set; }
}
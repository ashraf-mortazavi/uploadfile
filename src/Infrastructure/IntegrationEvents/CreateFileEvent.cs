namespace CsvFileUploadApp.Infrastructure.IntegrationEvents;

public record CreateFileEvent(
        string FileName,
        string ContentType,
        byte[] Content);
namespace CsvFileUploadApp.IntegrationEvents;

public record FileCsvUploadedEvent(
        string FileName,
        string ContentType,
        byte[] Content,
        string TrackingCode);
namespace CsvFileUploadApp.Consumers.IntegrationEvents;

public record FileCsvUploadedEvent(IFormFile File);
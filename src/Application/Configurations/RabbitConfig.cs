namespace CsvFileUploadApp.Application.Configurations;

public sealed class RabbitConfig
{
    public RabbitOptions RabbitOptions { get; set; } = null!;

}

public sealed class RabbitOptions
{
    public const string SectionName = "RabbitOptions";

    public required string Host { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}

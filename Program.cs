namespace CsvFileUploadApp;

public static class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var builder = CreateHostBuilder(args);
            var app = builder.Build();
            app.Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
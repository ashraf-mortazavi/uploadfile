using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace CsvFileUploadApp.Utilities.Csv;

public static class CsvParser
{
    public  async static Task<List<User>> ParsAsList(Stream stream)
    {
        var users = new List<User>();
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
        };
        
        using var reader = new StreamReader(stream);
        {
            await reader.ReadToEndAsync();
            var csv = new CsvReader(reader, configuration);
            users.AddRange(csv.GetRecords<User>().ToList());
        }
        return users;
    }
}
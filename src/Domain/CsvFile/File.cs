using CsvHelper.Configuration.Attributes;

namespace CsvFileUploadApp.Domain.CsvFile;

public class File
{
    [Optional]
    public int Id { get; set; }
    public long Code { get; set; }
    public long PhoneNumber { get; set; }
    public string FullName { get; set; }
    public Gender Gender { get; set; }
    public string Age { get; set; }
    public string Job { get; set; }
    public string EducationDegree { get; set; }
    public string WorkExperience { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}
using Microsoft.EntityFrameworkCore;
using File = CsvFileUploadApp.Domain.CsvFile.File;

namespace CsvFileUploadApp.Infrastructure.Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<File> Files { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
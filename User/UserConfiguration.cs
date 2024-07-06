using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CsvFileUploadApp;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasIndex(x => x.Code).IsUnique();

        builder.Property(x => x.Code)
            .HasMaxLength(20);
        
        builder.Property(x => x.FullName)
            .IsRequired()
            .HasMaxLength(80);
        
        builder.Property(x => x.Gender)
            .HasConversion(new EnumToStringConverter<Gender>())
            .HasMaxLength(8);
       
        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(20);
    }
}
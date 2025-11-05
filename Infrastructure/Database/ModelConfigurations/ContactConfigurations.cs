using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.ModelConfigurations;

internal sealed class ContactConfigurations : BaseModelConfiguration<Contact>
{
    protected override void ConfigureInternal(EntityTypeBuilder<Contact> builder)
    {
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(255);
        builder.Property(x => x.MiddleName).HasMaxLength(255);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Email)
               .IsRequired()
               .HasMaxLength(255)
               .HasConversion(x => x.ToString(), value => new(value!));
    }
}
using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.ModelConfigurations;

internal sealed class ContactConfigurations : BaseModelConfiguration<Contact>
{
    public override void ConfigureInternal(EntityTypeBuilder<Contact> builder)
    {
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
    }
}
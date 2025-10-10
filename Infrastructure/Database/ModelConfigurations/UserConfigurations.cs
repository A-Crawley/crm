using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.ModelConfigurations;

internal sealed class UserConfigurations : BaseModelConfiguration<User>
{
    public override void ConfigureInternal(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Email).IsRequired();
        builder.HasIndex(x => new { x.Email, x.ArchivedAt }).IsUnique();
    }
}
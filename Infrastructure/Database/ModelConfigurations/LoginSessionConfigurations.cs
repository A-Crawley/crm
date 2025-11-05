using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.ModelConfigurations;

internal sealed class LoginSessionConfigurations : BaseModelConfiguration<LoginSession>
{
    protected override void ConfigureInternal(EntityTypeBuilder<LoginSession> builder)
    {
        builder.Property(x => x.UserId).IsRequired();
        builder.HasOne(x => x.User).WithMany(x => x.LoginSessions);
    }
}
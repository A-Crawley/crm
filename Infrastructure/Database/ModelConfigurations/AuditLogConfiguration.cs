using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.ModelConfigurations;

internal class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("audit_log");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().HasColumnName("id");
        builder.Property(x => x.When).IsRequired().HasColumnName("when");
        builder.Property(x => x.Reference).IsRequired().HasColumnName("reference");
        builder.Property(x => x.Table).IsRequired().HasColumnName("table");
        builder.Property(x => x.NewValue).HasColumnName("new_value");
        builder.Property(x => x.OldValue).HasColumnName("old_value");
        builder.Property(x => x.Who).HasColumnName("who");
    }
}
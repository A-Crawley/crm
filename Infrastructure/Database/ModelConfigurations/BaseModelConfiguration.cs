using Domain.Extensions;
using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.ModelConfigurations;

internal abstract class BaseModelConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        ConfigureTable(builder);
        ConfigureColumns(builder);
        ConfigureInternal(builder);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.CreatedAt).IsRequired();
    }

    protected abstract void ConfigureInternal(EntityTypeBuilder<T> builder);

    private void ConfigureTable(EntityTypeBuilder<T> builder)
    {
        var tableName = typeof(T).Name;
        builder.ToTable(tableName.ToSnakeCase());
    }

    private void ConfigureColumns(EntityTypeBuilder<T> builder)
    {
        foreach (var propertyName in builder.Metadata.GetProperties().Where(p => p.FieldInfo is not null)
                     .Select(t => t.Name))
            builder.Property(propertyName).HasColumnName(propertyName.ToSnakeCase());
    }
}
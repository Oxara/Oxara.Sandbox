using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ETag.Delta;

public partial class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> entity)
    {
        entity.HasKey(e => e.UUID);
        entity.Property(e => e.RowVersion).IsRowVersion().HasConversion<byte[]>();
    }
}

public partial class BaseEntity
{
    public Guid UUID { get; set; }
    public byte[] RowVersion { get; set; }
}

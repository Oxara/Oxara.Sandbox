using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json.Serialization;

namespace ETag.Delta
{
    public partial class DummyEntityConfiguration : IEntityTypeConfiguration<DummyEntity>
    {
        public void Configure(EntityTypeBuilder<DummyEntity> entity)
        {
            entity.ToTable(nameof(DummyEntity));
            entity.HasKey(e => e.UUID);
            entity.Property(e => e.RowVersion).IsRowVersion().HasConversion<byte[]>();

            entity.Property(e => e.UserName).HasMaxLength(255);
            entity.Property(e => e.UserEmail).IsRequired().HasMaxLength(255);

            // One To Many
            entity.HasMany(e => e.Relations)
                  .WithOne(r => r.DummyEntity)
                  .HasForeignKey(r => r.DummyEntityUUID)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public partial class DummyEntity
    {
        public Guid UUID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public byte[] RowVersion { get; set; }
        public ICollection<DummyEntityRelation> Relations { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json.Serialization;

namespace ETag.Delta
{
    public partial class DummyEntityRelationConfiguration : IEntityTypeConfiguration<DummyEntityRelation>
    {
        public void Configure(EntityTypeBuilder<DummyEntityRelation> entity)
        {
            entity.ToTable(nameof(DummyEntityRelation));
            entity.HasKey(e => e.UUID);
            entity.Property(e => e.RowVersion).IsRowVersion().HasConversion<byte[]>();

            entity.Property(e => e.UserName).HasMaxLength(255);
            entity.Property(e => e.UserEmail).IsRequired().HasMaxLength(255);

            // Many To One
            entity.HasOne(r => r.DummyEntity)
                  .WithMany(d => d.Relations) // Relations koleksiyonunu kullanarak Many-to-One iliþkiyi belirtiyoruz
                  .HasForeignKey(r => r.DummyEntityUUID);
        }
    }

    public partial class DummyEntityRelation
    {
        public Guid UUID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public byte[] RowVersion { get; set; }
        public Guid DummyEntityUUID { get; set; }
        public DummyEntity DummyEntity { get; set; }
    }
}

using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tryout.ETag.Delta.EFContext
{
    public partial class DummyEntityConfiguration : IEntityTypeConfiguration<DummyEntity>
    {
        public void Configure(EntityTypeBuilder<DummyEntity> entity)
        {
            entity.ToTable(nameof(DummyEntity));
            entity.HasKey(e => e.UUID);
            entity
                .Property(e => e.RowVersion)
                .IsRowVersion()
                .HasConversion<byte[]>();

            entity.Property(e => e.UserName)
                  .HasMaxLength(255);

            entity.Property(e => e.UserEmail)
                  .IsRequired()
                  .HasMaxLength(255);



            entity.HasData(DataSeeder.CreateMockData());
        }
    }

    public partial class DummyEntity 
    {
        public Guid UUID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public byte[] RowVersion { get; set; }
    }

    public static partial class DataSeeder
    {
        public static List<DummyEntity> CreateMockData()
        {
            // Toplu mock veri oluþturma
            var faker = new Faker<DummyEntity>()
                .RuleFor(e => e.UUID, f => Guid.CreateVersion7())
                .RuleFor(e => e.UserName, f => f.Name.FullName())
                .RuleFor(e => e.UserEmail, f => f.Internet.Email());

            // 10.000 kayýt oluþtur
            var mockData = faker.Generate(10000);

            // Rastgele bir konuma özel kayýt ekle
            var specialRecord = new DummyEntity
            {
                UUID = Guid.CreateVersion7(),
                UserName = "Erdem ÖZKARA",
                UserEmail = "erdemozkara@hotmail.com.tr"
            };

            // Özel kaydý rastgele bir pozisyona ekleme
            var randomIndex = new Random().Next(0, mockData.Count);
            mockData.Insert(randomIndex, specialRecord);

            return mockData;
        }
    }
}

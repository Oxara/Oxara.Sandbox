using Microsoft.EntityFrameworkCore;

namespace ETag.Delta
{
    public static partial class DataSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // Ana tablo için dummy veriler
            var dummyEntities = new List<DummyEntity>();
            for (int i = 1; i <= 10000; i++)
            {
                dummyEntities.Add(new DummyEntity
                {
                    UUID = Guid.CreateVersion7(),
                    UserName = $"User {i}",
                    UserEmail = $"user{i}@example.com"
                });
            }

            // Alt tablo için ilişkili dummy veriler
            var dummyRelations = new List<DummyEntityRelation>();
            foreach (var entity in dummyEntities)
            {
                for (int j = 1; j <= 5; j++) // Her entity için 5 relation
                {
                    dummyRelations.Add(new DummyEntityRelation
                    {
                        UUID = Guid.CreateVersion7(),
                        UserName = $"{entity.UserName}'s Relation {j}",
                        UserEmail = $"relation{j}@example.com",
                        DummyEntityUUID = entity.UUID // Yabancı anahtar eşleştirmesi
                    });
                }
            }

            // ModelBuilder kullanarak verileri ekle
            modelBuilder.Entity<DummyEntity>().HasData(dummyEntities);
            modelBuilder.Entity<DummyEntityRelation>().HasData(dummyRelations);
        }
    }
}

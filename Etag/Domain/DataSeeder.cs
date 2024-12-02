using Bogus;
using Microsoft.EntityFrameworkCore;
namespace ETag.Delta;

public static partial class DataSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        // Alan adları listesi
        var domains = new[] { "example.com", "test.com", "demo.net", "sample.org" };

        // User için Faker
        var userFaker = new Faker<User>()
            .RuleFor(u => u.UUID, f => Guid.CreateVersion7())
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.UserName, (f, u) => $"{u.FirstName.ToLower()}.{u.LastName.ToLower()}")
            .RuleFor(u => u.UserEmail, (f, u) => $"{u.FirstName.ToLower()}.{u.LastName.ToLower()}@{f.PickRandom(domains)}")
            .RuleFor(u => u.Birthday, f => DateOnly.FromDateTime(f.Date.Past(30))); // Son 30 yıl içinde doğum tarihi

        var users = userFaker.Generate(1000);

        // UserContact için Faker
        var userContactFaker = new Faker<UserContact>()
            .RuleFor(uc => uc.UUID, f => Guid.CreateVersion7())
            .RuleFor(uc => uc.FirstName, f => f.Name.FirstName())
            .RuleFor(uc => uc.LastName, f => f.Name.LastName())
            .RuleFor(uc => uc.UserName, (f, uc) => $"{uc.FirstName.ToLower()}.{uc.LastName.ToLower()}")
            .RuleFor(uc => uc.UserEmail, (f, uc) => $"{uc.FirstName.ToLower()}.{uc.LastName.ToLower()}@{f.PickRandom(domains)}")
            .RuleFor(uc => uc.UserUUID, f => f.PickRandom(users).UUID);

        var userContacts = userContactFaker.Generate(5000); // Her kullanıcı için 5 ilişki

        // ModelBuilder kullanarak verileri ekle
        modelBuilder.Entity<User>().HasData(users);
        modelBuilder.Entity<UserContact>().HasData(userContacts);
    }
}

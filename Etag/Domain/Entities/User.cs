using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ETag.Delta;

public partial class UserConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> entity)
    {
        entity.ToTable(nameof(User));
        entity.Property(e => e.UserName).HasMaxLength(255);
        entity.Property(e => e.FirstName).IsRequired().HasMaxLength(255);
        entity.Property(e => e.LastName).IsRequired().HasMaxLength(255);

        entity.Property(e => e.Birthday)
              .HasConversion(
                  v => v.ToDateTime(TimeOnly.MinValue),  // DateOnly -> DateTime
                  v => DateOnly.FromDateTime(v)          // DateTime -> DateOnly
              )
              .IsRequired();

        entity.Property(e => e.UserEmail).IsRequired().HasMaxLength(255);

        // One (User) To Many (UserContact) Via (UserUUID)
        entity.HasMany(e => e.UserContacts)
              .WithOne(r => r.User)
              .HasForeignKey(r => r.UserUUID)
              .OnDelete(DeleteBehavior.Cascade);

        base.Configure(entity);
    }
}

public partial class User : BaseEntity
{
    // Entity Properties
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserEmail { get; set; }
    public DateOnly Birthday { get; set; }

    // Foreign Relation
    public ICollection<UserContact> UserContacts { get; set; }
}

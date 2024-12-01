using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETag.Delta
{
    public partial class UserContactConfiguration : BaseEntityConfiguration<UserContact>
    {
        public override void Configure(EntityTypeBuilder<UserContact> entity)
        {
            entity.ToTable(nameof(UserContact));
            entity.Property(e => e.UserName).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.UserEmail).IsRequired().HasMaxLength(255);

            // Many (UserContact) To One (User) Via (UserUUID)
            entity.HasOne(r => r.User)
                  .WithMany(d => d.UserContacts)
                  .HasForeignKey(r => r.UserUUID);

            base.Configure(entity);
        }
    }

    public partial class UserContact : BaseEntity
    {
        // Entity Properties
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserEmail { get; set; }

        // Foreign Relation
        public User User { get; set; }
        public Guid UserUUID { get; set; }
    }
}

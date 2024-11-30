using Microsoft.EntityFrameworkCore;

namespace Tryout.ETag.Delta.EFContext
{
    public partial class EF_Context : DbContext
    {
        public virtual DbSet<DummyEntity> User { get; set; }

        public EF_Context(DbContextOptions<EF_Context> options) : base(options)
        {
        }

        protected EF_Context(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Turkish_CI_AS");
            modelBuilder.ApplyConfiguration(new DummyEntityConfiguration());
        }
    }
}

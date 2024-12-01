using Microsoft.EntityFrameworkCore;

namespace ETag.Delta
{
    public partial class EF_Context : DbContext
    {
        public virtual DbSet<DummyEntity> DummyEntity { get; set; }
        public DbSet<DummyEntityRelation> DummyEntityRelations { get; set; }

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
            modelBuilder.ApplyConfiguration(new DummyEntityRelationConfiguration());

            DataSeeder.Seed(modelBuilder);
        }
    }
}

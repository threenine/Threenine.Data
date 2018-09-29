using Microsoft.EntityFrameworkCore;

namespace TestDatabase
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
        }

        public virtual DbSet<TestCategory> TestCategories { get; set; }
        public virtual DbSet<TestProduct> TestProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestCategory>(entity =>
            {
                entity.ToTable("TestCategory");
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<TestProduct>(entity =>
            {
                entity.ToTable("TestProduct");
                entity.HasIndex(e => e.CategoryId).HasName("testCategory_testCategory_id_foreign");
                entity.Property(e => e.CategoryId).HasColumnName("CategoryId");
                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("testCategory_testCategory_id_foreign");
            });
        }
    }
}
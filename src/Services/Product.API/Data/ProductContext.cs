using Contracts.Domains.Interfaces;
using Microsoft.EntityFrameworkCore;
using Product.API.Models;

namespace Product.API.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        public DbSet<Catalog> Catalogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Catalog>(b =>
            {
                b.HasKey(v => v.Id);
                b.Property(v => v.Id).ValueGeneratedOnAdd();
                b.ToTable("Catalogs_Catalog");
            });

            ProductSeedData.SeedData(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductContext).Assembly);

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted);

            foreach (var entry in modifiedEntries)
            {
                switch(entry.State)
                {
                    case EntityState.Added:
                        if (entry.Entity is IDateTracking addedEntity)
                        {
                            addedEntity.CreatedAt = DateTime.UtcNow;
                            addedEntity.LastModifiedAt = DateTime.UtcNow;
                        }
                        break;
                    case EntityState.Modified:
                        Entry(entry.Entity).Property("Id").IsModified = false;
                        if (entry.Entity is IDateTracking modifiedEntity)
                        {
                            modifiedEntity.LastModifiedAt = DateTime.UtcNow;
                            entry.State = EntityState.Modified;
                        }
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

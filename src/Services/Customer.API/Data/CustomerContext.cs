using Contracts.Domains.Interfaces;
using Microsoft.EntityFrameworkCore;
using Customer.API.Models;

namespace Customer.API.Data
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {
        }

        public DbSet<Models.Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.Customer>(b =>
            {
                b.HasKey(v => v.Id);
                b.Property(v => v.Id).ValueGeneratedOnAdd();
                b.HasIndex(v => v.UserName);
                b.HasIndex(v => v.Email).IsUnique();
                b.ToTable("Customers_Customer");
            });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerContext).Assembly);

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

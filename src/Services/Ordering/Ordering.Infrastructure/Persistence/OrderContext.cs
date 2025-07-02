using Contracts.Domains.Interfaces;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using Ordering.Domain.Enums;
using System.Data;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasIndex(r => r.UserName).IsUnique();

            modelBuilder.Entity<Order>()
                .HasIndex(r => r.EmailAddress).IsUnique();

            modelBuilder.Entity<Order>()
                .Property(r => r.Status).HasDefaultValue(EOrderStatus.New).IsRequired();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted);

            foreach (var entry in modifiedEntries)
            {
                switch (entry.State)
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

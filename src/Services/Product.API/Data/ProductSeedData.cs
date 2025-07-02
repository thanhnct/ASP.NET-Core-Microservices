using Microsoft.EntityFrameworkCore;
using Product.API.Models;

namespace Product.API.Data
{
    public static class ProductSeedData
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catalog>().HasData(Catalogs);
        }

        private static IEnumerable<Catalog> Catalogs => new List<Catalog>
        {
            new Catalog
            {
                Id = 1,
                No = "P0001",
                Name = "Product 1",
                Summary = "Product 1 Summary",
                Description = "Product 1 Description",
                Price = 100
            },
            new Catalog
            {
                Id = 2,
                No = "P0002",
                Name = "Product 2",
                Summary = "Product 2 Summary",
                Description = "Product 2 Description",
                Price = 250
            },
            new Catalog
            {
                Id = 3,
                No = "P0003",
                Name = "Product 3",
                Summary = "Product 3 Summary",
                Description = "Product 3 Description",
                Price = 400
            }
        };
    }
}

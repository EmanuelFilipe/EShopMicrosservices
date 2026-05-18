using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync(cancellation))
                return;

            // Marten UPSERT will cater for existing records
            session.Store<Product>(GetPreconfiguredProducts());
            await session.SaveChangesAsync(cancellation);
        }

        private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>
        {
            new()
            {
                    Id = Guid.NewGuid(),
                    Name = "Iphone 12",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Price = 999.99m,
                    Category = new List<string> { "Smartphones", "Electronics" }
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Samsung Galaxy S21",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Price = 899.99m,
                Category = new List<string> { "Smartphones", "Electronics" }
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Google Pixel 5",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Price = 699.99m,
                Category = new List<string> { "Smartphones", "Electronics" }
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "OnePlus 9",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Price = 729.99m,
                Category = new List<string> { "Smartphones", "Electronics" }
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Sony WH-1000XM4",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Price = 349.99m,
                Category = new List<string> { "Headphones", "Electronics" }
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Bose QuietComfort 35 II",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Price = 299.99m,
                Category = new List<string> { "Headphones", "Electronics" }
            }
        };
    }
}

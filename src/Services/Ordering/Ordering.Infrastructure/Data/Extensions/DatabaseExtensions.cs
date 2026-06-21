using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Apply any pending migrations to the database
        dbContext.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedDataAsync(dbContext);
    }

    private static async Task SeedDataAsync(ApplicationDbContext dbContext)
    {
        await SeedCustomerAsync(dbContext);
        await SeedProductAsync(dbContext);
        await SeedOrderAndItemsAsync(dbContext);
        
        // Check if the database is empty and seed initial data if necessary
        //if (!dbContext.Products.Any())
        //{
        //    var products = new List<Product>
        //    {
        //        Product.Create(ProductId.CreateUnique(), "Product 1", 10.99m),
        //        Product.Create(ProductId.CreateUnique(), "Product 2", 19.99m),
        //        Product.Create(ProductId.CreateUnique(), "Product 3", 5.99m)
        //    };
        //    dbContext.Products.AddRange(products);
        //    await dbContext.SaveChangesAsync();
        //}
    }

    private static async Task SeedCustomerAsync(ApplicationDbContext dbContext)
    {
        if (!await dbContext.Customers.AnyAsync())
        {
            await dbContext.Customers.AddRangeAsync(InitialData.Customers);
            await dbContext.SaveChangesAsync();
        }
    }

    private static async Task SeedProductAsync(ApplicationDbContext dbContext)
    {
        if (!await dbContext.Products.AnyAsync())
        {
            await dbContext.Products.AddRangeAsync(InitialData.Products);
            await dbContext.SaveChangesAsync();
        }
    }

    private static async Task SeedOrderAndItemsAsync(ApplicationDbContext dbContext)
    {
        if (!await dbContext.Orders.AnyAsync())
        {
            await dbContext.Orders.AddRangeAsync(InitialData.OrdersWithItems);
            await dbContext.SaveChangesAsync();
        }
    }
}

using SkiNet.Domain.Entities;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;

namespace SkiNet.Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var basePath = Directory.GetCurrentDirectory(); // Get the base path of the application
                var brandsFilePath = Path.Combine(basePath, "Infrastructure", "Data", "SeedData", "brands.json");
                var typesFilePath = Path.Combine(basePath, "Infrastructure", "Data", "SeedData", "types.json");
                var productsFilePath = Path.Combine(basePath, "Infrastructure", "Data", "SeedData", "products.json");

                // Seed ProductBrands
                if (!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText(brandsFilePath);
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (var item in brands)
                    {
                        context.ProductBrands.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                // Seed ProductTypes
                if (!context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText(typesFilePath);
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var item in types)
                    {
                        context.ProductTypes.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                // Seed Products
                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText(productsFilePath);
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var product in products)
                    {
                        context.Products.Add(product);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }
    }
}

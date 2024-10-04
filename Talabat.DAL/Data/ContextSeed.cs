using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace Talabat
{
    public class ContextSeed
    {
        public static async Task SeedAsync(StoreContext context,ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Brands.Any())
                {
                    var brandsData = File.ReadAllText("../Talabat.DAL/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (var brand in brands!)
                    {
                        context.Set<ProductBrand>().Add(brand);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.Types.Any())
                {
                    var TypesData = File.ReadAllText("../Talabat.DAL/Data/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductBrand>>(TypesData);

                    foreach (var type in types!)
                    {
                        context.Set<ProductBrand>().Add(type);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var ProductsData = File.ReadAllText("../Talabat.DAL/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<ProductBrand>>(ProductsData);

                    foreach (var product in products!)
                    {
                        context.Set<ProductBrand>().Add(product);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.DeliveryMethods.Any())
                {
                    var deliveryData = File.ReadAllText("../Talabat.DAL/Data/SeedData/delivery.json");
                    var deliveries = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);

                    foreach (var delivery in deliveries!)
                    {
                        context.Set<DeliveryMethod>().Add(delivery);
                    }
                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ContextSeed>();
                logger.LogError(ex, ex.Message);
            }
        }
    }
}
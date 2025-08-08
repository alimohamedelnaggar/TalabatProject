using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order;

namespace Talabat.Repository.Data.Contexts
{
    public static class SeedDataContext
    {
        public async static Task SeedAsync(TalabatDbContext dbContext)
        {
            if (dbContext.ProductBrands.Count() == 0)
            {

                var brandData = File.ReadAllText("E:\\MyProjects\\TalabatProject\\Talabat.Repository\\Data\\DataSeed\\brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                if (brands?.Count > 0)
                {
                    await dbContext.ProductBrands.AddRangeAsync(brands);

                    await dbContext.SaveChangesAsync();
                }
            }
            if (dbContext.ProductCategories.Count() == 0)
            {

                var typesData = File.ReadAllText("E:\\MyProjects\\TalabatProject\\Talabat.Repository\\Data\\DataSeed\\types.json");
                var Types = JsonSerializer.Deserialize<List<ProductCategory>>(typesData);
                if (Types?.Count > 0)
                {
                    await dbContext.ProductCategories.AddRangeAsync(Types);

                    await dbContext.SaveChangesAsync();
                }
            }
            if (dbContext.Products.Count() == 0)
            {

                var productsData = File.ReadAllText("E:\\MyProjects\\TalabatProject\\Talabat.Repository\\Data\\DataSeed\\products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products?.Count > 0)
                {
                    await dbContext.Products.AddRangeAsync(products);

                    await dbContext.SaveChangesAsync();
                }
            }
            if (dbContext.DeliveryMethods.Count() == 0)
            {

                var deliveryMethods = File.ReadAllText("E:\\MyProjects\\TalabatProject\\Talabat.Repository\\Data\\DataSeed\\delivery.json");
                var delivery = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethods);
                if (delivery?.Count > 0)
                {
                    await dbContext.DeliveryMethods.AddRangeAsync(delivery);

                    await dbContext.SaveChangesAsync();
                }
            }
        }

    }
}

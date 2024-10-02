using Store.G04.Core.Entities;
using Store.G04.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.G04.Repository.Data
{
    public class StoreDbContextSeed
    {
        public static async Task SeedAsync(StoreDbContext context)
        {
            ///Brand
            if (context.ProductBrands.Count() == 0)
            {
                var brandsData = File.ReadAllText(@"../Store.G04.Repository/Data/DataSeed/brands.json");
                var brand = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brand is not null && brand.Count() > 0)
                {
                    await context.ProductBrands.AddRangeAsync(brand);
                   

                }


            }
            if (context.ProductTypes.Count() == 0)
            {
                var typesData = File.ReadAllText(@"../Store.G04.Repository/Data/DataSeed/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                if (types is not null && types.Count() > 0)
                {
                    await context.ProductTypes.AddRangeAsync(types);
                   

                }


            }
            if (context.Products.Count() == 0)
            {
                var productsData = File.ReadAllText(@"../Store.G04.Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products is not null && products.Count() > 0)
                {
                    await context.Products.AddRangeAsync(products);
                  

                }


            }

            await context.SaveChangesAsync();

        }




    }
}

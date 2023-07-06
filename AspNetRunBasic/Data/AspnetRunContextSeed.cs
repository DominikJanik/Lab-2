using AspNetRunBasic.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetRunBasic.Data
{
    public class AspnetRunContextSeed
    {
        public static async Task SeedAsync(AspnetRunContext aspnetrunContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {

                if (!aspnetrunContext.Categories.Any())
                {
                    aspnetrunContext.Categories.AddRange(GetPreconfiguredCategories());
                    await aspnetrunContext.SaveChangesAsync();
                }

                if (!aspnetrunContext.Products.Any())
                {
                    aspnetrunContext.Products.AddRange(GetPreconfiguredProducts());
                    await aspnetrunContext.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<AspnetRunContextSeed>();
                    log.LogError(exception.Message);
                    await SeedAsync(aspnetrunContext, loggerFactory, retryForAvailability);
                }
                throw;
            }
        }

        private static IEnumerable<Category> GetPreconfiguredCategories()
        {
            return new List<Category>()
            {
                new Category() { Name = "Nabiał", Description = "Nabiał" },
                new Category() { Name = "Mięso", Description = "Mięso" },
                new Category() { Name = "Warzywa", Description = "Warzywa" },
                new Category() { Name = "Owoce", Description = "Owoce" },
                new Category() { Name = "Pieczywo", Description = "Pieczywo" },
                new Category() { Name = "Inne", Description = "Wszystkie inne produkty" }

            };
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new Product() { Name = "Ser", Description = "Mozzarella i chedar", CategoryId = 1 },
                new Product() { Name = "Kurczak", Description = "Tak z 2 kg", CategoryId = 2 },
                new Product() { Name = "Banan", Description = "3 sztuki", CategoryId = 4 },
                new Product() { Name = "Drukarka", Description = "Najlepiej HP", CategoryId =6 }
            };
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FruitsApp.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new FruitsDbContext(serviceProvider.GetRequiredService<DbContextOptions<FruitsDbContext>>()))
            {
                // Look for any movies.
                if (context.Fruits.Any())
                {
                    return;   // DB teble has been seeded
                }

                context.Fruits.AddRange(
                    new Fruit
                    {
                        Name = "Apple",
                        Description = "Has seeds",
                        DateAdded = DateTime.Now,
                        MarketPrice = 10,
                        FruitUpkeepDifficulty = FruitUpkeepDifficulty.Medium

                    },

                    new Fruit
                    {
                        Name = "Pear",
                        Description = "Does not have seeds",
                        DateAdded = DateTime.UtcNow,
                        MarketPrice = 15,
                        FruitUpkeepDifficulty = FruitUpkeepDifficulty.Easy

                    }
                );
                context.SaveChanges();
            }
        }
    }
}

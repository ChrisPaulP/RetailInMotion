using Microsoft.EntityFrameworkCore;
using RetailInMotion.Domain.Entities;
using RetailInMotion.Domain.Entities.Inventory;
using RetailInMotion.Domain.ValueObjects;

namespace RetailInMotion.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            
                if (!await context.Products.AnyAsync())
                {
                    await context.Products.AddRangeAsync(
                        GetPreconfiguredProducts());
                   
                    await context.SaveChangesAsync();
                }

                if (!await context.Orders.AnyAsync())
                {
                    var or = GetPreconfiguredOrders();
                    foreach (var o in or)
                    {
                        o.AddOrderItem(1, "t-shirt", 10);
                        o.AddOrderItem(2, "mug", 4);
                        o.AddOrderItem(3, "trousers", 20);
                        o.AddOrderItem(4, "jacket", 45);
                        o.AddOrderItem(5, "coat", 60);
                        context.Orders.Add(o);
                    }              
                    await context.SaveChangesAsync();
                }
        }

        static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>
            {
                new(100),
                new(50),
                new(200),
                new(80),
                new(40),
            };
        }
        static IEnumerable<DeliveryAddress> GetPreconfiguredDeliveryAddresses()
        {
            return new List<DeliveryAddress>
            {
                new("West Street", "Dublin", "Ireland", "A12 FBDE"),
                new("North Street", "London", "England", "A09 FBC"),
                new("South Street", "Glasgow", "Scotland", "A01 FRY"),
                new("East Street", "Cardiff", "Wales", "A12 BRE"),
                new("Upper Street", "Belfast", "Northern Ireland", "BT8 6AZ")
            };
        }

        static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new(GetPreconfiguredDeliveryAddresses().ElementAt(0)),
                new(GetPreconfiguredDeliveryAddresses().ElementAt(1)),
                new(GetPreconfiguredDeliveryAddresses().ElementAt(2)),
                new(GetPreconfiguredDeliveryAddresses().ElementAt(3)),
                new(GetPreconfiguredDeliveryAddresses().ElementAt(4)),
            };
        }
    }
}

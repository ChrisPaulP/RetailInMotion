using FluentAssertions;
using NUnit.Framework;
using RetailInMotion.Application.Orders.Commands.CreateOrder;
using RetailInMotion.Domain.Entities;
using RetailInMotion.Domain.Entities.Inventory;

namespace RetailInMotion.Application.IntegrationTests.Orders.Commands;
using static Testing;
public class CreateOrderTests : TestBase
{
   
    [Test]
    public async Task ShouldCreateOrder()
    {
        var product = new Product(20);
        await AddAsync(product);
        var command = new CreateOrderCommand
        {
            City = "Test City",
            Street = "Test Street",
            Country = "Test Country",
            PostCode = "Test PostCode",
            OrderItemProductId = product.Id,
            OrderItemName = "T-shirt",
            OrderItemPrice = 10,
            OrderItemQuantity = 10
        };

        var id = await SendAsync(command);

        var order = await FindAsync<Order>(id);

        order!.DeliveryAddress.City.Should().Be(command.City);
        order!.DeliveryAddress.Street.Should().Be(command.Street);
        order!.DeliveryAddress.Country.Should().Be(command.Country);
        order!.DeliveryAddress.PostCode.Should().Be(command.PostCode);
    }
}

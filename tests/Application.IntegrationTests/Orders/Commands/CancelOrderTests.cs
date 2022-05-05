using static RetailInMotion.Application.IntegrationTests.Testing;
using NUnit.Framework;
using RetailInMotion.Application.Orders.Commands.CreateOrder;
using RetailInMotion.Application.Orders.Commands.CancelOrder;
using RetailInMotion.Domain.Entities;
using FluentAssertions;
using RetailInMotion.Domain.Entities.Inventory;

namespace RetailInMotion.Application.IntegrationTests.Orders.Commands;

public class CancelOrderTests : TestBase
{
    [Test]
    public async Task ShouldCancelOrder()
    {
        var product = new Product(20);
        await AddAsync(product);
        var orderId = await SendAsync(new CreateOrderCommand
        {
            Street = "West Street",
            City = "Dublin",
            Country = "Ireland",
            PostCode = "A12 FBDE",
            OrderItemProductId = product.Id,
            OrderItemName = "T-shirt",
            OrderItemPrice = 10,
            OrderItemQuantity = 10
        });

        var command = new CancelOrderCommand
        {
            OrderId = orderId          
        };

        await SendAsync(command);

        var item = await FindAsync<Order>(orderId);
        item?.IsCancelled.Should().BeTrue();
    }
}
using FluentAssertions;
using NUnit.Framework;
using RetailInMotion.Application.Orders.Commands.CreateOrder;
using RetailInMotion.Application.Orders.Commands.UpdateOrderItems;
using RetailInMotion.Domain.Entities;
using RetailInMotion.Domain.Entities.Inventory;

namespace RetailInMotion.Application.IntegrationTests.Orders.Commands;
using static Testing;
public class UpdateOrderItemsTests : TestBase
{
    [Test]
    public async Task ShouldUpdateOrderItems()
    {
        var product = new Product(20);
        await AddAsync(product);
        var orderId = await SendAsync(new CreateOrderCommand()
        {
            City = "Update Items City",
            Street = "Update Items Street",
            Country = "Update Items Country",
            PostCode = "Update Items Post Code",
            OrderItemProductId = product.Id,
            OrderItemName = "T-shirt",
            OrderItemPrice = 10,
            OrderItemQuantity = 10
        });
        var order = await FindAsync<Order>(orderId);

        var command = new UpdateOrderItemsCommand
        {
            OrderId = order.Id,
            ProductId = product.Id,
            Quantity = 6
        };

        var UpdatedOrderItemId = await SendAsync(command);
        var existingOrderItem = await FindAsync<OrderItem>(UpdatedOrderItemId);
        existingOrderItem!.GetQuantity().Should().Be(command.Quantity);
    }
}
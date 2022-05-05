using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using RetailInMotion.Application.Orders.Commands.CreateOrder;
using RetailInMotion.Application.TodoLists.Commands.UpdateOrderDeliveryAddress;
using RetailInMotion.Domain.Entities;
using RetailInMotion.Domain.Entities.Inventory;
using static RetailInMotion.Application.IntegrationTests.Testing;

namespace RetailInMotion.Application.IntegrationTests.Orders.Commands;

public class UpdateOrderAddressTests : TestBase
{
    [Test]
    public async Task ShouldUpdateOrderAddress()
    {
        var product = new Product(100);
        await AddAsync(product);
        var order = await SendAsync(new CreateOrderCommand
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

        var command = new UpdateOrderDeliveryAddressCommand
        {
            OrderId = order,
            Street = "New 1",
            City = "New 2",
            Country = "New 3",
            PostCode = "New 4"
        };

        await SendAsync(command);

        var updatedOrder = await FindAsync<Order>(order);

        updatedOrder.Should().NotBeNull();
        updatedOrder!.DeliveryAddress.Street.Should().Be(command.Street);
        updatedOrder!.DeliveryAddress.Street.Should().Be(command.Street);
        updatedOrder!.DeliveryAddress.Country.Should().Be(command.Country);
        updatedOrder!.DeliveryAddress.PostCode.Should().Be(command.PostCode);
    }
}

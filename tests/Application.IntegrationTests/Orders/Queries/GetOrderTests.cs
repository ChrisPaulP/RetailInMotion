using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using RetailInMotion.Application.Orders.Queries.GetOrder;
using RetailInMotion.Domain.Entities;
using RetailInMotion.Domain.ValueObjects;

namespace RetailInMotion.Application.IntegrationTests.Orders.Queries;
using static Testing;
public class GetOrderTests : TestBase
{
    [Test]
    public async Task ShouldReturnAllOrders()
    {
        var deliveryAddress = new DeliveryAddress("Update Items Street", "Update Items City", "Update Items Country", "Update Items Post Code");
        var order = new Order(deliveryAddress);
        await AddAsync(order);

        var query = new GetOrderQuery()
        {
            OrderId = order.Id
        };

        var result = await SendAsync(query);

        result.Id.Should().Be(query.OrderId);
    }
}

using FluentAssertions;
using NUnit.Framework;
using RetailInMotion.Application.Orders.Queries.GetOrdersWithPagination;
using RetailInMotion.Domain.Entities;
using RetailInMotion.Domain.ValueObjects;

namespace RetailInMotion.Application.IntegrationTests.Orders.Queries;
using static Testing;
public class GetOrdersWithPaginationTests : TestBase
{
    [Test]
    public async Task ShouldReturnAllOrders()
    {
        var deliveryAddress = new DeliveryAddress("Update Items Street", "Update Items City", "Update Items Country", "Update Items Post Code");
        await AddAsync(new Order(deliveryAddress));

        var deliveryAddressTwo = new DeliveryAddress("Street", "City", "Country", "Post Code");
        await AddAsync(new Order(deliveryAddressTwo));

        var query = new GetOrdersWithPaginationQuery();

        var result = await SendAsync(query);

        result.Items.Should().HaveCount(2);
    }
}

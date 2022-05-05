using Microsoft.AspNetCore.Mvc;
using RetailInMotion.Application.Common.Models;
using RetailInMotion.Application.Orders.Commands.CancelOrder;
using RetailInMotion.Application.Orders.Commands.CreateOrder;
using RetailInMotion.Application.Orders.Commands.UpdateOrderItems;
using RetailInMotion.Application.Orders.Queries.GetOrder;
using RetailInMotion.Application.Orders.Queries.GetOrdersWithPagination;
using RetailInMotion.Application.TodoLists.Commands.UpdateOrderDeliveryAddress;


namespace RetailInMotion.WebAPI.Controllers;

public class OrderController : ApiControllerBase
{
    // GET: api/Order/1
    [HttpGet("{id}", Name ="Get")]
    public async Task<Application.Orders.Queries.GetOrder.OrderDto> Get(int id)
    {
        return await Mediator.Send(new GetOrderQuery { OrderId = id });
    }

    //// GET: api/Order/
    [HttpGet]
    public async Task<ActionResult<PaginatedList<Application.Orders.Queries.GetOrdersWithPagination.OrderDto>>> GetOrdersWithPagination([FromQuery] GetOrdersWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    // POST: api/Order
    [HttpPost]
    [Produces("application/json")]
    public async Task<ActionResult<int>> Create(CreateOrderCommand command)
    {
        return await Mediator.Send(command);
    }

    // PUT: api/Order/5
    [HttpPut("UpdateOrderAddress/{id}", Name = "UpdateOrderAddress")]
    public async Task<ActionResult> UpdateOrderAddress(int id, UpdateOrderDeliveryAddressCommand command)
    {
        if (id != command.OrderId)
        {
            return BadRequest();
        }
        await Mediator.Send(command);

        return NoContent();
    }

    //[Route("UpdateOrderItems")]
    [HttpPut("UpdateOrderItems/{id}", Name = "UpdateOrderItems")]
    public async Task<ActionResult> UpdateOrderItems(int id, UpdateOrderItemsCommand command)
    {
        if (id != command.OrderId)
        {
            return BadRequest();
        }
        await Mediator.Send(command);

        return NoContent();
    }

    // DELETE: api/Order/5
    [HttpPut("CancelOrder/{id}")]
    public async Task<ActionResult> CancelOrder(int id)
    {
        await Mediator.Send(new CancelOrderCommand { OrderId = id });

        return NoContent();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using RetailInMotion.Application.Common.Interfaces;
using RetailInMotion.Application.Common.Models;
using RetailInMotion.Domain.Entities.Inventory;
using RetailInMotion.Domain.Events;

namespace RetailInMotion.Application.Orders.EventHandlers;

public class OrderCreatedDomainEventHandler : INotificationHandler<DomainEventNotification<OrderCreatedDomainEvent>>
{
    private readonly ILogger<OrderCreatedDomainEventHandler> _logger;
    private readonly IApplicationDbContext _context;

    public OrderCreatedDomainEventHandler(IApplicationDbContext context, ILogger<OrderCreatedDomainEventHandler> logger)
    {
        _logger = logger;
        _context = context;
    }

    public async Task Handle(DomainEventNotification<OrderCreatedDomainEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;
        
        foreach (var orderItem in notification.DomainEvent.Order.OrderItems)
        {
            var product = _context.Products
                .Where(x => x.Id == orderItem.ProductId).FirstOrDefault();
            product.RemoveUnits(orderItem.GetQuantity());
        }
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("RetailInMotion Domain Event: {DomainEvent}", domainEvent.GetType().Name);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using RetailInMotion.Application.Common.Interfaces;
using RetailInMotion.Application.Common.Models;
using RetailInMotion.Domain.Events;

namespace RetailInMotion.Application.Orders.EventHandlers;

public class OrderCancelledDomainEventHandler : INotificationHandler<DomainEventNotification<OrderCancelledDomainEvent>>
{
    private readonly ILogger<OrderCancelledDomainEventHandler> _logger;
    private readonly IApplicationDbContext _context;

    public OrderCancelledDomainEventHandler(IApplicationDbContext context, ILogger<OrderCancelledDomainEventHandler> logger)
    {
        _logger = logger;
        _context = context;
    }

    public async Task Handle(DomainEventNotification<OrderCancelledDomainEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        foreach (var orderItem in notification.DomainEvent.Order.OrderItems)
        {
            var product = _context.Products
                .Where(x => x.Id == orderItem.ProductId).FirstOrDefault();
            product.AddUnits(orderItem.GetQuantity());
        }
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("RetailInMotion Domain Event: {DomainEvent}", domainEvent.GetType().Name);

    }
}

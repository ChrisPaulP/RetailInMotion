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

public class OrderItemAlteredDomainEventHandler : INotificationHandler<DomainEventNotification<OrderItemAlteredDomainEvent>>
{
    private readonly ILogger<OrderItemAlteredDomainEventHandler> _logger;
    private readonly IApplicationDbContext _context;

    public OrderItemAlteredDomainEventHandler(IApplicationDbContext context, ILogger<OrderItemAlteredDomainEventHandler> logger)
    {
        _logger = logger;
        _context = context;
    }

    public async Task Handle(DomainEventNotification<OrderItemAlteredDomainEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

       
            var product = _context.Products
                .Where(x => x.Id == domainEvent.ProductId).FirstOrDefault();
            product.AddUnits(domainEvent.Quantity);
        
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("RetailInMotion Domain Event: {DomainEvent}", domainEvent.GetType().Name);

    }
}


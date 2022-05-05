using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailInMotion.Application.Common.Exceptions;
using RetailInMotion.Application.Common.Interfaces;
using RetailInMotion.Domain.Entities;

namespace RetailInMotion.Application.Orders.Commands.CancelOrder
{
    public class CancelOrderCommand : IRequest
    {
        public int OrderId { get; set; }
    }

    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
    {
        private readonly IApplicationDbContext _context;

        public CancelOrderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.Include(oi => oi.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);

            if (order == null)
                throw new NotFoundException(nameof(Order), request.OrderId);

            order.CancelOrder();
            
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
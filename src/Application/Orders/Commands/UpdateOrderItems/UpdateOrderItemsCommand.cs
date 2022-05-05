using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailInMotion.Application.Common.Exceptions;
using RetailInMotion.Application.Common.Interfaces;
using RetailInMotion.Domain.Entities;

namespace RetailInMotion.Application.Orders.Commands.UpdateOrderItems
{
    public class UpdateOrderItemsCommand : IRequest<int>
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        //public decimal ItemPrice { get; set; }
    }

    public class UpdateOrderItemsCommandHandler : IRequestHandler<UpdateOrderItemsCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public UpdateOrderItemsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateOrderItemsCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.Include(oi => oi.OrderItems).FirstOrDefaultAsync(p => p.Id == request.OrderId, cancellationToken);
            if (order == null)
                throw new NotFoundException(nameof(Order), request.OrderId);

            var existingOrderItem =  order.OrderItems.Where(oi => oi.ProductId == request.ProductId).FirstOrDefault();
            if (existingOrderItem == null)
                throw new NotFoundException(nameof(OrderItem), existingOrderItem.Id);

            order.UpdateOrderItem(existingOrderItem, request.Quantity);

            await _context.SaveChangesAsync(cancellationToken);

            return existingOrderItem.Id;
        }
    }
}
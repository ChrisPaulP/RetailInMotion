using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailInMotion.Application.Common.Exceptions;
using RetailInMotion.Application.Common.Interfaces;
using RetailInMotion.Domain.Entities;

namespace RetailInMotion.Application.TodoLists.Commands.UpdateOrderDeliveryAddress
{ 
        public class UpdateOrderDeliveryAddressCommand : IRequest
    {
        public int OrderId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
    }

    public class UpdateOrderDeliveryAddressCommandHandler : IRequestHandler<UpdateOrderDeliveryAddressCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateOrderDeliveryAddressCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateOrderDeliveryAddressCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(p => p.Id == request.OrderId, cancellationToken);
            if (order == null)
                throw new NotFoundException(nameof(Order), request.OrderId);

            order.UpdateOrderDeliveryAddress(request.Street, request.City, request.Country, request.PostCode);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

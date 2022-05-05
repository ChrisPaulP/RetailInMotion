using MediatR;
using RetailInMotion.Application.Common.Exceptions;
using RetailInMotion.Application.Common.Interfaces;
using RetailInMotion.Domain.Entities;
using RetailInMotion.Domain.ValueObjects;

namespace RetailInMotion.Application.Orders.Commands.CreateOrder;

    public class CreateOrderCommand : IRequest<int>
    {
        private readonly List<OrderItemDTO> _orderItems;
        public string Street { get;  set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostCode { get;  set; }
        public int OrderItemProductId { get; set; }
        public string OrderItemName { get; set; }
        public decimal OrderItemPrice { get; set; }
        public int OrderItemQuantity { get; set; }
        public IEnumerable<OrderItemDTO> OrderItems => _orderItems;
        public CreateOrderCommand()
        {
            _orderItems = new List<OrderItemDTO>();
        }
}
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateOrderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateOrderCommand request,CancellationToken cancellationToken)
        {
            
            var deliveryAddress = new DeliveryAddress(request.Street, request.City ,request.Country, request.PostCode);
            var order = new Order(deliveryAddress);
            if (order == null)
                throw new NotFoundException();

            order.AddOrderItem(request.OrderItemProductId, request.OrderItemName, request.OrderItemPrice, request.OrderItemQuantity);          
            _context.Orders.Add(order);

            await _context.SaveChangesAsync(cancellationToken);

            return order.Id;
        }
    }

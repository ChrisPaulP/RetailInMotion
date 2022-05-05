using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailInMotion.Application.Common.Exceptions;
using RetailInMotion.Application.Common.Interfaces;
using RetailInMotion.Domain.Entities;

namespace RetailInMotion.Application.Orders.Queries.GetOrder
{
    public class GetOrderQuery : IRequest<OrderDto>
    {
        public int OrderId { get; set; }
    }

    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetOrderQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.AsNoTracking()
               .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
               .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);

            if (order == null)
                throw new NotFoundException(nameof(Order), request.OrderId);

            return order;
        }
    }
}
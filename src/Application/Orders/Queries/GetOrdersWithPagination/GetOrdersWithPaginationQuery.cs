using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using RetailInMotion.Application.Common.Interfaces;
using RetailInMotion.Application.Common.Mappings;
using RetailInMotion.Application.Common.Models;

namespace RetailInMotion.Application.Orders.Queries.GetOrdersWithPagination
{
    public class GetOrdersWithPaginationQuery : IRequest<PaginatedList<OrderDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetOrdersWithPaginationQueryHandler : IRequestHandler<GetOrdersWithPaginationQuery, PaginatedList<OrderDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetOrdersWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<OrderDto>> Handle(GetOrdersWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Orders
                .OrderBy(x => x.Id)
                .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
using AutoMapper;
using RetailInMotion.Application.Common.Mappings;
using RetailInMotion.Domain.Entities;

namespace RetailInMotion.Application.Orders.Queries.GetOrder
{
    public class OrderDto : IMapFrom<Order>
    {
        public int Id { get; set; }
        public string Street { get; private set; }

        public string City { get; private set; }

        public string Country { get; private set; }

        public string PostCode { get; private set; }
        private readonly List<OrderItemDto> _orderItems;
        public IEnumerable<OrderItemDto> OrderItems => _orderItems;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderDto>()
                .ForMember(dto => dto.Street, opt => opt.MapFrom(p => p.DeliveryAddress.Street))
                .ForMember(dto => dto.City, opt => opt.MapFrom(p => p.DeliveryAddress.City))
                .ForMember(dto => dto.Country, opt => opt.MapFrom(p => p.DeliveryAddress.Country))
                .ForMember(dto => dto.PostCode, opt => opt.MapFrom(p => p.DeliveryAddress.PostCode))
                .ForMember(dto => dto.OrderItems, opt => opt.MapFrom(p => p.OrderItems.ToList()));
        }
    }

}
using AutoMapper;
using RetailInMotion.Application.Common.Mappings;
using RetailInMotion.Domain.Entities;

namespace RetailInMotion.Application.Orders.Queries.GetOrder
{
    public class OrderItemDto : IMapFrom<OrderItem>
    {
        public int ProductId { get; init; }

        public string ProductName { get; init; }

        public decimal ItemPrice { get; init; }

        public int Quantity { get; init; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dto => dto.ProductName, opt => opt.MapFrom(p => p.GetOrderItemProductName()))
                .ForMember(dto => dto.ItemPrice, opt => opt.MapFrom(p => p.GetOrderItemPrice()));
        }
    }
}
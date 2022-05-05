using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RetailInMotion.Application.Common.Mappings;
using RetailInMotion.Application.Orders.Queries.GetOrder;
using RetailInMotion.Domain.Entities;

namespace RetailInMotion.Application.Orders.Commands.CreateOrder;

public record OrderItemDTO : IMapFrom<OrderItem>
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
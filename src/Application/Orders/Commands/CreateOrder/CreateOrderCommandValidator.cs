using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RetailInMotion.Application.Common.Interfaces;
using RetailInMotion.Application.Orders.Commands.CreateOrder;

namespace RetailInMotion.Application.TodoLists.Commands.Orders
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(command => command.City).NotEmpty().WithMessage("Please enter a City.");
            RuleFor(command => command.Street).NotEmpty().WithMessage("Please enter a Street.");
            RuleFor(command => command.PostCode).NotEmpty().WithMessage("Please enter a Post Code.");
            RuleFor(command => command.Country).NotEmpty().WithMessage("Please enter a Country.");
            RuleFor(command => command.OrderItemProductId).NotEmpty().WithMessage("No order item productId found");
            RuleFor(command => command.OrderItemName).NotEmpty().WithMessage("Please enter a order item name.");
            RuleFor(command => command.OrderItemPrice).NotEmpty().WithMessage("Please enter a order item price.");
            RuleFor(command => command.OrderItemQuantity).NotEmpty().WithMessage("Please enter a order item quantity.");
        }
    }
}
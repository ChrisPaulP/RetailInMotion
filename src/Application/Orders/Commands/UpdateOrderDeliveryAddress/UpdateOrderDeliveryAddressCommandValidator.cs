using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using RetailInMotion.Application.Common.Interfaces;
using RetailInMotion.Application.TodoLists.Commands.UpdateOrderDeliveryAddress;

namespace RetailInMotion.Application.Orders.Commands.UpdateOrderDeliveryAddress;

public class UpdateOrderDeliveryAddressCommandValidator : AbstractValidator<UpdateOrderDeliveryAddressCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdateOrderDeliveryAddressCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(command => command.City).NotEmpty().WithMessage("Please enter a City.");
        RuleFor(command => command.Street).NotEmpty().WithMessage("Please enter a Street.");
        RuleFor(command => command.PostCode).NotEmpty().WithMessage("Please enter a Post Code.");
        RuleFor(command => command.Country).NotEmpty().WithMessage("Please enter a Country.");
    }
}

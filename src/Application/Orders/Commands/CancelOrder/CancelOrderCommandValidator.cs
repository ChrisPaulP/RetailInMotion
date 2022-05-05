using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using RetailInMotion.Application.Common.Interfaces;

namespace RetailInMotion.Application.Orders.Commands.CancelOrder;

public class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
{
    public CancelOrderCommandValidator()
    {
        RuleFor(order => order.OrderId).NotEmpty().WithMessage("No orderId found");
    }
}
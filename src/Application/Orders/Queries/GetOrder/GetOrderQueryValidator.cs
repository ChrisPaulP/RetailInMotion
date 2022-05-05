using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace RetailInMotion.Application.Orders.Queries.GetOrder;

public class GetOrderQueryValidator : AbstractValidator<GetOrderQuery>
{
    public GetOrderQueryValidator()
    {
        RuleFor(x => x.OrderId)
             .NotEmpty().WithMessage("OrderId is required.");
    }
}

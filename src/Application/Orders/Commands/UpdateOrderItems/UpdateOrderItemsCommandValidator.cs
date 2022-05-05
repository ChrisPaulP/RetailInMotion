using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RetailInMotion.Application.Common.Interfaces;
using RetailInMotion.Application.Orders.Commands.UpdateOrderItems;


namespace RetailInMotion.Application.TodoLists.Commands.Orders
{
    public class UpdateOrderItemsCommandValidator : AbstractValidator<UpdateOrderItemsCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateOrderItemsCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.OrderId)
                .NotEmpty().WithMessage("OrderId is required.");
            RuleFor(v => v.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");
            RuleFor(v => v.Quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .MustAsync(BeProductStockAvailable).WithMessage("There is no stock available for this quantity.");
        }
        public async Task<bool> BeProductStockAvailable(UpdateOrderItemsCommand model, int quantity, CancellationToken cancellationToken)
        {
            var product = _context.Products.Where(p => p.Id == model.ProductId).FirstOrDefault();
            return product?.CurrentStock() +- quantity > 0;
            
        }
    }
}
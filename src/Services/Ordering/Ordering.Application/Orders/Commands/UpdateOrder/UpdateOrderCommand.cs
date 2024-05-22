using BuildingBlocks.Abstractions;
using FluentValidation;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(OrderDto Order)
    : ICommand<UpdateOrderResult>;

public record UpdateOrderResult(bool IsSuccess);

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Order.Id).NotEmpty().WithMessage($"{nameof(UpdateOrderCommand.Order.Id)} is required.");
        RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage($"{nameof(UpdateOrderCommand.Order.OrderName)} is required.");
        RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage($"{nameof(UpdateOrderCommand.Order.CustomerId)} is required.");
    }
}
using BuildingBlocks.Abstractions;
using FluentValidation;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order)
    : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid Id);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Order.OrderName).NotEmpty()
            .WithMessage($"{nameof(CreateOrderCommand.Order.OrderName)} is required.");
        RuleFor(x => x.Order.CustomerId).NotEmpty()
            .WithMessage($"{nameof(CreateOrderCommand.Order.CustomerId)} is required.");
        RuleFor(x => x.Order.OrderItems).NotEmpty()
            .WithMessage($"{nameof(CreateOrderCommand.Order.OrderItems)} should not be empty.");
    }
}
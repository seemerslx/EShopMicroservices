using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Application.Exceptions;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler(IApplicationDbContext context)
    : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
         // Update order entity from command object
         // save to database
         // return result

         var orderId = OrderId.Of(command.Order.Id);

         //var orderTest = await context.Orders.SingleOrDefaultAsync(order => order.Id == orderId);

         var order = await context.Orders
             .FindAsync(orderId, cancellationToken); // todo ALEX: check how this will work

         if (order is null)
         {
             throw new OrderNotFoundException(command.Order.Id);
         }

         UpdateOrderWithNewValues(order, command.Order);

         context.Orders.Update(order);

         await context.SaveChangesAsync(cancellationToken);

         return new UpdateOrderResult(true);
    }

    public void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
    {
        var updatedShippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName,
            orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine,
            orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);

        var updatedBillingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName,
            orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine,
            orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);

        var updatedPayment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber,
            orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod);

        order.Update(
            orderName: OrderName.Of(orderDto.OrderName),
            shippingAddress: updatedShippingAddress,
            billingAddress: updatedBillingAddress, 
            payment: updatedPayment,
            status: orderDto.Status);
    }
}
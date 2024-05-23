using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints.Orders;

public record DeleteOrderResponse(bool IsSuccess);

public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{orderId}", async (Guid orderId, ISender sender) =>
            {
                var command = new DeleteOrderCommand(orderId);

                var result = await sender.Send(command, CancellationToken.None);

                var response = result.Adapt<DeleteOrderResponse>();

                return Results.Ok(response);
            })
            .WithName("Delete Order")
            .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Order")
            .WithDescription("Delete Order");
    }
}
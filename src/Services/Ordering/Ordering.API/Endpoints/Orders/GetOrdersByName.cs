using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.Endpoints.Orders;

public record GetOrderByNameResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{orderName}", async (string orderName, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersByNameQuery(orderName), CancellationToken.None);

            var response = result.Adapt<GetOrderByNameResponse>();

            return Results.Ok(response);
        }).WithName("GetOrdersByName")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders By Name")
        .WithDescription("Get Orders By Name");
    }
}
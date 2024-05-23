using Ordering.Application.Orders.Queries.GetOrdersByCustomer;
using Ordering.Domain.ValueObjects;

namespace Ordering.API.Endpoints.Orders;

public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersByCustomerQuery(customerId), CancellationToken.None);

                var response = result.Adapt<GetOrdersByCustomerResponse>();

                return Results.Ok(response);
            })
            .WithName("GetOrdersByCustomer")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Orders By Customer")
            .WithDescription("Get Orders By Customer");
    }
}
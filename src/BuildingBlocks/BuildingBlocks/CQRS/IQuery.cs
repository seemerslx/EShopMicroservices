using MediatR;

namespace BuildingBlocks.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse> { }
using BuildingBlocks.Exceptions;
using Ordering.Domain.Models;

namespace Ordering.Application.Exceptions;

public class OrderNotFoundException(Guid id) : NotFoundException(nameof(Order), id);
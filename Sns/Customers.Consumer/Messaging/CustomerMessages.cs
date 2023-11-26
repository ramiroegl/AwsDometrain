using MediatR;

namespace Customers.Consumer.Messaging;

public class CustomerCreated : IRequest, ISqsMessage
{
    public required Guid Id { get; init; }
    public required string FullName { get; init; }
    public required string Email { get; init; }
    public required string Username { get; init; }
    public required DateTimeOffset DateOfBirth { get; init; }
}

public class CustomerUpdated : IRequest, ISqsMessage
{
    public required Guid Id { get; init; }
    public required string FullName { get; init; }
    public required string Email { get; init; }
    public required string Username { get; init; }
    public required DateTimeOffset DateOfBirth { get; init; }
}

public class CustomerDeleted : IRequest, ISqsMessage
{
    public required Guid Id { get; init; }
}
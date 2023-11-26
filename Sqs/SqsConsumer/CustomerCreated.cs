namespace SqsConsumer;

public class CustomerCreated
{
    public required Guid Id { get; init; }
    public required string FullName { get; init; }
    public required string Email { get; init; }
    public required string Username { get; init; }
    public required DateTimeOffset DateOfBirth { get; init; }
}
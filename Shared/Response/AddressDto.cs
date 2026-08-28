namespace Shared.Response;

public record AddressDto
{
    public string FirstName { get; init; } = null!;
    public string LasttName { get; init; } = null!;
    public string Country { get; init; } = null!;
    public string City { get; init; } = null!;
    public string Street { get; init; } = null!;
}
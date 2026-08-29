namespace Shared.Response;

public record OrderItemDto
{
    public int ProductId { get; init; }

    public string ProductName { get; init; } = null!;

    public string PictureUrl { get; init; } = null!;
    public decimal Price { get; init; }

    public int Quatity { get; init; }
}
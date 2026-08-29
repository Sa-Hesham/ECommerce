namespace Shared.Response;

public record BasketItemsResponse
{
    public int Id { get; init; }
    public string ProductName { get; init; } = null!;

    public string PictureUrl { get; init; } = null!;


    public decimal Price { get; init; }
}
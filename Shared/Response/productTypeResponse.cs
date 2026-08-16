

namespace Shared.Response;

public record productTypeResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
}

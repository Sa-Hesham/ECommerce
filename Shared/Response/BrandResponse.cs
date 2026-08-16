using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Response;

public record BrandResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
}

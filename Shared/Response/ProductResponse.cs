using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Response;

public record ProductResponse
{
    public int Id { get; init; } 
    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;
    public string PictureUrl { get; init; } = null!;

    public decimal price { get; init; }

    
    public string BrandName { get; init; } = null!; 

    public string TypeName { get; init; } = null!; 
}

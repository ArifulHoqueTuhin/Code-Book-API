using System;
using System.Collections.Generic;

namespace CodeBookAPL.Models;

public partial class FeaturedProduct
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string? Overview { get; set; }

    public string? LongDescription { get; set; }

    public decimal Price { get; set; }

    public string? Poster { get; set; }

    public string? ImageLocal { get; set; }

    public int? Rating { get; set; }

    public bool InStock { get; set; }

    public int? Size { get; set; }

    public bool BestSeller { get; set; }

    public virtual Product Product { get; set; } = null!;
}

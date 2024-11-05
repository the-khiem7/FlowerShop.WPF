using System;
using System.Collections.Generic;

namespace Shop_Flower.DAL.Entities;

public partial class FlowerInfo
{
    public int FlowerId { get; set; }

    public string FlowerName { get; set; } = null!;

    public string? FlowerDescription { get; set; }

    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }

    public int AvailableQuantity { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }
}

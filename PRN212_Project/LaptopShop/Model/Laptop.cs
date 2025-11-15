using System;
using System.Collections.Generic;

namespace LaptopShop.Model;

public partial class Laptop
{
    public int LaptopId { get; set; }

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string? Processor { get; set; }

    public string? Ram { get; set; }

    public string? Storage { get; set; }

    public string? Gpu { get; set; }

    public decimal Price { get; set; }

    public int? Stock { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<LaptopSupplier> LaptopSuppliers { get; set; } = new List<LaptopSupplier>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}

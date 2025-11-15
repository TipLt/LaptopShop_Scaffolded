using System;
using System.Collections.Generic;

namespace LaptopShop.Model;

public partial class LaptopSupplier
{
    public int LaptopId { get; set; }

    public int SupplierId { get; set; }

    public DateTime? SupplyDate { get; set; }

    public decimal? SupplyPrice { get; set; }

    public virtual Laptop Laptop { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}

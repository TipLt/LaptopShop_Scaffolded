using System;
using System.Collections.Generic;

namespace LaptopShop.Model;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string SupplierName { get; set; } = null!;

    public string? ContactPerson { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<LaptopSupplier> LaptopSuppliers { get; set; } = new List<LaptopSupplier>();
}

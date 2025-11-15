using System;
using System.Collections.Generic;

namespace LaptopShop.Model;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int? OrderId { get; set; }

    public int? LaptopId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public virtual Laptop? Laptop { get; set; }

    public virtual Order? Order { get; set; }
}

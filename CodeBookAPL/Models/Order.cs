using System;
using System.Collections.Generic;

namespace CodeBookAPL.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public string UserEmail { get; set; } = null!;

    public decimal AmountPaid { get; set; }

    public int Quantity { get; set; }

    public DateTime OrderDate { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User User { get; set; } = null!;
}

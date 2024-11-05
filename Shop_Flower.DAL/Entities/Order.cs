using System;
using System.Collections.Generic;

namespace Shop_Flower.DAL.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public string? PhoneNumber { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string DeliveryMethod { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public string AddressId { get; set; } = null!;

    public decimal? TotalPrice { get; set; }

    public virtual User? User { get; set; }
}

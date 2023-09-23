using System;
using System.Collections.Generic;

namespace SanaWebShop.Api.Models.Entities;

public partial class Order
{
    public int Id { get; set; }

    public int IdCustomer { get; set; }

    public bool Status { get; set; } = false;

    public virtual Customer IdCustomerNavigation { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
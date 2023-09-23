using System;
using System.Collections.Generic;

namespace SanaWebShop.Api.Models.Entities;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public string CodeProduct { get; set; } = null!;

    public int IdOrder { get; set; }

    public virtual Product CodeProductNavigation { get; set; } = null!;

    public virtual Order IdOrderNavigation { get; set; } = null!;
}

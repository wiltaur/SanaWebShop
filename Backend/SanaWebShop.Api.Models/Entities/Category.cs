using System;
using System.Collections.Generic;

namespace SanaWebShop.Api.Models.Entities;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<ProdCategory> ProdCategories { get; set; } = new List<ProdCategory>();
}

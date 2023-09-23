using System;
using System.Collections.Generic;

namespace SanaWebShop.Api.Models.Entities;

public partial class ProdCategory
{
    public int Id { get; set; }

    public string CodeProduct { get; set; } = null!;

    public int IdCategory { get; set; }

    public virtual Product CodeProductNavigation { get; set; } = null!;

    public virtual Category IdCategoryNavigation { get; set; } = null!;
}

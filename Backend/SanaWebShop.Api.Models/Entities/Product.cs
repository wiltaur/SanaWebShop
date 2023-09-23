namespace SanaWebShop.Api.Models.Entities;

public partial class Product
{
    public string Code { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ProdCategory> ProdCategories { get; set; } = new List<ProdCategory>();
}
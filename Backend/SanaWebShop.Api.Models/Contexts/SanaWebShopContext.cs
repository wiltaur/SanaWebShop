using Microsoft.EntityFrameworkCore;
using SanaWebShop.Api.Models.Entities;

namespace SanaWebShop.Api.Models.Contexts;

public partial class SanaWebShopContext : DbContext
{
    public SanaWebShopContext()
    {
    }

    public SanaWebShopContext(DbContextOptions<SanaWebShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<ProdCategory> ProdCategories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public void TestModelCreation(ModelBuilder model)
    {
        OnModelCreating(model);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_Categories").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_Cust_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdCustomer)
                .HasConstraintName("FK_Orders_Customer");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasIndex(e => new { e.CodeProduct, e.IdOrder }, "IX_OrderDetails").IsUnique();

            entity.Property(e => e.CodeProduct).HasMaxLength(50);

            entity.HasOne(d => d.CodeProductNavigation).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.CodeProduct)
                .HasConstraintName("FK_OrDet_Products");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.IdOrder)
                .HasConstraintName("FK_OrDet_Orders");
        });

        modelBuilder.Entity<ProdCategory>(entity =>
        {
            entity.HasIndex(e => new { e.CodeProduct, e.IdCategory }, "IX_ProdCategories").IsUnique();

            entity.Property(e => e.CodeProduct).HasMaxLength(50);

            entity.HasOne(d => d.CodeProductNavigation).WithMany(p => p.ProdCategories)
                .HasForeignKey(d => d.CodeProduct)
                .HasConstraintName("FK_ProdCat_Products");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.ProdCategories)
                .HasForeignKey(d => d.IdCategory)
                .HasConstraintName("FK_ProdCat_Categories");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
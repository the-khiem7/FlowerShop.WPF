using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Shop_Flower.DAL.Entities;

namespace Shop_Flower.DAL;

public partial class ShopContext : DbContext
{
    public ShopContext()
    {
    }

    public ShopContext(DbContextOptions<ShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<FlowerInfo> FlowerInfos { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
<<<<<<< Updated upstream
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);Database= Shop;UID=sa;PWD=123;TrustServerCertificate=True");
=======

        => optionsBuilder.UseSqlServer(GetConnectionString());


    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", true, true)
      .Build();
        var strConn = config["ConnectionStrings:DefaultConnectionStringDB"];
        return strConn;

    }
>>>>>>> Stashed changes

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__D54EE9B452456207");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<FlowerInfo>(entity =>
        {
            entity.HasKey(e => e.FlowerId).HasName("PK__Flower_I__177E0A7E46FFC00E");

            entity.ToTable("Flower_Info");

            entity.Property(e => e.FlowerId).HasColumnName("flower_id");
            entity.Property(e => e.AvailableQuantity).HasColumnName("available_quantity");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.FlowerDescription)
                .HasMaxLength(255)
                .HasColumnName("flower_description");
            entity.Property(e => e.FlowerName)
                .HasMaxLength(255)
                .HasColumnName("flower_name");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");

            entity.HasOne(d => d.Category).WithMany(p => p.FlowerInfos)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Flower_In__categ__3C69FB99");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__465962297AFE32A6");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.AddressId)
                .HasMaxLength(255)
                .HasColumnName("address_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.DeliveryMethod)
                .HasMaxLength(255)
                .HasColumnName("delivery_method");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("payment_method");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");

            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_price");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Orders__status__403A8C7D");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370F10C72F6F");

            entity.HasIndex(e => e.Username, "UQ__Users__F3DBC5720FB5A875").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

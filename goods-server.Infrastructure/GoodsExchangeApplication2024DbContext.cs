using System;
using System.Collections.Generic;
using goods_server.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace goods_server.Infrastructure;

public partial class GoodsExchangeApplication2024DbContext : DbContext
{
    public GoodsExchangeApplication2024DbContext()
    {
    }

    public GoodsExchangeApplication2024DbContext(DbContextOptions<GoodsExchangeApplication2024DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<ReplyComment> ReplyComments { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<RequestHistory> RequestHistories { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = GetConnectionString();
        optionsBuilder.UseSqlServer(connectionString);
    }

    public string GetConnectionString()
    {
        string connectionString;
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        connectionString = config.GetConnectionString("PhucDatabase");
        return connectionString;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.Property(e => e.AvatarUrl).HasMaxLength(50);
            entity.Property(e => e.DenyRes).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.JoinDate).HasColumnType("datetime");
            entity.Property(e => e.PasswordHash).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Accounts_Roles");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("City");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.Property(e => e.Descript).HasMaxLength(50);
            entity.Property(e => e.PostDate).HasColumnType("datetime");

            entity.HasOne(d => d.Commenter).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CommenterId)
                .HasConstraintName("FK_Comments_Accounts");

            entity.HasOne(d => d.Product).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Comments_Products");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK_Type");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Orders_Accounts");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId });

            entity.ToTable("OrderDetail");

            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductName).HasMaxLength(50);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Orders");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Products");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DenyRes).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(400);
            entity.Property(e => e.ImagePro).HasMaxLength(50);
            entity.Property(e => e.IsDisplay)
                .HasMaxLength(50)
                .HasColumnName("is_Display");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Products_Category");

            entity.HasOne(d => d.City).WithMany(p => p.Products)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_Products_City");

            entity.HasOne(d => d.Genre).WithMany(p => p.Products)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK_Products_Type");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.ToTable("Rating");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Descript).HasMaxLength(50);

            entity.HasOne(d => d.Customer).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Rating_Accounts");

            entity.HasOne(d => d.Product).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Rating_Products");
        });

        modelBuilder.Entity<ReplyComment>(entity =>
        {
            entity.HasKey(e => e.ReplyId);

            entity.Property(e => e.Descript).HasMaxLength(50);
            entity.Property(e => e.PostDate).HasColumnType("datetime");

            entity.HasOne(d => d.Comment).WithMany(p => p.ReplyComments)
                .HasForeignKey(d => d.CommentId)
                .HasConstraintName("FK_ReplyComments_Comments");

            entity.HasOne(d => d.Commenter).WithMany(p => p.ReplyComments)
                .HasForeignKey(d => d.CommenterId)
                .HasConstraintName("FK_ReplyComments_Accounts");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.Property(e => e.ReportId).ValueGeneratedNever();
            entity.Property(e => e.Descript).HasMaxLength(50);
            entity.Property(e => e.PostDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Account).WithMany(p => p.Reports)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Reports_Accounts");

            entity.HasOne(d => d.Product).WithMany(p => p.Reports)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Reports_Products");
        });

        modelBuilder.Entity<RequestHistory>(entity =>
        {
            entity.ToTable("RequestHistory");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Buyer).WithMany(p => p.RequestHistoryBuyers)
                .HasForeignKey(d => d.BuyerId)
                .HasConstraintName("FK_RequestHistory_Accounts");

            entity.HasOne(d => d.ProductBuyer).WithMany(p => p.RequestHistoryProductBuyers)
                .HasForeignKey(d => d.ProductBuyerId)
                .HasConstraintName("FK_RequestHistory_Products1");

            entity.HasOne(d => d.ProductSeller).WithMany(p => p.RequestHistoryProductSellers)
                .HasForeignKey(d => d.ProductSellerId)
                .HasConstraintName("FK_RequestHistory_Products");

            entity.HasOne(d => d.Seller).WithMany(p => p.RequestHistorySellers)
                .HasForeignKey(d => d.SellerId)
                .HasConstraintName("FK_RequestHistory_Accounts1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

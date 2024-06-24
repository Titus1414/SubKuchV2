using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SubKuchV2.Models
{
    public partial class SubKuchDbContext : DbContext
    {
        public SubKuchDbContext()
        {
        }

        public SubKuchDbContext(DbContextOptions<SubKuchDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Business> Businesses { get; set; }
        public virtual DbSet<CouponCode> CouponCodes { get; set; }
        public virtual DbSet<DeliveryCharge> DeliveryCharges { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItme> OrderItmes { get; set; }
        public virtual DbSet<OrderPrice> OrderPrices { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<OtpCode> OtpCodes { get; set; }
        public virtual DbSet<Price> Prices { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCatagory> ProductCatagories { get; set; }
        public virtual DbSet<Rider> Riders { get; set; }
        public virtual DbSet<Slider> Sliders { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Userr> Userrs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-GSNAK40;Database=SubKuchDb;Trusted_Connection=True;MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Business>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CouponCode>(entity =>
            {
                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DiscountedPrice).HasColumnType("money");

                entity.Property(e => e.MinimumOrder)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ValidityDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DeliveryCharge>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.CustomerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerPhone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrderItme>(entity =>
            {
                entity.Property(e => e.Oid).HasColumnName("OId");

                entity.HasOne(d => d.OidNavigation)
                    .WithMany(p => p.OrderItmes)
                    .HasForeignKey(d => d.Oid)
                    .HasConstraintName("FK_OrderItmes_Orders");

                entity.HasOne(d => d.Pr)
                    .WithMany(p => p.OrderItmes)
                    .HasForeignKey(d => d.PrId)
                    .HasConstraintName("FK_OrderItmes_Products");
            });

            modelBuilder.Entity<OrderPrice>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DiscountPrice).HasColumnType("money");

                entity.Property(e => e.Oid).HasColumnName("OId");

                entity.Property(e => e.TotalPrice).HasColumnType("money");

                entity.HasOne(d => d.Cc)
                    .WithMany(p => p.OrderPrices)
                    .HasForeignKey(d => d.CcId)
                    .HasConstraintName("FK_OrderPrices_CouponCodes");

                entity.HasOne(d => d.Dc)
                    .WithMany(p => p.OrderPrices)
                    .HasForeignKey(d => d.DcId)
                    .HasConstraintName("FK_OrderPrices_DeliveryCharges");

                entity.HasOne(d => d.OidNavigation)
                    .WithMany(p => p.OrderPrices)
                    .HasForeignKey(d => d.Oid)
                    .HasConstraintName("FK_OrderPrices_Orders");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("OrderStatus");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Oid).HasColumnName("OId");

                entity.Property(e => e.Rid).HasColumnName("RId");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.OidNavigation)
                    .WithMany(p => p.OrderStatuses)
                    .HasForeignKey(d => d.Oid)
                    .HasConstraintName("FK_OrderStatus_Orders");

                entity.HasOne(d => d.RidNavigation)
                    .WithMany(p => p.OrderStatuses)
                    .HasForeignKey(d => d.Rid)
                    .HasConstraintName("FK_OrderStatus_Riders");
            });

            modelBuilder.Entity<Price>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.PurchasePrice).HasColumnType("money");

                entity.Property(e => e.SalePrice).HasColumnType("money");

                entity.HasOne(d => d.Pr)
                    .WithMany(p => p.Prices)
                    .HasForeignKey(d => d.PrId)
                    .HasConstraintName("FK_Prices_Products");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Pc)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.PcId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_ProductCatagories");
            });

            modelBuilder.Entity<ProductCatagory>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.SidNavigation)
                    .WithMany(p => p.ProductCatagories)
                    .HasForeignKey(d => d.Sid)
                    .HasConstraintName("FK_ProductCatagories_Stores");
            });

            modelBuilder.Entity<Rider>(entity =>
            {
                entity.Property(e => e.Cnic)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.ModelNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Vehical)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Slider>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Image)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sid).HasColumnName("SId");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Image)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.BidNavigation)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.Bid)
                    .HasConstraintName("FK_Stores_Businesses");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Userr>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId).HasColumnName("User_id");

                entity.Property(e => e.ContactNumber).HasColumnName("Contact_Number");

                entity.Property(e => e.UserName).HasColumnName("User_Name");
            });

            modelBuilder.HasSequence<int>("id");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RPPBA.Models
{
    public partial class RPPBAContext : DbContext
    {
        public RPPBAContext()
        {
        }

        public RPPBAContext(DbContextOptions<RPPBAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Addresses> Addresses { get; set; }
        public virtual DbSet<Balance> Balance { get; set; }
        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Discounts> Discounts { get; set; }
        public virtual DbSet<OrderBasket> OrderBasket { get; set; }
        public virtual DbSet<OrderHistory> OrderHistory { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Organizations> Organizations { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Regions> Regions { get; set; }
        public virtual DbSet<System> System { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=u1042103.plsk.regruhosting.ru;Trusted_Connection=false;database=u1042103_kazvo;user id=u1042103_kseniya;password=3V5udc6*;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Addresses>(entity =>
            {
                entity.HasKey(e => e.AddressId)
                    .HasName("addresses_pk");

                entity.ToTable("addresses");

                entity.Property(e => e.AddressId)
                    .HasColumnName("address_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.BuildingInt)
                    .HasColumnName("building_INT")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CityId).HasColumnName("city_id");

                entity.Property(e => e.StreetName)
                    .HasColumnName("street_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("table_7_cities_fk");
            });

            modelBuilder.Entity<Balance>(entity =>
            {
                entity.HasKey(e => e.OrganizationId)
                    .HasName("balance_pk");

                entity.ToTable("balance");

                entity.Property(e => e.OrganizationId)
                    .HasColumnName("organization_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CurrentBalance).HasColumnName("current_balance");

                entity.HasOne(d => d.Organization)
                    .WithOne(p => p.Balance)
                    .HasForeignKey<Balance>(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("table_9_organizations_fk");
            });
            modelBuilder.Entity<DiscountProgram>(entity =>
            {
                entity.HasKey(e => e.DiscountId)
                    .HasName("PK__discount__BDBE9EF977B6E5F1");

                entity.ToTable("discount_program", "dbo");

                entity.Property(e => e.DiscountId)
                    .HasColumnName("discount_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.EndSum).HasColumnName("end_sum");

                entity.Property(e => e.StartSum).HasColumnName("start_sum");
            });
            modelBuilder.Entity<Cities>(entity =>
            {
                entity.HasKey(e => e.CityId)
                    .HasName("cities_pk");

                entity.ToTable("cities");

                entity.Property(e => e.CityId)
                    .HasColumnName("city_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CityName)
                    .HasColumnName("city_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("table_6_countries_fk");
            });

            modelBuilder.Entity<Countries>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                    .HasName("countries_pk");

                entity.ToTable("countries");

                entity.Property(e => e.CountryId)
                    .HasColumnName("country_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CountryName)
                    .HasColumnName("country_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RegionId).HasColumnName("region_id");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Countries)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("table_5_regions_fk");
            });

            modelBuilder.Entity<Discounts>(entity =>
            {
                entity.HasKey(e => e.OrganizationId)
                    .HasName("discounts_pk");

                entity.ToTable("discounts");

                entity.Property(e => e.OrganizationId)
                    .HasColumnName("organization_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.HasOne(d => d.Organization)
                    .WithOne(p => p.Discounts)
                    .HasForeignKey<Discounts>(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("table_12_organizations_fk");
            });

            modelBuilder.Entity<OrderBasket>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("order_basket_pk");

                entity.ToTable("order_basket");

                entity.Property(e => e.TransactionId)
                    .HasColumnName("transaction_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.TotalSale).HasColumnName("total_sale");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderBasketOrder)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("table_14_orders_fk");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderBasket)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("table_14_products_fk");

                entity.HasOne(d => d.Transaction)
                    .WithOne(p => p.OrderBasketTransaction)
                    .HasForeignKey<OrderBasket>(d => d.TransactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_basket_orders_fk");
            });

            modelBuilder.Entity<OrderHistory>(entity =>
            {
                entity.ToTable("order_history");

                entity.Property(e => e.OrderHistoryId)
                    .HasColumnName("order_history_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AmountTransfer).HasColumnName("amount_transfer");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.OrderStatus)
                    .HasColumnName("order_status")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StatusUpdateDate)
                    .HasColumnName("status_update_date")
                    .HasColumnType("date");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderHistory)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("table_13_orders_fk");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("orders_pk");

                entity.ToTable("orders");

                entity.Property(e => e.TransactionId)
                    .HasColumnName("transaction_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.ExtraDiscount).HasColumnName("extra_discount");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.OrderStatus)
                    .HasColumnName("order_status")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.OrganizationId).HasColumnName("organization_id");

                entity.Property(e => e.ShippingDate)
                    .HasColumnName("shipping_date")
                    .HasColumnType("date");

                entity.Property(e => e.TotalSale).HasColumnName("total_sale");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("table_11_addresses_fk");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("table_11_organizations_fk");
            });

            modelBuilder.Entity<Organizations>(entity =>
            {
                entity.HasKey(e => e.OrganizationId)
                    .HasName("organizations_pk");

                entity.ToTable("organizations");

                entity.Property(e => e.OrganizationId)
                    .HasColumnName("organization_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.OrganizationAddressId).HasColumnName("organization_address_id");

                entity.Property(e => e.OrganizationDirectorFullname)
                    .HasColumnName("organization_director_fullname")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.OrganizationName)
                    .HasColumnName("organization_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.OrganizationPaymentAccount)
                    .HasColumnName("organization_payment_account")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.OrganizationPhoneInt)
                    .HasColumnName("organization_phone_INT")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.OrganizationAddress)
                    .WithMany(p => p.Organizations)
                    .HasForeignKey(d => d.OrganizationAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("table_8_addresses_fk");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("products_pk");

                entity.ToTable("products");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ProductAvailableQuantity).HasColumnName("product_available_quantity");

                entity.Property(e => e.ProductDescription)
                    .HasColumnName("product_description")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .HasColumnName("product_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProductPrice).HasColumnName("product_price");

                entity.Property(e => e.ProductReservedQuantity).HasColumnName("product_reserved_quantity");
            });

            modelBuilder.Entity<Regions>(entity =>
            {
                entity.HasKey(e => e.RegionId)
                    .HasName("regions_pk");

                entity.ToTable("regions");

                entity.Property(e => e.RegionId)
                    .HasColumnName("region_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.RegionName)
                    .HasColumnName("region_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<System>(entity =>
            {
                entity.ToTable("system");

                entity.Property(e => e.SystemId)
                    .HasColumnName("system_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Login)
                    .HasColumnName("login")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Role).HasColumnName("role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

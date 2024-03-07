using fluttyBackend.Domain.Models;
using fluttyBackend.Domain.Models.CartItem;
using fluttyBackend.Domain.Models.Company;
using fluttyBackend.Domain.Models.Company.OnRequest;
using fluttyBackend.Domain.Models.DeliveryProduct;
using fluttyBackend.Domain.Models.ProductEntities;
using fluttyBackend.Domain.Models.ProductEntities.OnRequest;
using fluttyBackend.Domain.Models.UserRoleEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace fluttyBackend.Domain.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Product ===============================================================
        public DbSet<Product> Products { get; set; }
        public DbSet<OtMPhotosOfProduct> OtMPhotosOfProducts { get; set; }
        public DbSet<ProductAdditionRequest> ProductAdditionRequests { get; set; }
        // Product ===============================================================

        // User & Role ===========================================================
        public DbSet<User> Users { get; set; }
        // User & Role ===========================================================

        // Company ===============================================================
        public DbSet<CompanyTbl> Companies { get; set; }
        public DbSet<OtMCompanyEmployees> CompanyEmployees { get; set; }
        public DbSet<CompanyAdditionRequest> CompanyAdditionRequests { get; set; }
        // Company ===============================================================

        // CartItem ==============================================================
        public DbSet<CartItem> CartItems { get; set; }
        // CartItem ==============================================================

        // Delivery Product ======================================================
        public DbSet<DeliveryOrderProduct> DeliveryProducts { get; set; }
        public DbSet<OtMOrderProduct> OtMDeliveryProducts { get; set; }
        // Delivery Product ======================================================


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Product configuration =============================================
            modelBuilder.Entity<Product>()
                .HasMany(p => p.AdditionalPhotos)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            // Product configuration =============================================

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) { }
    }
}
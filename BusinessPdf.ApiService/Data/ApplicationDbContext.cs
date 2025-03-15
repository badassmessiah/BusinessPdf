using BusinessPdf.ApiService.Models;
using BusinessPdf.ApiService.Models.PDF;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BusinessPdf.ApiService.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TenantModel> Tenants { get; set; }
        public DbSet<ApplicationUserTenant> ApplicationUserTenants { get; set; }
        public DbSet<CompanyInfo> CompanyInfos { get; set; }
        public DbSet<InvoiceModel> InvoiceModels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUserTenant>().HasKey(t => new { t.UserId, t.TenantId });

            builder.Entity<ApplicationUserTenant>()
                .HasOne(t => t.ApplicationUser)
                .WithMany(u => u.ApplicationUserTenants)
                .HasForeignKey(t => t.UserId);

            builder.Entity<ApplicationUserTenant>()
                .HasOne(t => t.TenantModel)
                .WithMany(tm => tm.ApplicationUserTenants)
                .HasForeignKey(t => t.TenantId);

        }
    }
}

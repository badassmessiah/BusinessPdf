using BusinessPdf.ApiService.Data;
using BusinessPdf.ApiService.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessPdf.ApiService.Services
{
    public class TenantService
    {
        private readonly ApplicationDbContext _context;
        public TenantService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TenantModel>> GetTenantsAsync()
        {
            return await _context.Tenants.ToListAsync();
        }
        public async Task<TenantModel> GetTenantAsync(Guid tenantId)
        {
            return await _context.Tenants.FindAsync(tenantId);
        }
        public async Task<TenantModel> GetTenantByNameAsync(string tenantName)
        {
            return await _context.Tenants.FirstOrDefaultAsync(t => t.Name == tenantName);
        }
        public async Task<TenantModel> CreateTenantAsync(string tenantName)
        {
            var tenant = new TenantModel
            {
                Id = Guid.NewGuid(),
                Name = tenantName
            };
            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();
            return tenant;
        }
        public async Task<ApplicationUserTenant> AddUserToTenantAsync(ApplicationUser user, TenantModel tenant)
        {
            var userTenant = await _context.ApplicationUserTenants.FirstOrDefaultAsync(ut => ut.UserId == user.Id && ut.TenantId == tenant.Id);
            if (userTenant == null)
            {
                userTenant = new ApplicationUserTenant
                {
                    UserId = user.Id,
                    TenantId = tenant.Id
                };
                await _context.ApplicationUserTenants.AddAsync(userTenant);
                await _context.SaveChangesAsync();
            }
            return userTenant;
        }
        public async Task<ApplicationUserTenant> RemoveUserFromTenantAsync(ApplicationUser user, TenantModel tenant)
        {
            var userTenant = await _context.ApplicationUserTenants.FirstOrDefaultAsync(ut => ut.UserId == user.Id && ut.TenantId == tenant.Id);
            if (userTenant != null)
            {
                _context.ApplicationUserTenants.Remove(userTenant);
                await _context.SaveChangesAsync();
            }
            return userTenant;
        }

        public async Task<List<ApplicationUser>> GetUsersForTenantAsync(Guid tenantId)
        {
            return await _context.ApplicationUserTenants
                .Where(ut => ut.TenantId == tenantId)
                .Select(ut => ut.ApplicationUser)
                .ToListAsync();
        }

        public async Task<List<TenantModel>> GetTenantsForUserAsync(string userId)
        {
            return await _context.ApplicationUserTenants
                .Where(ut => ut.UserId == userId)
                .Include(ut => ut.TenantModel)
                .Select(ut => ut.TenantModel)
                .ToListAsync();
        }

    }
}

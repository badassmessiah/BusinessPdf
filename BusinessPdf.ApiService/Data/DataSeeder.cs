using Microsoft.AspNetCore.Identity;

namespace BusinessPdf.ApiService.Data
{
    public class DataSeeder
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataSeeder(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task SeedRolesAsync()
        {
            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
        }
    }
}

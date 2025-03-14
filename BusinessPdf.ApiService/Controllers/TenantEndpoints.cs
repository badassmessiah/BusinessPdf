using System.Runtime.CompilerServices;
using BusinessPdf.ApiService.Models;
using BusinessPdf.ApiService.Services;
using Microsoft.AspNetCore.Identity;

namespace BusinessPdf.ApiService.Controllers
{
    public static class TenantEndpoints
    {
        public static void MapTenantEndpoints(this IEndpointRouteBuilder endpoint)
        {
            endpoint.MapGet("/alltenants", async (HttpContext context) =>
            {
                var tenantService = context.RequestServices.GetRequiredService<TenantService>();
                var tenants = await Task.Run(() => tenantService.GetTenantsAsync());
                return Results.Ok(tenants);
            }).WithTags("TenantOperations");

            endpoint.MapGet("/tenant/{tenantId}", async (HttpContext context, string tenantId) =>
            {
                var tenantService = context.RequestServices.GetRequiredService<TenantService>();
                if (!Guid.TryParse(tenantId, out var tenantGuid))
                {
                    return Results.BadRequest("Invalid tenant ID format.");
                }
                var tenant = await Task.Run(() => tenantService.GetTenantAsync(tenantGuid));
                var users = await Task.Run(() => tenantService.GetUsersForTenantAsync(tenantGuid));

                if (tenant != null && users != null)
                {
                    return Results.Ok(new { tenant, users });
                }

                return Results.NotFound();
            }).WithTags("TenantOperations");

            endpoint.MapGet("/tenantbyname/{tenantName}", async (HttpContext context, string tenantName) =>
            {
                var tenantService = context.RequestServices.GetRequiredService<TenantService>();
                var tenant = await Task.Run(() => tenantService.GetTenantByNameAsync(tenantName));
                return tenant != null ? Results.Ok(tenant) : Results.NotFound();
            }).WithTags("TenantOperations");

            endpoint.MapPost("/tenant/{tenantName}", async (HttpContext context, string tenantName) =>
            {
                var tenantService = context.RequestServices.GetRequiredService<TenantService>();
                if (string.IsNullOrWhiteSpace(tenantName))
                {
                    return Results.BadRequest("Tenant name is missing or invalid.");
                }

                // Check if tenant name already exists
                var existingTenant = await Task.Run(() => tenantService.GetTenantByNameAsync(tenantName));
                if (existingTenant != null)
                {
                    return Results.BadRequest("Tenant name already in use.");
                }

                // Create tenant if name is available
                var newTenant = await Task.Run(() => tenantService.CreateTenantAsync(tenantName));
                return Results.Created($"/tenant/{newTenant.Id}", newTenant);
            }).WithTags("TenantOperations");

            endpoint.MapPost("/tenant/{tenantId}/adduser/{userId}", async (HttpContext context, string tenantId, string userId) =>
            {
                var tenantService = context.RequestServices.GetRequiredService<TenantService>();
                if (!Guid.TryParse(tenantId, out var tenantGuid))
                {
                    return Results.BadRequest("Invalid tenant ID format.");
                }
                var tenant = await tenantService.GetTenantAsync(tenantGuid);
                if (tenant == null)
                {
                    return Results.NotFound("Tenant not found.");
                }
                var userManager = context.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
                var user = await userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return Results.BadRequest("User not found.");
                }

                var userTenant = await tenantService.AddUserToTenantAsync(user, tenant);

                // Return a simple response without the navigation properties
                return Results.Ok("User is added to tenant");
            }).WithTags("TenantOperations");

            endpoint.MapPost("/tenant/{tenantId}/removeuser/{userId}", async (HttpContext context, string tenantId, string userId) =>
            {
                var tenantService = context.RequestServices.GetRequiredService<TenantService>();
                var tenant = await Task.Run(() => tenantService.GetTenantAsync(Guid.TryParse(tenantId, out var tenantGuid) ? tenantGuid : Guid.Empty));
                if (tenant == null)
                {
                    return Results.NotFound();
                }
                var userManager = context.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
                var user = await userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return Results.BadRequest("User data is missing");
                }
                var userTenant = await Task.Run(() => tenantService.RemoveUserFromTenantAsync(user, tenant));
                return userTenant != null ? Results.Ok(userTenant) : Results.NotFound();
            }).WithTags("TenantOperations");
        }
    }
}

using BusinessPdf.ApiService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BusinessPdf.ApiService.Controllers
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/allusers", async (HttpContext context) =>
            {
                var userManager = context.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
                var users = await Task.Run(() => userManager.Users.ToList());
                return users;
            });
        }
    }
}

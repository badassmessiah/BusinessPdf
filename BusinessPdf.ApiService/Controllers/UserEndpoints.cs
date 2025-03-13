using BusinessPdf.ApiService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;

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
                return Results.Ok(users);
            }).WithTags("UserOperations");

            endpoints.MapGet("/getuser/{id}", async (HttpContext context, string id) =>
            {
                var userManager = context.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
                var user = await userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(user);
            }).WithTags("UserOperations");

            endpoints.MapPatch("/edituser/{id}", async (HttpContext context, string id, EditUserModel editUserModel) =>
            {
                var userManager = context.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
                var user = await userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return Results.NotFound();
                }

                var editUserProperties = typeof(EditUserModel).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                var userProperties = typeof(ApplicationUser).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var property in editUserProperties)
                {
                    var value = property.GetValue(editUserModel);
                    if (value != null)
                    {
                        var userProperty = userProperties.FirstOrDefault(p => p.Name == property.Name);
                        if (userProperty != null)
                        {
                            userProperty.SetValue(user, value);
                        }
                    }
                }

                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Results.Ok(user);
                }
                return Results.BadRequest(result.Errors);
            }).WithTags("UserOperations");

            endpoints.MapDelete("/deleteuser/{id}", async (HttpContext context, string id) =>
            {
                var userManager = context.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
                var user = await userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return Results.NotFound();
                }
                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return Results.Ok();
                }
                return Results.BadRequest(result.Errors);
            }).WithTags("UserOperations");

        }
    }
}

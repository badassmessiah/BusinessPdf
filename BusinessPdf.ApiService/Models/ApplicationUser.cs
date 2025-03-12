using Microsoft.AspNetCore.Identity;

namespace BusinessPdf.ApiService.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Guid TenantID { get; set; }
    }
}

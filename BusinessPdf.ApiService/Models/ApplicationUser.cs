﻿using Microsoft.AspNetCore.Identity;

namespace BusinessPdf.ApiService.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Guid TenantID { get; set; }
        public string Name { get; set; }

        // Many-to-many relationship
        public ICollection<ApplicationUserTenant> ApplicationUserTenants { get; set; }
    }
}

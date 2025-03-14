namespace BusinessPdf.ApiService.Models
{
    public class TenantModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // Many-to-many relationship
        public ICollection<ApplicationUserTenant> ApplicationUserTenants { get; set; }
    }
}

namespace BusinessPdf.ApiService.Models
{
    public class ApplicationUserTenant
    {
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public Guid TenantId { get; set; }
        public TenantModel TenantModel { get; set; }
    }
}

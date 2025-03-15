namespace BusinessPdf.ApiService.Models.Invoice
{
    public class CompanyInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string VatNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string WebSite { get; set; }
        public string SwiftInfo { get; set; }
        public string IbanInfo { get; set; }
    }
}
namespace BusinessPdf.ApiService.Models.Invoice
{
    public class ServiceForm
    {
        public Guid Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public List<ServiceItems> ServiceItems { get; set; }
        public double TotalPrice { get; set; }
        public double TotalPriceWithVat { get; set; }
    }
}

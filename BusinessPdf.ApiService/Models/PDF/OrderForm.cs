namespace BusinessPdf.ApiService.Models.PDF
{
    public class OrderForm
    {
        public string Header { get; set; }
        public string Description { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public double TotalPrice { get; set; }
        public double TotalPriceWithVat { get; set; }
    }
}

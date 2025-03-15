using System.Globalization;
using System.Net;

namespace BusinessPdf.ApiService.Models.PDF
{
    public class InvoiceModel
    {
        public Guid Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }

        public CompanyInfo Seller { get; set; }
        public CompanyInfo Customer { get; set; }

        public List<OrderForm> OrderForms { get; set; }
        public List<ServiceForm> ServiceForms { get; set; }
        public string Remarks { get; set; }
        public string Comments { get; set; }

        public double GrandTotalPrice { get; set; }
        public double GrandTotalPriceWithVat { get; set; }
        public ApplicationUser User { get; set; }
    }
}

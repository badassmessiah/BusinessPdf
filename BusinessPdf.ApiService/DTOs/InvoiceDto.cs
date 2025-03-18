namespace BusinessPdf.ApiService.DTOs.InvoiceDto
{
    public class InvoiceDto
    {
        public string InvoiceNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }

        public CompanyInfoDto Seller { get; set; }
        public CompanyInfoDto Customer { get; set; }

        public List<OrderFormDto> OrderForms { get; set; }
        public List<ServiceFormDto> ServiceForms { get; set; }
        public string Remarks { get; set; }
        public string Comments { get; set; }

        public double GrandTotalPrice { get; set; }
        public double GrandTotalPriceWithVat { get; set; }
    }

    public class CompanyInfoDto
    {
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

    public class OrderFormDto
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public double TotalPrice { get; set; }
        public double TotalPriceWithVat { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }
    }

    public class OrderItemDto
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
    }

    public class ServiceFormDto
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public double TotalPrice { get; set; }
        public double TotalPriceWithVat { get; set; }

        public List<ServiceItemsDto> ServiceItems { get; set; }
    }

    public class ServiceItemsDto
    {
        public string Name { get; set; }
        public string Duration { get; set; }
    }
}

﻿namespace BusinessPdf.ApiService.Models.Invoice
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }

    }
}
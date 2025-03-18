using BusinessPdf.ApiService.DTOs.InvoiceDto;
using BusinessPdf.ApiService.Models.Invoice;
using BusinessPdf.ApiService.Services.InvoiceServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BusinessPdf.ApiService.Controllers
{
    public static class InvoiceTemplateEndpoints
    {
        public static void MapInvoiceTemplateEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/invoices", async (InvoiceTemplateService invoiceService) =>
            {
                var invoices = await invoiceService.GetAllAsync();
                return Results.Ok(invoices);
            }).WithTags("InvoiceOperations");

            endpoints.MapGet("/invoices/{id:guid}", async (InvoiceTemplateService invoiceService, Guid id) =>
            {
                var invoice = await invoiceService.GetByIdAsync(id);
                return invoice is not null ? Results.Ok(invoice) : Results.NotFound();
            }).WithTags("InvoiceOperations");

            endpoints.MapGet("/invoicenumber/{number}", async (InvoiceTemplateService invoiceService, string number) =>
            {
                var invoice = await invoiceService.GetByNumberAsync(number);
                return invoice is not null ? Results.Ok(invoice) : Results.NotFound();
            }).WithTags("InvoiceOperations");

            endpoints.MapPost("/invoices", async (InvoiceTemplateService invoiceService, InvoiceDto invoiceDto) =>
            {
                var createdInvoice = await invoiceService.CreateAsync(invoiceDto);
                return Results.Ok(createdInvoice);
            }).WithTags("InvoiceOperations");

            endpoints.MapPut("/invoices/{id:guid}", async (InvoiceTemplateService invoiceService, Guid id, InvoiceDto invoiceDto) =>
            {
                var updatedInvoice = await invoiceService.UpdateAsync(id, invoiceDto);
                return updatedInvoice is not null ? Results.Ok(updatedInvoice) : Results.NotFound();
            }).WithTags("InvoiceOperations");

            endpoints.MapDelete("/invoices/{id:guid}", async (InvoiceTemplateService invoiceService, Guid id) =>
            {
                await invoiceService.DeleteAsync(id);
                return Results.Ok("Invoice Deleted successfully");
            }).WithTags("InvoiceOperations");
        }
    }
}

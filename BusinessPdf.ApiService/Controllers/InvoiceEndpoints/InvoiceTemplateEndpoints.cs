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

            endpoints.MapGet("/invoices/{id}", async (InvoiceTemplateService invoiceService, string id) =>
            {
                var invoice = await invoiceService.GetByIdAsync(id);
                return invoice != null ? Results.Ok(invoice) : Results.NotFound();
            }).WithTags("InvoiceOperations");

            endpoints.MapGet("/invoicenumber/{number}", async (InvoiceTemplateService invoiceService, string number) =>
            {
                var invoice = await invoiceService.GetByNumberAsync(number);
                return invoice != null ? Results.Ok(invoice) : Results.NotFound();
            }).WithTags("invoiceOperations");

            endpoints.MapPost("/invoices", async (InvoiceTemplateService invoiceService, InvoiceModel invoiceModel) =>
            {
                var createdInvoice = await invoiceService.CreateAsync(invoiceModel);
                return Results.Created($"/invoices/{createdInvoice.Id}", createdInvoice);
            }).WithTags("InvoiceOperations");

            endpoints.MapPut("/invoices/{id}", async (InvoiceTemplateService invoiceService, string id, InvoiceModel invoiceModel) =>
            {
                if (id != invoiceModel.Id.ToString())
                {
                    return Results.BadRequest("Invoice ID mismatch.");
                }

                var updatedInvoice = await invoiceService.UpdateAsync(invoiceModel);
                return Results.Ok(updatedInvoice);
            }).WithTags("InvoiceOperations");

            endpoints.MapDelete("/invoices/{id}", async (InvoiceTemplateService invoiceService, string id) =>
            {
                await invoiceService.DeleteAsync(id);
                return Results.NoContent();
            }).WithTags("InvoiceOperations");
        }
    }
}

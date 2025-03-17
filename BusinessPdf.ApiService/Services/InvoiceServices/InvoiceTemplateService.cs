﻿using BusinessPdf.ApiService.Data;
using BusinessPdf.ApiService.Models.Invoice;
using Microsoft.EntityFrameworkCore;

namespace BusinessPdf.ApiService.Services.InvoiceServices
{
    public class InvoiceTemplateService
    {
        private readonly ApplicationDbContext _context;

        public InvoiceTemplateService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InvoiceModel>> GetAllAsync()
        {
            return await _context.InvoiceModels.ToListAsync();
        }

        public async Task<InvoiceModel> GetByIdAsync(Guid id)
        {
            return await _context.InvoiceModels.FindAsync(id);
        }

        public async Task<InvoiceModel> GetByNumberAsync(string number)
        {
            return await _context.InvoiceModels.FirstOrDefaultAsync(i => i.InvoiceNumber == number);
        }

        public async Task<InvoiceModel> CreateAsync(InvoiceModel invoiceModel)
        {
            _context.InvoiceModels.Add(invoiceModel);
            await _context.SaveChangesAsync();
            return invoiceModel;
        }

        public async Task<InvoiceModel> UpdateAsync(InvoiceModel invoiceModel)
        {
            _context.Entry(invoiceModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return invoiceModel;
        }

        public async Task DeleteAsync(Guid id)
        {
            var invoiceModel = await _context.InvoiceModels.FindAsync(id);
            if (invoiceModel != null)
            {
                _context.InvoiceModels.Remove(invoiceModel);
                await _context.SaveChangesAsync();
            }
        }
    }
}

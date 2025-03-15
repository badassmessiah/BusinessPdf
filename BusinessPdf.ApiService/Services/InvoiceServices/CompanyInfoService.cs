using BusinessPdf.ApiService.Data;
using BusinessPdf.ApiService.Models.PDF;
using Microsoft.EntityFrameworkCore;

namespace BusinessPdf.ApiService.Services.InvoiceServices
{
    public class CompanyInfoService
    {
        private readonly ApplicationDbContext _context;

        public CompanyInfoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CompanyInfo>> GetAllAsync()
        {
            return await _context.CompanyInfos.ToListAsync();
        }

        public async Task<CompanyInfo> GetByIdAsync(Guid id)
        {
            return await _context.CompanyInfos.FindAsync(id);
        }

        public async Task<CompanyInfo> CreateAsync(CompanyInfo companyInfo)
        {
            _context.CompanyInfos.Add(companyInfo);
            await _context.SaveChangesAsync();
            return companyInfo;
        }

        public async Task<CompanyInfo> UpdateAsync(CompanyInfo companyInfo)
        {
            _context.Entry(companyInfo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return companyInfo;
        }

        public async Task DeleteAsync(Guid id)
        {
            var companyInfo = await _context.CompanyInfos.FindAsync(id);
            if (companyInfo != null)
            {
                _context.CompanyInfos.Remove(companyInfo);
                await _context.SaveChangesAsync();
            }
        }
    }
}

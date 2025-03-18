using BusinessPdf.ApiService.Data;
using BusinessPdf.ApiService.Models.Invoice;
using Microsoft.EntityFrameworkCore;
using BusinessPdf.ApiService.DTOs.InvoiceDto;

namespace BusinessPdf.ApiService.Services.InvoiceServices
{
    public class CompanyInfoService
    {
        private readonly ApplicationDbContext _context;

        public CompanyInfoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CompanyInfoDto>> GetAllAsync()
        {
            var entities = await _context.CompanyInfos.ToListAsync();
            return entities.Select(ToDto).ToList();
        }

        public async Task<CompanyInfoDto> GetByIdAsync(Guid id)
        {
            var entity = await _context.CompanyInfos.FindAsync(id);
            return entity is not null ? ToDto(entity) : null;
        }

        public async Task<CompanyInfoDto> CreateAsync(CompanyInfoDto dto)
        {
            var entity = ToEntity(dto);
            _context.CompanyInfos.Add(entity);
            await _context.SaveChangesAsync();
            return ToDto(entity);
        }

        public async Task<CompanyInfoDto> UpdateAsync(Guid id, CompanyInfoDto dto)
        {
            var entity = await _context.CompanyInfos.FindAsync(id);
            if (entity == null)
                return null;

            entity.Name = dto.Name;
            entity.Address = dto.Address;
            entity.City = dto.City;
            entity.Country = dto.Country;
            entity.VatNumber = dto.VatNumber;
            entity.PhoneNumber = dto.PhoneNumber;
            entity.WebSite = dto.WebSite;
            entity.SwiftInfo = dto.SwiftInfo;
            entity.IbanInfo = dto.IbanInfo;

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return ToDto(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.CompanyInfos.FindAsync(id);
            if (entity != null)
            {
                _context.CompanyInfos.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        private static CompanyInfoDto ToDto(CompanyInfo entity)
        {
            return new CompanyInfoDto
            {
                Name = entity.Name,
                Address = entity.Address,
                City = entity.City,
                Country = entity.Country,
                VatNumber = entity.VatNumber,
                PhoneNumber = entity.PhoneNumber,
                WebSite = entity.WebSite,
                SwiftInfo = entity.SwiftInfo,
                IbanInfo = entity.IbanInfo
            };
        }

        private static CompanyInfo ToEntity(CompanyInfoDto dto)
        {
            return new CompanyInfo
            {
                Name = dto.Name,
                Address = dto.Address,
                City = dto.City,
                Country = dto.Country,
                VatNumber = dto.VatNumber,
                PhoneNumber = dto.PhoneNumber,
                WebSite = dto.WebSite,
                SwiftInfo = dto.SwiftInfo,
                IbanInfo = dto.IbanInfo
            };
        }
    }
}

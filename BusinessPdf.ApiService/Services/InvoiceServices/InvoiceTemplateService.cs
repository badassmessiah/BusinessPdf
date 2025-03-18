using BusinessPdf.ApiService.Data;
using BusinessPdf.ApiService.Models.Invoice;
using Microsoft.EntityFrameworkCore;
using BusinessPdf.ApiService.DTOs.InvoiceDto;

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
            var invoices = await _context.InvoiceModels
                .Include(i => i.Seller)
                .Include(i => i.Customer)
                .Include(i => i.OrderForms)
                    .ThenInclude(of => of.OrderItems)
                .Include(i => i.ServiceForms)
                    .ThenInclude(sf => sf.ServiceItems)
                .ToListAsync();

            return invoices.ToList();
        }

        public async Task<InvoiceDto> GetByIdAsync(Guid id)
        {
            var invoice = await _context.InvoiceModels
                .Include(i => i.Seller)
                .Include(i => i.Customer)
                .Include(i => i.OrderForms)
                    .ThenInclude(of => of.OrderItems)
                .Include(i => i.ServiceForms)
                    .ThenInclude(sf => sf.ServiceItems)
                .FirstOrDefaultAsync(i => i.Id == id);

            return invoice is not null ? ToDto(invoice) : null;
        }

        public async Task<InvoiceDto> GetByNumberAsync(string number)
        {
            var invoice = await _context.InvoiceModels
                .Include(i => i.Seller)
                .Include(i => i.Customer)
                .Include(i => i.OrderForms)
                    .ThenInclude(of => of.OrderItems)
                .Include(i => i.ServiceForms)
                    .ThenInclude(sf => sf.ServiceItems)
                .FirstOrDefaultAsync(i => i.InvoiceNumber == number);

            return invoice is not null ? ToDto(invoice) : null;
        }

        public async Task<InvoiceDto> CreateAsync(InvoiceDto invoiceDto)
        {
            var entity = ToEntity(invoiceDto);
            _context.InvoiceModels.Add(entity);
            await _context.SaveChangesAsync();
            return ToDto(entity);
        }

        public async Task<InvoiceDto> UpdateAsync(Guid id, InvoiceDto invoiceDto)
        {
            var entity = await _context.InvoiceModels.FindAsync(id);
            if (entity == null) return null;

            entity.InvoiceNumber = invoiceDto.InvoiceNumber;
            entity.IssueDate = invoiceDto.IssueDate;
            entity.DueDate = invoiceDto.DueDate;
            entity.Remarks = invoiceDto.Remarks;
            entity.Comments = invoiceDto.Comments;
            entity.GrandTotalPrice = invoiceDto.GrandTotalPrice;
            entity.GrandTotalPriceWithVat = invoiceDto.GrandTotalPriceWithVat;

            entity.Seller = invoiceDto.Seller != null ? new CompanyInfo
            {
                Name = invoiceDto.Seller.Name,
                Address = invoiceDto.Seller.Address,
                City = invoiceDto.Seller.City,
                Country = invoiceDto.Seller.Country,
                VatNumber = invoiceDto.Seller.VatNumber,
                PhoneNumber = invoiceDto.Seller.PhoneNumber,
                WebSite = invoiceDto.Seller.WebSite,
                SwiftInfo = invoiceDto.Seller.SwiftInfo,
                IbanInfo = invoiceDto.Seller.IbanInfo
            } : null;

            entity.Customer = invoiceDto.Customer != null ? new CompanyInfo
            {
                Name = invoiceDto.Customer.Name,
                Address = invoiceDto.Customer.Address,
                City = invoiceDto.Customer.City,
                Country = invoiceDto.Customer.Country,
                VatNumber = invoiceDto.Customer.VatNumber,
                PhoneNumber = invoiceDto.Customer.PhoneNumber,
                WebSite = invoiceDto.Customer.WebSite,
                SwiftInfo = invoiceDto.Customer.SwiftInfo,
                IbanInfo = invoiceDto.Customer.IbanInfo
            } : null;

            entity.OrderForms = invoiceDto.OrderForms?.Select(o => new OrderForm
            {
                Header = o.Header,
                Description = o.Description,
                TotalPrice = o.TotalPrice,
                TotalPriceWithVat = o.TotalPriceWithVat,
                OrderItems = o.OrderItems?.Select(i => new OrderItem
                {
                    Name = i.Name,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            }).ToList();

            entity.ServiceForms = invoiceDto.ServiceForms?.Select(s => new ServiceForm
            {
                Header = s.Header,
                Description = s.Description,
                Duration = s.Duration,
                TotalPrice = s.TotalPrice,
                TotalPriceWithVat = s.TotalPriceWithVat,
                ServiceItems = s.ServiceItems?.Select(i => new ServiceItems
                {
                    Name = i.Name,
                    Duration = i.Duration
                }).ToList()
            }).ToList();

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return ToDto(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var invoice = await _context.InvoiceModels
                .Include(i => i.OrderForms)
                .Include(i => i.ServiceForms)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice != null)
            {
                _context.InvoiceModels.Remove(invoice);
                await _context.SaveChangesAsync();
            }
        }

        private static InvoiceDto ToDto(InvoiceModel model)
        {
            return new InvoiceDto
            {
                InvoiceNumber = model.InvoiceNumber,
                IssueDate = model.IssueDate,
                DueDate = model.DueDate,
                Seller = model.Seller == null ? null : new CompanyInfoDto
                {
                    Name = model.Seller.Name,
                    Address = model.Seller.Address,
                    City = model.Seller.City,
                    Country = model.Seller.Country,
                    VatNumber = model.Seller.VatNumber,
                    PhoneNumber = model.Seller.PhoneNumber,
                    WebSite = model.Seller.WebSite,
                    SwiftInfo = model.Seller.SwiftInfo,
                    IbanInfo = model.Seller.IbanInfo
                },
                Customer = model.Customer == null ? null : new CompanyInfoDto
                {
                    Name = model.Customer.Name,
                    Address = model.Customer.Address,
                    City = model.Customer.City,
                    Country = model.Customer.Country,
                    VatNumber = model.Customer.VatNumber,
                    PhoneNumber = model.Customer.PhoneNumber,
                    WebSite = model.Customer.WebSite,
                    SwiftInfo = model.Customer.SwiftInfo,
                    IbanInfo = model.Customer.IbanInfo
                },
                OrderForms = model.OrderForms?.Select(o => new OrderFormDto
                {
                    Header = o.Header,
                    Description = o.Description,
                    TotalPrice = o.TotalPrice,
                    TotalPriceWithVat = o.TotalPriceWithVat,
                    OrderItems = o.OrderItems?.Select(i => new OrderItemDto
                    {
                        Name = i.Name,
                        Quantity = i.Quantity,
                        Price = i.Price
                    }).ToList()
                }).ToList(),
                ServiceForms = model.ServiceForms?.Select(s => new ServiceFormDto
                {
                    Header = s.Header,
                    Description = s.Description,
                    Duration = s.Duration,
                    TotalPrice = s.TotalPrice,
                    TotalPriceWithVat = s.TotalPriceWithVat,
                    ServiceItems = s.ServiceItems?.Select(i => new ServiceItemsDto
                    {
                        Name = i.Name,
                        Duration = i.Duration
                    }).ToList()
                }).ToList(),
                Remarks = model.Remarks,
                Comments = model.Comments,
                GrandTotalPrice = model.GrandTotalPrice,
                GrandTotalPriceWithVat = model.GrandTotalPriceWithVat
            };
        }

        private static InvoiceModel ToEntity(InvoiceDto dto)
        {
            return new InvoiceModel
            {
                InvoiceNumber = dto.InvoiceNumber,
                IssueDate = dto.IssueDate,
                DueDate = dto.DueDate,
                Remarks = dto.Remarks,
                Comments = dto.Comments,
                GrandTotalPrice = dto.GrandTotalPrice,
                GrandTotalPriceWithVat = dto.GrandTotalPriceWithVat,
                Seller = dto.Seller == null ? null : new CompanyInfo
                {
                    Name = dto.Seller.Name,
                    Address = dto.Seller.Address,
                    City = dto.Seller.City,
                    Country = dto.Seller.Country,
                    VatNumber = dto.Seller.VatNumber,
                    PhoneNumber = dto.Seller.PhoneNumber,
                    WebSite = dto.Seller.WebSite,
                    SwiftInfo = dto.Seller.SwiftInfo,
                    IbanInfo = dto.Seller.IbanInfo
                },
                Customer = dto.Customer == null ? null : new CompanyInfo
                {
                    Name = dto.Customer.Name,
                    Address = dto.Customer.Address,
                    City = dto.Customer.City,
                    Country = dto.Customer.Country,
                    VatNumber = dto.Customer.VatNumber,
                    PhoneNumber = dto.Customer.PhoneNumber,
                    WebSite = dto.Customer.WebSite,
                    SwiftInfo = dto.Customer.SwiftInfo,
                    IbanInfo = dto.Customer.IbanInfo
                },
                OrderForms = dto.OrderForms?.Select(o => new OrderForm
                {
                    Header = o.Header,
                    Description = o.Description,
                    TotalPrice = o.TotalPrice,
                    TotalPriceWithVat = o.TotalPriceWithVat,
                    OrderItems = o.OrderItems?.Select(i => new OrderItem
                    {
                        Name = i.Name,
                        Quantity = i.Quantity,
                        Price = i.Price
                    }).ToList()
                }).ToList(),
                ServiceForms = dto.ServiceForms?.Select(s => new ServiceForm
                {
                    Header = s.Header,
                    Description = s.Description,
                    Duration = s.Duration,
                    TotalPrice = s.TotalPrice,
                    TotalPriceWithVat = s.TotalPriceWithVat,
                    ServiceItems = s.ServiceItems?.Select(i => new ServiceItems
                    {
                        Name = i.Name,
                        Duration = i.Duration
                    }).ToList()
                }).ToList()
            };
        }
    }
}

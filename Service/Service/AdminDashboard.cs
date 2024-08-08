using HAMDA.Core.Extentions;
using HAMDA.Data;
using HAMDA.Models.EntityModels.Enum;
using HAMDA.Models.ViewModels;
using HAMDA.Repository.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HAMDA.Service.Service
{
    public class AdminDashboard : IAdminDashboard
    {
        private readonly ApplicationDbContext _context;
        private readonly IAttachmentService _attachmentService;

        public AdminDashboard(ApplicationDbContext context, IAttachmentService attachmentService)
        {
            _context = context;
            _attachmentService = attachmentService;
        }


        public async Task<AdminDashboardModel> GetAllAsync(int status, int pageNumber, int pageSize)
        {
            var skip = (pageNumber - 1) * pageSize;

            var costumers = await _context.Costumers
                .Where(x => x.Status == status)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            var result = new AdminDashboardModel
            {
                lstItems = new List<AdminDashboarditems>(),
                totalCount = await _context.Costumers.CountAsync(x => x.Status == status),
                pageNumber = pageNumber,
                pageSize = pageSize
            };

            if (costumers.IsNotNullOrEmpty())
            {
                result.pageNumber = pageNumber;
                result.pageSize = pageSize;

                foreach (var costumer in costumers)
                {
                    var lstAttachments = await _attachmentService.GetAttachmentsCountAsync(costumer.Id, 1);
                    result.lstItems.Add(new AdminDashboarditems
                    {
                        Id = costumer.Id,
                        Country = costumer.Country,
                        Email = costumer.Email,
                        Phone = costumer.Phone,
                        Username = costumer.Username,
                        AttachmentsCount = lstAttachments,
                    });
                }

                if (result.IsNotNullOrEmpty())
                {
                    return result;
                }
            }

            
            return new AdminDashboardModel();
        }



        public async Task<AdminDashboarditems> Details(int Id)
        {
            var lstAttachments = await _attachmentService.GetAttachmentsAsync(Id, 1);
            var costumer = await _context.Costumers.Where(x => x.Id == Id).Select(x =>  
            new AdminDashboarditems
            {
                Id = x.Id,
                CurrentStatus = x.Status,
                Username = x.Username,
                Country = x.Country,
                Phone = x.Phone,
                Email = x.Email,
                Attachments = lstAttachments.IsNotNullOrEmpty() ? lstAttachments : null,
                AttachmentsCount = lstAttachments.IsNotNullOrEmpty() ? lstAttachments.Count() : 0,
            }).FirstOrDefaultAsync();

            if (costumer.IsNotNullOrEmpty())
            {
                return costumer;
            }
            
            return null;
        }


        public async Task<bool> MakeAction(long Id, int Status,Guid UserId)
        {
            var costumer = await _context.Costumers.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (costumer.IsNotNullOrEmpty())
            {
                costumer.ModifiedById = UserId;
                costumer.Status = Status;
                _context.Costumers.Update(costumer);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;

        }



    }
}

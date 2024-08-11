using HAMDA.Core.Extentions;
using HAMDA.Data;
using HAMDA.Models.EntityModels.Costumer;
using HAMDA.Models.EntityModels.Enum;
using HAMDA.Models.ViewModels;
using HAMDA.Repository.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

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


        public async Task<AdminDashboardModel> GetAllAsync(int status, int pageNumber, int pageSize,string hSearch = null)
        {
            var skip = (pageNumber - 1) * pageSize;

            var costumers = await _context.Costumers
                .Where(GetAllCustomersCondition(status,hSearch))
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            var result = new AdminDashboardModel
            {
                lstItems = new List<AdminDashboarditems>(),
                totalCount = await _context.Costumers.CountAsync(x => x.Status == status),
                pageNumber = pageNumber,
                pageSize = pageSize,
                NumberOfSeats = (await _context.Configuration.FirstOrDefaultAsync(x => x.Id == 1)).NumberOfSeats
            };

            if (costumers.IsNotNullOrEmpty())
            {                
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
            }

            return result;
        }

        private static Expression<Func<CostumerRegister, bool>> GetAllCustomersCondition(int status, string hSearch = null)
        {
            Expression<Func<CostumerRegister, bool>> condition = x => x.Status == status;
            if (hSearch.IsNotNullOrEmpty())
            {
                hSearch = hSearch.ToLower();
                condition = condition.AndAlso(x =>
                    x.Email.ToLower().Contains(hSearch) ||
                    x.Phone.ToLower().Contains(hSearch) ||
                    x.Username.ToLower().Contains(hSearch) ||
                    x.Country.ToLower().Contains(hSearch));
            }

            return condition;
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


        public async Task<bool> MakeAction(UpdateCostumerModel model, Guid UserId)
        {
            var costumer = await _context.Costumers.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
            if (costumer.IsNotNullOrEmpty())
            {
                costumer.ModifiedById = UserId;
                costumer.Status = model.Status;
                _context.Costumers.Update(costumer);

                var attachmentsSaved = false;

                if (model.Status == 1)
                {
                    attachmentsSaved = await _attachmentService.SaveAttachmentsAsync(model.Id, 1, model.SaveAttachments);
                }
                else if (model.Status == 2)
                {
                    attachmentsSaved = true;
                }


                if (attachmentsSaved)
                {
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;

        }

        public async Task<bool> UpdateNumberOfSeats(int NumberOfSeats)
        {
            var configModel = await _context.Configuration.FirstOrDefaultAsync(x=>x.Id == 1);

            if (configModel.IsNotNullOrEmpty())
            {
                configModel.NumberOfSeats = NumberOfSeats;
                _context.Configuration.Update(configModel);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}

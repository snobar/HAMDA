using HAMDA.Data;
using HAMDA.Models.EntityModels.Enum;
using HAMDA.Repository.IService;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace HAMDA.Service.Service
{
    public class Costumer : ICostumer
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IAttachmentService _attachmentService;
        public Costumer(ApplicationDbContext context, IEmailSender emailSender, IAttachmentService attachmentService)
        {
            _context = context;
            _emailSender = emailSender;
            _attachmentService = attachmentService;
        }

        public async Task<bool> AddCostumer(HAMDA.Models.ViewModels.RegisterCostumerModel Model)
        {
            var EntityModel = new HAMDA.Models.EntityModels.Costumer.CostumerRegister
            {
                Country = Model.Country,
                Email = Model.Email,
                Phone = Model.Phone,
                Username = Model.Username,
                Status = (int)Status.Pending,
            };

            var returnedResponse = await _context.Costumers.AddAsync(EntityModel);

            if (await _context.SaveChangesAsync() > 0)
            {                
                var htmlMessage = new StringBuilder();
                htmlMessage.Append($"<h3>Dear {Model.Username},</h3><br>");
                htmlMessage.Append($"<p>Thank you for getting in touch. We will reach out to you shortly.</p><br>");
                htmlMessage.Append($"<p>Best regards,<br>HAMDA Team</p><br>");

                 await _emailSender.SendEmailAsync(Model.Email, "Thank you for your registry at HAMDA", htmlMessage.ToString());
                return true;
            }

            return false;
        }

        public async Task<int> GetActiveCostumersCount()
        {
            var count = await _context.Costumers.CountAsync(x=>x.Status == 1);

            return count;
        }

        public async Task<bool> UpdateActiveCostumersToOld()
        {
            var allActive = _context.Costumers.Where(x=>x.Status == 1);

            foreach (var item in allActive)
            {
                item.Status = 4;
            }
            _context.Costumers.UpdateRange(allActive);

            var IsUpdated = await _context.SaveChangesAsync() > 0;

            return IsUpdated;
        }
    }
}

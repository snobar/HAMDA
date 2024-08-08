using HAMDA.Data;
using HAMDA.Models.EntityModels.Enum;
using HAMDA.Repository.IService;
using Microsoft.AspNetCore.Identity.UI.Services;
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
                bool attachmentsSaved = await _attachmentService.SaveAttachmentsAsync(EntityModel.Id,1,Model.Attachments);

                var htmlMessage = new StringBuilder();
                htmlMessage.Append($"<h3>Dear {Model.Username},</h3><br>");
                htmlMessage.Append($"<p>Thank you for getting in touch. We will reach out to you shortly.</p><br>");
                htmlMessage.Append($"<p>Best regards,<br>HAMDA Team</p><br>");

                 await _emailSender.SendEmailAsync(Model.Email, "Thank you for your registry at HAMDA", htmlMessage.ToString());
                return true;
            }

            return false;
        }
    }
}

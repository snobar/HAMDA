using HAMDA.Core.Extentions;
using HAMDA.Repository.IService;
using Microsoft.AspNetCore.Mvc;

namespace HAMDA.Controllers
{
    public class AttachmentController : AuthorizedController
    {
        private readonly IAttachmentService _attachmentService;

        public AttachmentController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Download(long Id)
        {
            var attachment = await _attachmentService.GetAttachmentAsync(Id);
            if (attachment is null)
            {
                return Redirect(Request.Headers["Referer"].ToString() ?? "/");
            }

            var fileBytes = Convert.FromBase64String(attachment.Content);
            return File(fileBytes, attachment.MimeType, attachment.FileName);
        }
    }
}

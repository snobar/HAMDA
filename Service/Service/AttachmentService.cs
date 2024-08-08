using HAMDA.Data;
using HAMDA.Repository.IService;
using Microsoft.AspNetCore.Http;
using HAMDA.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using HAMDA.Core.Extentions;

namespace HAMDA.Service.Service
{
    public class AttachmentService : IAttachmentService
    {
        private readonly ApplicationDbContext _context;

        public AttachmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveAttachmentsAsync(long objectId, int categoryId, List<IFormFile> files)
        {
            foreach (var file in files)
            {
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    var base64Content = Convert.ToBase64String(ms.ToArray());

                    var attachment = new HAMDA.Models.EntityModels.System.Attachment
                    {
                        ObjectId = objectId,
                        CategoryId = categoryId,
                        FileName = file.FileName,
                        MimeType = file.ContentType,
                        Content = base64Content,
                    };

                    await _context.Attachments.AddAsync(attachment);
                }
            }
            return (await _context.SaveChangesAsync()>0);
        }

        public async Task<Attachment> GetAttachmentAsync(long Id)
        {
            var entityModel = await _context.Attachments.FirstOrDefaultAsync(x => x.Id == Id);
            if (entityModel.IsNotNullOrEmpty())
            {
                var returnedModel = new Attachment
                {
                    Id = entityModel.Id,
                    ObjectId = entityModel.ObjectId,
                    CategoryId = entityModel.CategoryId,
                    FileName = entityModel.FileName,
                    Content = entityModel.Content,
                    MimeType = entityModel.MimeType,
                };

                return returnedModel;
            }

            return null;
        }
        
        public async Task<List<Attachment>> GetAttachmentsAsync(long objectId, int categoryId)
        {
            var entityModel = await _context.Attachments.Where(x => x.ObjectId == objectId && x.CategoryId == categoryId).Select(x =>
            new Attachment
            {
                Id = x.Id,
                ObjectId = objectId,
                CategoryId = categoryId,
                FileName = x.FileName,
                Content = x.Content,
                MimeType = x.MimeType,
            }).ToListAsync();
            if (entityModel.IsNotNullOrEmpty())
            {
                return entityModel;
            }
            return null;
        }


        public async Task<int> GetAttachmentsCountAsync(long objectId, int categoryId)
        {
            return await _context.Attachments.CountAsync(x => x.ObjectId == objectId && x.CategoryId == categoryId);
        }
    }
}

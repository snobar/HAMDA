using HAMDA.Data;
using HAMDA.Repository.IService;
using Microsoft.AspNetCore.Http;
using HAMDA.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using HAMDA.Core.Extentions;
using Microsoft.AspNetCore.Hosting;

namespace HAMDA.Service.Service
{
    public class AttachmentService : IAttachmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly string _attachmentsDirectory;

        public AttachmentService(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;

            _attachmentsDirectory = Path.Combine(environment.ContentRootPath, "Attachments");

            // Ensure the directory exists
            if (!Directory.Exists(_attachmentsDirectory))
            {
                Directory.CreateDirectory(_attachmentsDirectory);
            }
        }

        public async Task<bool> SaveAttachmentsAsync(long objectId, int categoryId, List<IFormFile> files)
        {
            foreach (var file in files)
            {
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);

                    var base64Content = Convert.ToBase64String(ms.ToArray());
                    var FileId = Guid.NewGuid();

                    var filePath = Path.Combine(_attachmentsDirectory, $"{FileId}.txt");
                    await File.WriteAllTextAsync(filePath, base64Content);


                    var attachment = new HAMDA.Models.EntityModels.System.Attachment
                    {
                        ObjectId = objectId,
                        CategoryId = categoryId,
                        FileName = file.FileName,
                        MimeType = file.ContentType,
                        FileId = FileId,
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
                var filePath = Path.Combine(_attachmentsDirectory, $"{entityModel.FileId}.txt");
                string content = null;
                if (File.Exists(filePath))
                {
                    content = await File.ReadAllTextAsync(filePath);
                }

                var returnedModel = new Attachment
                {
                    Id = entityModel.Id,
                    ObjectId = entityModel.ObjectId,
                    CategoryId = entityModel.CategoryId,
                    FileName = entityModel.FileName,
                    Content = content,
                    MimeType = entityModel.MimeType,
                };

                return returnedModel;
            }

            return null;
        }
        
        public async Task<List<Attachment>> GetAttachmentsAsync(long objectId, int categoryId)
        {
            var entityModel = await _context.Attachments.Where(x => x.ObjectId == objectId && x.CategoryId == categoryId).ToListAsync();

            var attachmentList = new List<Attachment>();

            foreach (var attachment in entityModel)
            {
                var filePath = Path.Combine(_attachmentsDirectory, $"{attachment.FileId}.txt");
                string content = null;
                if (File.Exists(filePath))
                {
                    content = await File.ReadAllTextAsync(filePath);
                }

                var ss = new Attachment
                {
                    Id = attachment.Id,
                    ObjectId = objectId,
                    CategoryId = categoryId,
                    FileName = attachment.FileName,
                    Content = content,
                    MimeType = attachment.MimeType,
                };

                attachmentList.Add(ss);
            };
            
            if (attachmentList.IsNotNullOrEmpty())
            {
                return attachmentList;
            }
            return null;
        }


        public async Task<int> GetAttachmentsCountAsync(long objectId, int categoryId)
        {
            return await _context.Attachments.CountAsync(x => x.ObjectId == objectId && x.CategoryId == categoryId);
        }
    }
}

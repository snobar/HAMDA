using HAMDA.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace HAMDA.Repository.IService
{
    public interface IAttachmentService
    {
        Task<bool> SaveAttachmentsAsync(long objectId, int categoryId, List<IFormFile> files);
        Task<Attachment> GetAttachmentAsync(long Id);
        Task<List<Attachment>> GetAttachmentsAsync(long objectId, int categoryId);
        Task<int> GetAttachmentsCountAsync(long objectId, int categoryId);
    }
}

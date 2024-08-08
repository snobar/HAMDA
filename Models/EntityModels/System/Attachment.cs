using HAMDA.Models.EntityModels.Base;

namespace HAMDA.Models.EntityModels.System
{
    public class Attachment : EntityBase
    {
        public long ObjectId { get; set; }
        public int CategoryId { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public string Content { get; set; }
    }
}

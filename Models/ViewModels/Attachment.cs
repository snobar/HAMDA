
using System.IO.Compression;

namespace HAMDA.Models.ViewModels
{
    public class Attachment
    {
        public long Id { get; set; }
        public long ObjectId { get; set; }
        public int CategoryId { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public string Content { get; set; }
    }
}

using HAMDA.Models.ViewModels.Base;

namespace HAMDA.Models.ViewModels
{
    public class AdminDashboardModel :hFilterModel
    {
        public int CurrentStatus { get; set; }
        public List<AdminDashboarditems> lstItems { get; set; }
    }

    public class AdminDashboarditems
    {
        public int CurrentStatus { get; set; }
        public long Id { get; set; }
        public string Username { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int AttachmentsCount { get; set; }

        public List<Attachment> Attachments { get; set; }
    }
}

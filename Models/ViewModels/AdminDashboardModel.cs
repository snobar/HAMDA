namespace HAMDA.Models.ViewModels
{
    public class AdminDashboardModel
    {
        public int totalCount { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int totalPages => (int)Math.Ceiling(totalCount / (double)pageSize);
        public bool HasPreviousPage => pageNumber > 1;
        public bool HasNextPage => pageNumber < totalPages;
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

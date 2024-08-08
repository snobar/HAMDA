using HAMDA.Models.ViewModels;

namespace HAMDA.Repository.IService
{
    public interface IAdminDashboard
    {
        Task<AdminDashboardModel> GetAllAsync(int status, int pageNumber, int pageSize);

        Task<AdminDashboarditems> Details(int Id);

        Task<bool> MakeAction(long Id, int Status,Guid UserId);
    }
}

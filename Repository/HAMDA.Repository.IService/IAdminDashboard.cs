using HAMDA.Models.ViewModels;

namespace HAMDA.Repository.IService
{
    public interface IAdminDashboard
    {
        Task<AdminDashboardModel> GetAllAsync(int status, int pageNumber, int pageSize, string hSearch);

        Task<AdminDashboarditems> Details(int Id);

        Task<bool> MakeAction(UpdateCostumerModel model, Guid UserId);

        Task<bool> UpdateNumberOfSeats(int NumberOfSeats);
    }
}

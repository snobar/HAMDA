namespace HAMDA.Repository.IService
{
    public interface ICostumer
    {
        Task<(bool response, bool isValid)> AddCostumer(HAMDA.Models.ViewModels.RegisterCostumerModel Model);
        Task<int> GetActiveCostumersCount();
        Task<bool> UpdateActiveCostumersToOld();
    }
}

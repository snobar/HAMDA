namespace HAMDA.Repository.IService
{
    public interface ICostumer
    {
        public Task<bool> AddCostumer(HAMDA.Models.ViewModels.RegisterCostumerModel Model);
        Task<int> GetActiveCostumersCount();
        Task<bool> UpdateActiveCostumersToOld();
    }
}

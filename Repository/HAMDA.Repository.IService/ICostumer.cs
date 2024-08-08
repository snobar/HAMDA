namespace HAMDA.Repository.IService
{
    public interface ICostumer
    {
        public Task<bool> AddCostumer(HAMDA.Models.ViewModels.RegisterCostumerModel Model);
    }
}

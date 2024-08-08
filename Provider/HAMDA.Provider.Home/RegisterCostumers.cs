using HAMDA.Core.Extentions;
using HAMDA.Models.DataLayer;
using HAMDA.Models.SystemCore;

namespace HAMDA.Provider.Home
{
    public class RegisterCostumers : Disposer
    {
        public bool RegisterCostumer(HAMDA.Models.ViewModels.RegisterCostumerModel Model)
        {
            if (Model.IsNotNullOrEmpty())
            {
                using var _dal = new DAL();
                _dal.dynamicParameters.Add("Username", Model.Username);
                _dal.dynamicParameters.Add("Country", Model.Country);
                _dal.dynamicParameters.Add("Phone", Model.Phone);
                _dal.dynamicParameters.Add("Email", Model.Email);
                _dal.dynamicParameters.Add("Status", (int)HAMDA.Models.EntityModels.Enum.Status.Pending);

                var response = _dal.ExecuteNonQuery("dbo.AddCustomer");

                return response == 0;
            }
            return false;
        }
    }
}

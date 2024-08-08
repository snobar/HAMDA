using HAMDA.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace HAMDA.Repository.IService
{
    public interface IAccountService
    {
        Task<IdentityResult> AddAdmin(AddAdminModel model);
    }
}

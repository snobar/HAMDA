using HAMDA.Models.ViewModels;
using HAMDA.Repository.IService;
using Microsoft.AspNetCore.Identity;

namespace HAMDA.Service.Service
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> AddAdmin(AddAdminModel model)
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email ,NormalizedUserName = $"{model.FirstName} {model.LastName}"};
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                await _userManager.AddToRoleAsync(user, "Admin");
            }

            return result;
        }
    }
}

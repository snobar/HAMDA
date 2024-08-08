using HAMDA.Models.ViewModels;
using HAMDA.Repository.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HAMDA.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminAccountManagerController : AuthorizedController
    {
        private readonly IAccountService _accountService;

        public AdminAccountManagerController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult AddAdmin()
        {
            return View( new AddAdminModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAdmin(AddAdminModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.AddAdmin(model);
                if (response.Succeeded)
                {
                    TempData["SuccessMessage1"] = "Admin user added successfully!";
                    return RedirectToAction("Index", "AdminDashboard");
                }
                else
                {
                    foreach (var error in response.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

    }
}

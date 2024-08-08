using HAMDA.Core.Extentions;
using HAMDA.Models.EntityModels.Enum;
using HAMDA.Models.ViewModels;
using HAMDA.Repository.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HAMDA.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : AuthorizedController
    {
        private readonly IAdminDashboard _adminDashboard;
        private readonly ICostumer _costumer;

        public AdminDashboardController(IAdminDashboard adminDashboard, ICostumer costumer)
        {
            _adminDashboard = adminDashboard;
            _costumer = costumer;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int? status = null)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            if (status.IsNotNullOrEmpty() && (status < 1 || status > 4))
            {
                status = 3;
            }

            var statusEnum = status.HasValue ? (Status)status.Value : Status.Pending;

            var data = await _adminDashboard.GetAllAsync((int)statusEnum, pageNumber, pageSize);

            data.CurrentStatus = (int)statusEnum;

            return View(data);
        }


        public async Task<IActionResult> Details(int Id)
        {
            var model = await _adminDashboard.Details(Id);
            if (model.IsNotNullOrEmpty())
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeAction(UpdateCostumerModel model)
        {
            if (model.Id.IsNotNullOrEmpty() && model.Status.IsNotNullOrEmpty())
            {
                var response = await _adminDashboard.MakeAction(model, CurrentUser.UserId);
                if (response)
                {
                    return RedirectToAction("Details", new { Id = model.Id });
                }
            }
            TempData["FailedMessage1"] = "Something went wrong! Please try again.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateActiveCostumersToOld()
        {
            var response = await _costumer.UpdateActiveCostumersToOld();

            return RedirectToAction("Index", new { pageNumber=1, status =4});
        }
    }
}

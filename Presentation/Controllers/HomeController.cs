using HAMDA.Core.Extentions;
using HAMDA.Repository.IService;
using Microsoft.AspNetCore.Mvc;

namespace HAMDA.Controllers
{
    public class HomeController : Controller
    {

        private readonly ICostumer _costumer;

        public HomeController(ICostumer costumer)
        {
            _costumer = costumer;
        }

        public async Task<IActionResult> Index(string errorMessage = null)
        {
            if (errorMessage.IsNotNullOrEmpty())
            {
                TempData["FailedMessage1"] = true;
                TempData["FailedMessage2"] = errorMessage;
            }

            var costumerCount = await _costumer.GetActiveCostumersCount();

            return View(new HAMDA.Models.ViewModels.RegisterCostumerModel { CostumerCount = costumerCount });
        }

        public IActionResult ThankYouPage(HAMDA.Models.ViewModels.ThankYouModel model)
        {
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistCostumer(HAMDA.Models.ViewModels.RegisterCostumerModel Model)
        {
            if (ModelState.IsValid)
            {
                var response = await _costumer.AddCostumer(Model);
                if (response.response && response.isValid)
                {
                    return RedirectToAction("ThankYouPage", new HAMDA.Models.ViewModels.ThankYouModel { UserName = Model.Username });
                }

                if (!response.isValid)
                {
                    TempData["FailedMessage1"] = true;
                    TempData["FailedMessage2"] = "waitForApproval";
                }

            }

            var costumerCount = await _costumer.GetActiveCostumersCount();
            Model.CostumerCount = costumerCount;
            return View("Index", Model);
        }
    }
}

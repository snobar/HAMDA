using HAMDA.Core.Extentions;
using HAMDA.Models.ViewModels;
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
                TempData["FailedMessage1"] = errorMessage;
            }

            var costumerCount = await _costumer.GetActiveCostumersCount();

            return View(new RegisterCostumerModel { CostumerCount = costumerCount });
        }

        public IActionResult ThankYouPage(ThankYouModel model)
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
                if (response)
                {
                    return RedirectToAction("ThankYouPage", new ThankYouModel { UserName = Model.Username });
                }
            }

            var costumerCount = await _costumer.GetActiveCostumersCount();
            Model.CostumerCount = costumerCount;
            return View("Index", Model);
        }
    }
}

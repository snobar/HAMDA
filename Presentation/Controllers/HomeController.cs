using HAMDA.Core.Extentions;
using HAMDA.Models;
using HAMDA.Repository.IService;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HAMDA.Controllers
{
    public class HomeController : Controller
    {

        private readonly ICostumer _costumer;

        public HomeController(ICostumer costumer)
        {
            _costumer = costumer;
        }

        public IActionResult Index(string errorMessage = null)
        {
            if (errorMessage.IsNotNullOrEmpty())
            {
                TempData["FailedMessage1"] = errorMessage;
            }
            return View();
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
                    TempData["SuccessMessage2"] = "You have registered successfully!, we will reach you out soon.";
                }
            }
            return View("Index");
        }
    }
}

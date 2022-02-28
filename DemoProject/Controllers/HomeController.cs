using DemoProject.IRepository;
using DemoProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.Controllers
{
    public class HomeController : Controller
    {
        private IConfigurationRepository _confRepo = null;

        public HomeController(IConfigurationRepository confRepo)
        {
            _confRepo = confRepo;
        }

        /// <summary>
        /// Configuration page.
        /// </summary>
        public IActionResult Configurations()
        {
            //var user = HttpContext.Session.GetString("sessionName");
            //if (user != null)
            //{
            //    ViewData["Username"] = user;
            //    return View();
            //}
            //return RedirectToAction("Login", "Account");
            return View();
        }

        /// <summary>
        /// Returns the building information list.
        /// </summary>
        /// <returns>JsonResult</returns>
        public JsonResult GetConfigurations()
        {
            var configurations = _confRepo.Gets();
            return Json(configurations);
        }

        /// <summary>
        /// Save the building informations.
        /// </summary>
        /// <returns>JsonResult</returns>
        public JsonResult SaveConfiguration(Configuration configuration)
        {
            var conf = configuration;
            if (configuration.BuildingCost > 0)
            {
                if((configuration.ConstructionTime >= 30) && (configuration.ConstructionTime < 1800))
                {
                    conf = _confRepo.Save(configuration);
                }
            }
            return Json(conf);
        }

        /// <summary>
        /// Delete the building information.
        /// </summary>
        /// <returns>JsonResult</returns>
        public JsonResult DeleteConfiguration(string confId)
        {
            bool isOk = _confRepo.Delete(confId);
            if (isOk)
            {
                return Json("Deleted");
            }
            return Json("Error");
        }

    }
}

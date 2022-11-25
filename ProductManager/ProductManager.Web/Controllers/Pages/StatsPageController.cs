using Microsoft.AspNetCore.Mvc;

namespace ProductManager.Web.Controllers.Pages
{
    public class StatsPageController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}

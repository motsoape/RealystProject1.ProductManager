using Microsoft.AspNetCore.Mvc;

namespace ProductManager.Web.Controllers
{
    public class StatsPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

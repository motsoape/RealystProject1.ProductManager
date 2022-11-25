using Microsoft.AspNetCore.Mvc;

namespace ProductManager.Web.Controllers.Pages
{
    public class ProductsPageController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}

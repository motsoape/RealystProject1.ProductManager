using Microsoft.AspNetCore.Mvc;

namespace ProductManager.Web.Controllers
{
    public class ProductsPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace ProductManager.Web.Controllers.Pages
{
    public class ProductsPageController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            return View((object)baseUrl);
        }
    }
}

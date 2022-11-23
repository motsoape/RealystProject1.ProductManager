using Microsoft.AspNetCore.Mvc;

namespace ProductManager.Web.Controllers
{
    public class CommentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

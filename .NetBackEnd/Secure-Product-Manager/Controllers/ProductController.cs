using Microsoft.AspNetCore.Mvc;

namespace Secure_Product_Manager.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

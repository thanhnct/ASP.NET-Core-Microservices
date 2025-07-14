using Microsoft.AspNetCore.Mvc;

namespace Product.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}

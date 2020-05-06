using Microsoft.AspNetCore.Mvc;

namespace HyraPokedex.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
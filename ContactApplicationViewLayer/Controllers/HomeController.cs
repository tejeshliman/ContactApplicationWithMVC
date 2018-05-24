using System.Web.Mvc;

namespace ContactApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public RedirectToRouteResult Index()
        {
            ViewBag.Title = "Home Page";

            return RedirectToAction("Index");
        }
    }
}

using System.Web.Mvc;

namespace SinglePageApp.Controllers
{
    public class ApplicationController : Controller
    {
        public ActionResult Start()
        {
            return View();
        }
    }
}

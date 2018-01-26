using System.Web.Mvc;

namespace InventorAccessPortal.Web.Controllers
{
    public class HomeController : InventorAccessPortalControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
using System.Web.Mvc;

namespace InventorAccessPortal.Web.Controllers
{
    public class AboutController : InventorAccessPortalControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
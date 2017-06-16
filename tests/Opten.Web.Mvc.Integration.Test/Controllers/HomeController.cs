using System.Web.Mvc;

namespace Opten.Web.Mvc.Integration.Test.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}
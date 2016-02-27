using System;
using System.Web.Mvc;

namespace WebApplication10.Controllers
{
	public class HomeController : Controller
	{
		private Itest1 _test1;
		private Isingletontest2 _isingletontest2;
		private IrequestClass _irequestClass;
		private ISessionClass _iSessionClass;

		public HomeController(Itest1 test1, Isingletontest2 isingletontest2, IrequestClass irequestClass, ISessionClass iSessionClass)
		{
			_test1 = test1;
			_isingletontest2 = isingletontest2;
			_irequestClass = irequestClass;
			_iSessionClass = iSessionClass;

			_test1.variable1 = "5";
			_isingletontest2.variable2 = "15";
			_irequestClass.variable = "22";
			_iSessionClass.variable = Guid.NewGuid().ToString();

		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}
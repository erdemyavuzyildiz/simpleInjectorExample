using System.Reflection;
using System.Web;
using System.Web.Mvc;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

namespace WebApplication10
{
	public static class SimpleInjectorDependencyManager
	{
			public static Container Container;

		public static  Container DependencyInjectionSetup()
		{
			var container = new Container();
			container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
			Register(container);
			RegisterForSession(container);

			// This is an extension method from the integration package.
			container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

			// This is an extension method from the integration package as well.
			container.RegisterMvcIntegratedFilterProvider();

			container.Verify();

			DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

			return container;
		}

		public  static void Register(Container container)
		{
			// Register your types, for instance:
			container.Register<Itest1>(test1.instance, Lifestyle.Scoped);
			container.Register<IrequestClass, RequestClass>(Lifestyle.Scoped);
			container.Register<Isingletontest2, isingletontest2>(Lifestyle.Singleton);
		}

		public static  void RegisterForSession(Container container)
		{
			container.Register<ISessionClass, SessionClass>(LifeStyleSession);
		}

				private static readonly Lifestyle LifeStyleSession = Lifestyle.CreateCustom("Session Based Lifetime", instanceCreator =>
   {
	   var syncRoot = new object();

	   // If the application has multiple registrations using this lifestyle, each registration
	   // will get its own Func<object> delegate (created here) and therefore get its own set
	   // of variables as defined above.
	   return () =>
		  {
			  lock (syncRoot)
			  {
				  if (HttpContext.Current == null || HttpContext.Current.Session == null)
				  {
					  return null;
				  }

				  var instanceInSession = HttpContext.Current.Session["SessionBasedDIContainerLifestyleObject"];
				  if (instanceInSession != null)
				  {
					  return instanceInSession;
				  }

				  var newInstance = instanceCreator();
				  HttpContext.Current.Session["SessionBasedDIContainerLifestyleObject"] = newInstance;
				  return newInstance;
			  }
		  };
   });
	}
}
using NH4CookbookHelpers.Model;
using NHibernate;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ActionFilterExample
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static ISessionFactory SessionFactory
        {
            get;
            private set;
        }

        protected void Application_Start()
        {
            SessionFactory=ProductModel.CreateExampleSessionFactory(true, conf =>
            {
                //conf.DataBaseIntegration(x =>
                //{
                //    x.Dialect<MsSql2008Dialect>();
                //    x.Driver<MiniProfilerSql2008ClientDriver>();
                //    x.ConnectionStringName = "db";
                //});

                conf.SetProperty("current_session_context_class", "web");
            });
            NHibernate.Glimpse.Plugin.RegisterSessionFactory(SessionFactory);
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(
  object sender,
  EventArgs e)
        {
            //var session = SessionFactory.OpenSession();
            //CurrentSessionContext.Bind(session);
        }

        protected void Application_EndRequest(
  object sender,
  EventArgs e)
        {
            //var session = CurrentSessionContext.Unbind(SessionFactory);
            //session.Dispose();
        }


    }
}

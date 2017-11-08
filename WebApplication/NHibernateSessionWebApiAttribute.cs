using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using NHibernate;
using NHibernate.Context;

namespace ActionFilterExample
{
    [AttributeUsage(AttributeTargets.Method)]
    public class NHibernateSessionWebApiAttribute
  : ActionFilterAttribute
    {
        public NHibernateSessionWebApiAttribute()
        {
            
        }
        protected ISessionFactory sessionFactory
        {
            get
            {
                return MvcApplication.SessionFactory;
            }
        }

public override void OnActionExecuting(HttpActionContext actionContext)
{
    var session = sessionFactory.OpenSession();
    CurrentSessionContext.Bind(session);
}

public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
{
    var session = CurrentSessionContext.Unbind(sessionFactory);
    session.Close();
}

      
    }

}
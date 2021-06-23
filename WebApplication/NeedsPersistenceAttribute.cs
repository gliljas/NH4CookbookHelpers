using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;

namespace ActionFilterExample
{
[AttributeUsage(AttributeTargets.Method,
AllowMultiple = true)]
public class NeedsPersistenceAttribute
: NHibernateSessionAttribute
{

    protected ISession session
    {
        get
        {
            return sessionFactory.GetCurrentSession();
        }
    }

    public override void OnActionExecuting(
        ActionExecutingContext filterContext)
    {
        base.OnActionExecuting(filterContext);
        session.BeginTransaction();
    }

    public override void OnActionExecuted(
        ActionExecutedContext filterContext)
    {
            
        var tx = session.GetCurrentTransaction();
        if (tx != null && tx.IsActive)
        {
            var noUnhandledException = filterContext.Exception == null || filterContext.ExceptionHandled;
            if (noUnhandledException && filterContext.Controller.ViewData.ModelState.IsValid)
            {
                session.GetCurrentTransaction().Commit();
            }
            else
            {
                session.GetCurrentTransaction().Rollback();
            }
        }
            
        base.OnActionExecuted(filterContext);
    }

}

}
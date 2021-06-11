using EmployeeManagementPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EmployeeManagementPlatform.Utils
{
	public class AccessControl : AuthorizeAttribute, IAuthorizationFilter
	{
		int right;
		public AccessControl(int right)
		{
			this.right = right;
		}

		override
		public void OnAuthorization(AuthorizationContext filterContext)
		{
			bool ok = false;
			if (HttpContext.Current.Session["user"] == null)
			{
				filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index" }));
				return;
			}
			else
			{
				Employee emp = (Employee)HttpContext.Current.Session["user"];
				if (emp.IntRight == this.right)
				{
					ok = true;
				}
			}
			if (!ok)
			{
				filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "AccessForbidden" }));
			}
		}
	}
}
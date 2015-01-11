using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOA.WLIMS.Web
{
    public class CustomAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        public new string[] Roles { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("HttpContext");
            } 

            //不需要权限模块直接访问，不验证登陆
            if (Roles == null)
            {
                return true;
            }
            if (Roles.Length == 0)
            {
                return true;
            }

            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }
           
            if (Roles.Any(httpContext.User.IsInRole))
            {
                return true;
            }
            return false;
        }

        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;
            string roles = GetRoles.GetActionRoles(actionName, controllerName);
            if (!string.IsNullOrWhiteSpace(roles))
            {
                this.Roles = roles.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                this.Roles = null;
            }
            base.OnAuthorization(filterContext);
        }
    }
}
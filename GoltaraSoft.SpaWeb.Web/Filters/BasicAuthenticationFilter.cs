using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;

namespace GoltaraSolutions.SpaWeb.Web.Filters
{
    public class BasicAuthenticationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (SkipAuthorization(filterContext))
            {
                return;
            }

            if (!Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                filterContext.Result =
                    new RedirectToRouteResult(
                        new RouteValueDictionary()
                        {
                            { "controller", "Account" },
                            { "action", "Login" }
                        }
                    );

                return;
            }

            if (!IsInRole(filterContext))
            {

            }
        }

        private static bool SkipAuthorization(AuthorizationContext filterContext)
        {
            return filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any()
                    || filterContext.Controller.GetType().GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any();
        }

        private static bool IsInRole(AuthorizationContext filterContext)
        {
            AuthorizeAttribute actionAttr = (AuthorizeAttribute)filterContext.ActionDescriptor.GetCustomAttributes(typeof(AuthorizeAttribute), true).FirstOrDefault();
            AuthorizeAttribute controllerAttr = (AuthorizeAttribute)filterContext.Controller.GetType().GetCustomAttributes(typeof(AuthorizeAttribute), true).FirstOrDefault();

            // Se não possui DataAnnotation específico para separar por Role, retorna OK
            if (actionAttr == null && controllerAttr == null)
            {
                return true;
            }

            // Separa Roles em um array de string
            string[] actionRoles = new string[] { };
            if (actionAttr != null)
            {
                actionRoles = actionAttr.Roles.Split(',');
            }

            string[] controllerRoles = new string[] { };
            if (controllerAttr != null)
            {
                controllerRoles = controllerAttr.Roles.Split(',');
            }

            // Não há Role específica então pode continuar
            if (!actionRoles.Any() && !controllerRoles.Any())
            {
                return true;
            }

            // Checa roles para saber se ele está na role determinada pela ACTION
            foreach (string role in actionRoles)
            {
                if (Thread.CurrentPrincipal.IsInRole(role))
                {
                    return true;
                }
            }

            // Checa roles para saber se ele está na role determinada pelo CONTROLLER
            foreach (string role in controllerRoles)
            {
                if (Thread.CurrentPrincipal.IsInRole(role))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
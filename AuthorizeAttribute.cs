using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Security.Claims;

namespace wwe
{
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute(params string[] role) : base(typeof(AuthorizeFilter))
        {
            Arguments = new object[] { role };
        }
    }

    public class AuthorizeFilter : IAuthorizationFilter
    {
        readonly string[] _role;

        public AuthorizeFilter(params string[] role)
        {
            _role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var IsAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
            var claimsIndentity = context.HttpContext.User.Identity as ClaimsIdentity;

            if (IsAuthenticated)
            {
                bool flagClaim = false;
                foreach (var item in _role)
                {
                    if (context.HttpContext.User.HasClaim(ClaimTypes.Role, item))
                        flagClaim = true;
                }
                if (!flagClaim)
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized; //Set HTTP 401
                    context.Result = new JsonResult("NotAuthorized") { StatusCode = 401, Value = new { Message = "Not allowed to access!" } };
                }
            }
            else
            if (!IsAuthenticated)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden; //Set HTTP 403
                context.Result = new JsonResult("NotAuthorized") { StatusCode = 403, Value = new { Message = "Empty or Invalid Token" } };
            }
            return;
        }

    }

}

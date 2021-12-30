using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using FinancialTracker.Core.Lib.CoreServices;

namespace FinanceTrackerAPI.Filters
{
    public class Authorization : AuthorizationFilterAttribute
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ISessionService _sessionService;
        public Authorization()
        {
            this.httpContextAccessor = DependencyResolver.Current.GetService<IHttpContextAccessor>();
            this._sessionService = DependencyResolver.Current.GetService<ISessionService>();
        }

        public override void OnAuthorization(HttpActionContext filterContext)
        {
            var account = httpContextAccessor.HttpContext.Items["User"];
            

            if (account == null || string.IsNullOrEmpty(filterContext.Request.Headers.FirstOrDefault(X=>X.Key=="Authorization").Value.ToString()))
            {
                filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                return;
            }
            else if (string.IsNullOrEmpty(_sessionService.GetUsername()))
            {
                _sessionService.SetUsername(httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(X => X.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
            }
        }
    }
}

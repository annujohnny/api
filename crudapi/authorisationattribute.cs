using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace crudapi
{
    public class AuthorizationRequiredAttribute : ActionFilterAttribute
    {
        private const string token = "token";
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var provider = new BusinessService();
            if (actionContext.Request.Headers.Contains(token))
            {
                var tokenvalue = actionContext.Request.Headers.GetValues(token).First();
                if (provider != null && !provider.ValidateToken(tokenvalue))
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Invalid Request " };
                    actionContext.Response = responseMessage;
                }
            }
            else
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }


            base.OnActionExecuting(actionContext);
        }

    }
}
using crudapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace crudapi.Controllers
{
    public class loginController : ApiController
    {
        BusinessService b = new BusinessService();
        
             [Route("auth")]
        private HttpResponseMessage getauth(string email,string password)
        {
            var token = b.generatetoken(email, password);
            var response = Request.CreateResponse(HttpStatusCode.OK, "authorised");
            response.Headers.Add("Token", token.authtoken);
            response.Headers.Add("Tokenexpiry", DateTime.Now.AddHours(2).ToString());
            response.Headers.Add("Acess-Control-Expose-Headers", "Token,Tokenexpiry");
            return response;
                
        }
        [Route("authenticate")]
        [HttpGet]
        public HttpResponseMessage Authenticate(string email, string password)
        {
            return getauth(email, password);
        }

        
        [AuthorizationRequired]
        [Route("GetUserAgain")]
        public string GetUserDetails(string email)
        {
            return "Invalid";
        }

    }
}

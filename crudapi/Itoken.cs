using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using crudapi.Models;

namespace crudapi
{
    interface Itoken
    {

         profile generatetoken(string email , string password);
        profile ValidateToken(string tokenvalue);
    }
}

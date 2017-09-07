using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using crudapi.Models;
using System.Net.Http;

namespace crudapi
{
    public class BusinessService :Itoken
    {
        public profile generatetoken(string email, string password)
        {
            string token = Guid.NewGuid().ToString();
            DateTime createdon = DateTime.Now;
            DateTime expiredon = DateTime.Now.AddSeconds(50000000);
            var tokendomain = new profile
            {
                email = email,
                authtoken = token,
                createdon = createdon,
                expiredon = expiredon
        };
            profile tk = new profile();
            annuEntities3 sd = new annuEntities3();
            tk = sd.profiles.Where(x => x.email == email & x.password == password).FirstOrDefault();
            if (tk.email != null)
            {
                tk.authtoken = tokendomain.authtoken;
                tk.createdon = tokendomain.createdon;
                tk.expiredon = tokendomain.expiredon;
                sd.SaveChanges();
            }
            return tokendomain;
        }


        internal bool ValidateToken(string tokenValue)
        {
            throw new NotImplementedException();
        }

        profile Itoken.ValidateToken(string tokenValue)
        {
            throw new NotImplementedException();
        }

    }

    }

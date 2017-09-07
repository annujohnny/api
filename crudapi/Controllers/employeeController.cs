using crudapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace crudapi.Controllers
{
    public class employeeController : ApiController
    {
        // GET: employee
        [HttpPost][Route("create")]
        public IHttpActionResult postempolyee(profile pro)
        {
            if (!ModelState.IsValid)
                return BadRequest("invalid model");
            using (var db = new annuEntities2())
            {
                db.profiles.Add(new profile()
                {
                    email = pro.email,
                    password = pro.password
                });
                db.SaveChanges();
            }
            return Ok("student added");

        }
        [Route("display")]
        public IHttpActionResult getall()
        {
            List<profile> p = new List<profile>();
            using (var db = new annuEntities2())
            {
                p = db.profiles.ToList();

            }
            if (p.Count > 0)
                return Ok(p);
            else
                return Ok("no employess");
        }
        [HttpPut][Route("update")]
        public IHttpActionResult update(profile p)
        {
            using (var db = new annuEntities2())
            {
                var pro = (from pr in db.profiles where p.id == pr.id select pr).FirstOrDefault();
                pro.email = p.email;
                pro.password = p.password;
                db.SaveChanges();

            }
            return Ok();

        }
        [HttpDelete][Route("del")]
        public IHttpActionResult delete(int id)
        {
            profile p = new profile();
            p.id = id;
            using (var db = new annuEntities2())
            {
                var pro = (from pr in db.profiles where p.id == pr.id select pr).FirstOrDefault();
                db.profiles.Remove(pro);
                db.SaveChanges();


            }
            return Ok();



        }
        [Route("getbyid")]
        public IHttpActionResult GetStudentById(int id)
        {
            profile p = new profile();
            using (var db = new annuEntities2())
            {
                p = (from pr in db.profiles where pr.id == id select pr).FirstOrDefault();
            }
            if (p== null)
                return Ok("No student with given ID found");
            else
                return Ok(p);
        }

    }
}


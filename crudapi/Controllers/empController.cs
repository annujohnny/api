using crudapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace crudapi.Controllers
{
    public class empController : Controller
    {
        // GET: emp
        public ActionResult Index()
        { IEnumerable<profile> p= null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52128/");
                var responseTask=client.GetAsync("display");
                responseTask.Wait();
                var result = responseTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<profile>>();
                    readTask.Wait();
                    p = readTask.Result;


                }
                else
                {
                    p = Enumerable.Empty<profile>();
                    ModelState.AddModelError(string.Empty, "server error");

                }



            }

            return View(p);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(profile p)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52128/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync("create", p);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(p);
        }

        public ActionResult Details(int id)
        {
            profile p = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52128/");
                //HTTP GET
                var responseTask = client.GetAsync("display?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<profile>();
                    readTask.Wait();

                    p = readTask.Result;
                }
            }

            return View(p);
        }

        public ActionResult Edit(int id)
        {
            profile p = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52128/");
                //HTTP GET
                var responseTask = client.GetAsync("getbyid?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<profile>();
                    readTask.Wait();

                    p = readTask.Result;
                }
            }

            return View(p);
        }

        [HttpPost]
        public ActionResult Edit(profile p)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52128/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync("update", p);
                putTask.Wait();


                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(p);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52128/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("p/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }
}
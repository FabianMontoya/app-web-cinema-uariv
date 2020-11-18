using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWeb_Cinema.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult createRegister(FormCollection formCollection)
        {
            var info = new personaClass()
            {
                nombre = formCollection["nombre"].ToString().ToUpper(),
                edad = int.Parse(formCollection["edad"].ToString())
            };
            ViewBag.MessageCreate = string.Concat("Se recibió correctamente al usuario ", formCollection["nombre"].ToString().ToUpper());
            return View("About");
        }
    }

    public class personaClass
    {
        public string nombre { get; set; }
        public int edad { get; set; }
    }
}
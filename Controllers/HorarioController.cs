using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWeb_Cinema.Controllers
{
    public class HorarioController : Controller
    {
        // GET: Horario
        public ActionResult Index()
        {
            return View();
        }

        // GET: Horario/Create
        public ActionResult Crear(int id)
        {
            ViewBag.Funcion = id;
            return View();
        }

        // POST: Horario/Create
        [HttpPost]
        public ActionResult Crear(FormCollection formCollection)
        {

            bool descuento = string.IsNullOrWhiteSpace(formCollection["descuento"]) ? false : true;
            int porcentaje = 0;

            if (descuento)
            {
                porcentaje = int.Parse(formCollection["porcentaje"]);
            }

            var data = new ServiceFunciones.InfoFuncionHora()
            {
                Funcion = int.Parse(formCollection["funcion"]),
                Fecha = formCollection["fecha"],
                Hora = formCollection["hora"],
                Descuento = descuento,
                PorcentajeDescuento = porcentaje,
                NumeroSillas = int.Parse(formCollection["sillas"]),
            };
            using (ServiceFunciones.FuncionesClient funciones = new ServiceFunciones.FuncionesClient())
            {
                funciones.CreateHoraFuncion(data);
            }
            return Redirect(string.Concat(Url.Content("~/Funcion/Horarios"),"/", formCollection["funcion"]));
        }

    }
}

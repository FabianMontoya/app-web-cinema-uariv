using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace AppWeb_Cinema.Controllers
{
    public class FuncionController : Controller
    {
        public ActionResult Index()
        {
            ServiceFunciones.ResultFunciones result;
            using (ServiceFunciones.FuncionesClient funciones = new ServiceFunciones.FuncionesClient())
            {
                result = funciones.ConsultFunciones();
            }

            return View(result);
        }

        public ActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Crear(FormCollection formCollection)
        {
            string nombre = formCollection["nombre"];
            int precio = int.Parse(formCollection["precio"]);
            using (ServiceFunciones.FuncionesClient funciones = new ServiceFunciones.FuncionesClient())
            {
                funciones.CreateFuncion(nombre, precio);
            }
            return Redirect(Url.Content("~/Funcion/"));
        }

        public ActionResult Horarios(int Id)
        {
            ViewBag.Funcion = Id;
            ServiceReservas.resultReservas fechasFuncion;
            using (ServiceReservas.ReservasClient funciones = new ServiceReservas.ReservasClient())
            {
                fechasFuncion = funciones.ConsultReservasFuncion(Id);
            }

            return View(fechasFuncion);
        }

        public ActionResult Reservas(int funcion, string fecha, string hora)
        {
            ViewBag.Funcion = funcion;
            ViewBag.Fecha = fecha;
            ViewBag.Hora = hora;
            ServiceReservas.resultReservasHora reservas;
            using (ServiceReservas.ReservasClient funciones = new ServiceReservas.ReservasClient())
            {
                reservas = funciones.ConsultReservasFuncionHora(funcion, fecha, hora);
            }

            return View(reservas);
        }
    }

}
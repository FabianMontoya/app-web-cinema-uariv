using AppWeb_Cinema.ServiceReservas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWeb_Cinema.Controllers
{
    public class ReservasController : Controller
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

        // GET: Reservas/Create
        public ActionResult Reservar(int funcion, string costo)
        {
            ViewBag.Funcion = funcion;

            byte[] data = System.Convert.FromBase64String(costo);
            ViewBag.CostoFuncion = System.Text.ASCIIEncoding.ASCII.GetString(data);

            ServiceFunciones.ResultFechas fechasFuncion;
            using (ServiceFunciones.FuncionesClient funciones = new ServiceFunciones.FuncionesClient())
            {
                fechasFuncion = funciones.ConsultFechasFuncion(funcion);
            }
            return View(fechasFuncion);
        }

        // POST: Reservas/Create
        [HttpPost]
        public ActionResult Reservar(FormCollection collection)
        {
            try
            {
                ServiceReservas.infoReserva data = new infoReserva()
                {
                    Funcion = int.Parse(collection["funcion"]),
                    Fecha = collection["fecha"],
                    Hora = collection["hora"],
                    Silla = int.Parse(collection["silla"]),
                    Documento = collection["documento"],
                    Nombre = collection["nombre"],
                    Telefono = collection["telefono"],
                    ValorPaga = int.Parse(collection["total"])
                };

                using (ServiceReservas.ReservasClient reservas = new ServiceReservas.ReservasClient())
                {
                    reservas.CreateReserva(data);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public JsonResult HorariosFecha(int funcion, string fecha)
        {
            ServiceFunciones.ResultHoras horasFecha;
            using (ServiceFunciones.FuncionesClient funciones = new ServiceFunciones.FuncionesClient())
            {
                horasFecha = funciones.ConsultHorasFuncion(funcion, fecha);
            }

            return Json(horasFecha, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ReservasFuncion(int funcion, string fecha, string hora)
        {
            ServiceFunciones.ResultSillas sillas;
            ServiceReservas.resultReservasHora reservasHechas;

            using (ServiceFunciones.FuncionesClient funciones = new ServiceFunciones.FuncionesClient())
            {
                sillas = funciones.ConsultSillasFuncion(funcion, fecha, hora);
            }
            using (ServiceReservas.ReservasClient reservas = new ServiceReservas.ReservasClient())
            {
                reservasHechas = reservas.ConsultReservasFuncionHora(funcion, fecha, hora);
            }

            resultReservasFuncion result = new resultReservasFuncion();
            result.sillas = sillas;
            result.reservasHechas = reservasHechas;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SillasFuncion(int funcion, string fecha, string hora)
        {
            ServiceFunciones.ResultSillas sillasFuncion;
            using (ServiceFunciones.FuncionesClient funciones = new ServiceFunciones.FuncionesClient())
            {
                sillasFuncion = funciones.ConsultSillasFuncion(funcion, fecha, hora);
            }

            return Json(sillasFuncion, JsonRequestBehavior.AllowGet);
        }

        internal class resultReservasFuncion
        {
            public ServiceFunciones.ResultSillas sillas { get; set; }
            public ServiceReservas.resultReservasHora reservasHechas { get; set; }

        }
    }
}

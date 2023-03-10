using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class LugarVendidoController : Controller
    {
        // GET: LugarVendido
        public ActionResult GetAll()
        {
            ML.Evento evento = new ML.Evento();
            evento.Eventos = new List<object>();
            ML.Result result = BL.Evento.GetAllEvento();
            if (result.Correct)
            {
                evento.Eventos = result.Objects.ToList();
            }
            return View(evento);
        }
        [HttpGet]
        public ActionResult Vista(int? idEvento)
        {
            ML.Result resultLugares = BL.Lugar.GetAllLugar();
            if (resultLugares.Correct)
            {
                ML.Venta venta = new ML.Venta();
                venta.Evento = new ML.Evento();
                venta.Evento.Lugar = new ML.Lugar();
                venta.Evento.Lugar.Lugares = resultLugares.Objects;
                if (idEvento == null) //Error
                {
                    ViewBag.Mensaje = "Evento no reconocido";
                }
                else //View Evento y lugar de asientos disponibles
                {
                    ML.Result resultEvento = BL.Evento.GetByIdEvento(idEvento.Value);
                    if (resultEvento.Correct)
                    {
                        venta.Evento = ((ML.Evento)resultEvento.Object);
                        venta.Evento.Lugar.Lugares = resultLugares.Objects;
                        venta.Ventas = BL.Venta.GetLugarVendido(idEvento.Value).Objects;
                        return View(venta);
                    }
                }
            }
            return PartialView("Modal");
        }
    }
}
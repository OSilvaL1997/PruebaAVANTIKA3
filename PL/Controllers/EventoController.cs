﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class EventoController : Controller
    {
        // GET: Evento
        //[HttpGet]
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


        [HttpPost]
        public ActionResult Form(ML.Evento evento)
        {
            ML.Result result = new ML.Result();
            if (evento.IdEvento == 0) //add
            {
                result = BL.Evento.AddEvento(evento);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Agregado exitosamente";
                }
                else
                {
                    ViewBag.Mensaje = "No se Agrego" + result.ErrorMessage;
                }
            }
            else //Update
            {
                result = BL.Evento.UpdateEvento(evento);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Actualizado exitosamente";
                }
                else
                {
                    ViewBag.Mensaje = "No se actualizo" + result.ErrorMessage;
                }
            }


            return PartialView("Modal");
        }
        public ActionResult Delete(ML.Evento evento)
        {
            ML.Result result = BL.Evento.DeleteEvento(evento);
            if (result.Correct)
            {
                ViewBag.Mensaje = "lugar Eliminada Exitosamente";
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al eliminar la lugar" + result.ErrorMessage;
            }
            return PartialView("Modal");
        }


        public JsonResult GetLugar(ML.Lugar lugar)
        {
            var result = BL.Lugar.GetByIdLugar(lugar);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }


    }
}
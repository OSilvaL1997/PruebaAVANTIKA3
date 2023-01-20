using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class RecaudadoController : Controller
    {
        // GET: Recaudado
        public ActionResult GetAll()
        {
            ML.Venta venta = new ML.Venta();
            venta.Ventas = new List<object>();
            ML.Result result = BL.Venta.TotalVentas();
            if (result.Correct)
            {
                foreach (ML.Venta total in result.Objects)
                {
                    venta.TotalSuma = total.TotalSuma;
                }
                venta.Ventas = result.Objects.ToList();
            }
            return View(venta);
        }
    }
}
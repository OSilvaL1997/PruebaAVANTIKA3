using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public ML.Evento Evento { get; set; }
        public ML.Lugar Lugar { get; set; }
        public int NumeroAsiento { get; set; }
        public DateTime FechaOperacion { get; set; }
        public decimal Total { get; set; }
        public decimal TotalSuma { get; set; }

        public List<object> Ventas { get; set; }
    }
}

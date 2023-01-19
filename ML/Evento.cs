using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Evento
    {
        public int IdEvento { get; set; }
        public String Nombre { get; set; }
        public ML.Lugar Lugar { get; set; }
        public DateTime FechaHora { get; set; }
        public decimal Precio { get; set; }
        public List<object> Eventos { get; set; }

    }
}
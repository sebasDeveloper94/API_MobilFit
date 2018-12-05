using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobilFit_API.Models
{
    public class ReporteDesempeño
    {
        public int id_reporteDesempeño { get; set; }
        public DateTime fechaReporte { get; set; }
        public float porcentajeRutina { get; set; }
        public float tiempoEntrenamiento { get; set; }
        public float caloriasQuemadas { get; set; }
        public float kmRecorridos { get; set; }
        public float IMC { get; set; }
        public float IGC { get; set; }
        public int id_desempeño { get; set; }
        public ReporteDesempeño()
        {

        }
    }
}
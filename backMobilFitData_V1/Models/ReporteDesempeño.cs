using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backMobilFitData_V1.Models
{
    class ReporteDesempeño
    {
        public int id_reporteDesempeño { get; set; }
        public DateTime fechaReporte { get; set; }
        public DateTime tiempoEntrenamiento { get; set; }
        public decimal caloriasQuemadas { get; set; }
        public decimal porcentajeMuscular { get; set; }
        public decimal pesoEsperado { get; set; }
        public decimal kmRecorridos { get; set; }
        public decimal imcEsperado { get; set; }
        public int id_desempeño { get; set; }
        public ReporteDesempeño()
        {

        }
    }
}

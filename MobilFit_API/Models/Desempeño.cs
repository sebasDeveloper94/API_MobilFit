using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backMobilFitData_V1.Models
{
    class Desempeño
    {
        public int id_desempeño { get; set; }
        public DateTime fecha { get; set; }
        public DateTime tiempo_entrenamiento { get; set; }
        public decimal porcentaje_rutina { get; set; }
        public decimal distancia_recorrida { get; set; }
        public int id_usuario { get; set; }
        public Desempeño()
        {

        }
    }
}

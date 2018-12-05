using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilFit_API.Models
{
    public class Desempeño
    {
        public int id_desempeño { get; set; }
        public DateTime fecha { get; set; }
        public float horas_entrenamiento { get; set; }
        public decimal distancia_recorrida { get; set; }
        public int id_plan_usuario { get; set; }
        public Desempeño()
        {

        }
    }
}

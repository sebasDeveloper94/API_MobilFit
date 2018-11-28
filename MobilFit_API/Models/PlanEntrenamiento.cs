using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobilFit_API.Models
{
    public class PlanEntrenamiento
    {
        public int idPlan { get; set; }
        public string nombre { get; set; }
        public int candidadDias { get; set; }
        public int tipoPlan { get; set; }
        public int objetivo { get; set; }
        public int nivel { get; set; }
        public Profesional objPresional { get; set; }
        public List<Rutina> rutinasPlan { get; set; }
        public Usuario objUsuario { get; set; }
        public int id_planUsuario { get; set; }
        public List<DiasEntrenamiento> DiasEntrenamiento { get; set; }
        public PlanEntrenamiento()
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobilFit_API.Models
{
    public class DiasEntrenamiento
    {
        public int idPlan { get; set; }
        public int idRutina { get; set; }
        public int dia { get; set; }
        public DiasEntrenamiento()
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobilFit_API.Models
{
    public class DiasEntrenamiento
    {
        public int idPlan { get; set; }
        public int idRutinas { get; set; }
        public int dia { get; set; }
        public DiasEntrenamiento()
        {

        }
    }
}
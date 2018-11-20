using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobilFit_API.Models
{
    public class DiasEntrenamiento
    {
        public int idPlan { get; set; }
        public List<int> idRutinas { get; set; }
        public List<int> dias { get; set; }
        public DiasEntrenamiento()
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobilFit_API.Models
{
    public class DiasEntrenamiento
    {
        public int dia { get; set; }
        public PlanEntrenamiento objPlan { get; set; }
        public DiasEntrenamiento()
        {

        }
    }
}
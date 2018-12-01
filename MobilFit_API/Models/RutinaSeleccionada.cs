using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobilFit_API.Models
{
    public class RutinaSeleccionada
    {
        public DiasEntrenamiento DiaEntrenamientos { get; set; }
        public List<Ejercicio> Ejercicios { get; set; }
        public bool RutinaCompletada { get; set; }
        public RutinaSeleccionada()
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace backDataMobilFit.Models
{
    class Ejercicio
    {
        public int id_ejercicio { get; set; }
        public string nombre_ejercicio { get; set; }
        public string descripcion { get; set; }
        public int repeticiones { get; set; }
        public int series { get; set; }
        public DateTime tiempo { get; set; }
        public decimal distancia { get; set; }
        public int id_rutina { get; set; }
        public Ejercicio()
        {

        }
    }
}

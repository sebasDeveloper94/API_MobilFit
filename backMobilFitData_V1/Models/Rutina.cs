﻿using System;
using System.Collections.Generic;
using System.Text;

namespace backDataMobilFit.Models
{
    class Rutina
    {
        public int id_rutina { get; set; }
        public string nombre { get; set; }
        public string meta { get; set; }
        public int dia { get; set; }
        public int id_tipoRutina { get; set; }
        public int id_categoria { get; set; }
        public int id_nivel { get; set; }
        public Rutina()
        {

        }
    }
}

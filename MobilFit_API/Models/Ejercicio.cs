﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobilFit_API.Models
{
    public class Ejercicio
    {
        public int id_ejercicio { get; set; }
        public string nombre_ejercicio { get; set; }
        public string descripcion { get; set; }
        public int repeticiones { get; set; }
        public int series { get; set; }
        public decimal peso { get; set; }
        public DateTime tiempo { get; set; }
        public decimal distancia { get; set; }
        public decimal descanso { get; set; }
        public Ejercicio()
        {

        }
    }
}
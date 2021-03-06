﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobilFit_API.Models
{
    public class Usuario
    {
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int edad { get; set; }
        public int sexo { get; set; }
        public string email { get; set; }
        public string contraseña { get; set; }
        public DateTime fechaRegistro { get; set; }
        public decimal peso { get; set; }
        public decimal altura { get; set; }
        public int id_tipoCuerpo { get; set; }
        public int id_nivel { get; set; }
        public int id_objetivo { get; set; }

        public Usuario()
        {

        }
    }
}
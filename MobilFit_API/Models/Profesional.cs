using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobilFit_API.Models
{
    public class Profesional
    {
        public int idProfesional { get; set; }
        public string profesion { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public Profesional()
        {

        }
    }
}
using MobilFit_API.Aplicacion;
using MobilFit_API.Models;
using MobilFit_API.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MobilFit_API.Controllers
{
    public class EjerciciosController : ApiController
    {
        public EjerciciosController()
        {

        }
        public IEnumerable<Ejercicio> GetEjercicios(int id_rutina)
        {
            EjerciciosAplicacionServicios ejercicioApp = new EjerciciosAplicacionServicios(conexionSQL.cadenaConexion);
            var ejercicios = ejercicioApp.GetEjercicios(id_rutina);
            return ejercicios;
        }

    }
}

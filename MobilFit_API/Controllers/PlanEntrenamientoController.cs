using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Http;
using MobilFit_API.Aplicacion;
using MobilFit_API.Models;
using MobilFit_API.Persistencia;

namespace MobilFit_API.Controllers
{
    public class PlanEntrenamientoController : ApiController
    {
        public PlanEntrenamientoController()
        {

        }

        [AcceptVerbs("GET")]
        [HttpGet]
        public IHttpActionResult GetPlanUsuario(int id_usuario)
        {
            PlanEntrenamientoAplicacionServicios planApp = new PlanEntrenamientoAplicacionServicios(conexionSQL.cadenaConexion);
            PlanEntrenamiento objPlan = new PlanEntrenamiento();
            objPlan = planApp.AsignarRutinas(id_usuario);
            if (objPlan == null)
            {
                return NotFound();
            }
            return Ok(objPlan);
        }

        [AcceptVerbs("POST")]
        [HttpGet]
        public IHttpActionResult GuardarDias(string jsonDias)
        {
            DiasEntrenamiento objDias = new DiasEntrenamiento();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonDias));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(DiasEntrenamiento));
            objDias = serializer.ReadObject(ms) as DiasEntrenamiento;
            if (objDias != null)
            {
                PlanEntrenamientoAplicacionServicios planApp = new PlanEntrenamientoAplicacionServicios(conexionSQL.cadenaConexion);
                int guardado = planApp.GuardarDiasRutinas(objDias);
                if (guardado > 0)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [AcceptVerbs("GET")]
        [HttpGet]
        public IHttpActionResult VerDiaRutina(int idRutina, int idPlanUsuario)
        {
            PlanEntrenamientoAplicacionServicios planApp = new PlanEntrenamientoAplicacionServicios(conexionSQL.cadenaConexion);
            RutinaSeleccionada rutinaSeleccionada = new RutinaSeleccionada();
            rutinaSeleccionada = planApp.VerDiaSeleccionado(idRutina, idPlanUsuario);
            if (rutinaSeleccionada == null)
            {
                return NotFound();
            }
            return Ok(rutinaSeleccionada);
        }

        [AcceptVerbs("GET")]
        [HttpGet]
        public IHttpActionResult VerDiaSeleccionados(int idPlanUsuario)
        {
            PlanEntrenamientoAplicacionServicios planApp = new PlanEntrenamientoAplicacionServicios(conexionSQL.cadenaConexion);
            List<DiasEntrenamiento> listDias = new List<DiasEntrenamiento>();
            listDias = planApp.VerDiasSeleccionados(idPlanUsuario);
            if (listDias == null)
            {
                return NotFound();
            }
            return Ok(listDias);
        }
    }
}

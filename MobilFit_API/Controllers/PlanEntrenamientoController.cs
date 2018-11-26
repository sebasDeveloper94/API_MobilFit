﻿using System;
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
        public string GuardarDias(int idPlan, int idRutina, int dia)
        {
            PlanEntrenamientoAplicacionServicios planApp = new PlanEntrenamientoAplicacionServicios(conexionSQL.cadenaConexion);
            int guardado = planApp.GuardarDiasRutinas(idPlan, idRutina, dia);
            if (guardado > 0)
            {
                return "ok";
            }
            else
            {
                return "no";
            }
        }
    }
}

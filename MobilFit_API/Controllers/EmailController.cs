using MobilFit_API.Aplicacion;
using MobilFit_API.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MobilFit_API.Controllers
{
    public class EmailController : ApiController
    {
        [AcceptVerbs("GET")]
        [HttpGet]
        public IHttpActionResult SendEmail(string email)
        {

            LoginAplicacionServicios loginApp = new LoginAplicacionServicios(conexionSQL.cadenaConexion);
            int envio = loginApp.SendEmail(email);

            return Ok(envio);
        }
    }
}

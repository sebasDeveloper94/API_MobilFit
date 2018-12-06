using MobilFit_API.Aplicacion;
using MobilFit_API.Models;
using MobilFit_API.Persistencia;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Http;

namespace MobilFit_API.Controllers
{
    public class LoginController : ApiController
    {
        public LoginController()
        {

        }

        [AcceptVerbs("GET")]
        [HttpGet]
        public IHttpActionResult Login(string email, string password)
        {

            LoginAplicacionServicios loginApp = new LoginAplicacionServicios(conexionSQL.cadenaConexion);
            Usuario usuario = new Usuario();
            usuario = loginApp.Acceso(email, password);

            return Ok(usuario);
        }

        [AcceptVerbs("POST")]
        [HttpGet]
        public IHttpActionResult Registrar(string jsonUsuario)
        {
            Usuario objUsuario = new Usuario();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonUsuario));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Usuario));
            objUsuario = serializer.ReadObject(ms) as Usuario;
            if (objUsuario != null)
            {
                LoginAplicacionServicios loginApp = new LoginAplicacionServicios(conexionSQL.cadenaConexion);
                Usuario usuario = new Usuario();
                usuario = loginApp.RegistrarUsuario(objUsuario);
                if (usuario != null && usuario.id_usuario > 0)
                {
                    return Ok(usuario);
                }
                else
                {
                    return Ok();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [AcceptVerbs("GET")]
        [HttpGet]
        public IHttpActionResult ValidaUsuario(string email)
        {

            LoginAplicacionServicios loginApp = new LoginAplicacionServicios(conexionSQL.cadenaConexion);
            int id = loginApp.ValidarUsuario(email);

            return Ok(id);
        }

        [AcceptVerbs("GET")]
        [HttpGet]
        public IHttpActionResult EditarUsuario(int idUsuario, string jsonUsuario)
        {
            Usuario objUsuario = new Usuario();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonUsuario));
            objUsuario = JsonConvert.DeserializeObject<Usuario>(jsonUsuario);

            LoginAplicacionServicios loginApp = new LoginAplicacionServicios(conexionSQL.cadenaConexion);
            Usuario usuario = new Usuario();
            usuario = loginApp.Editar(idUsuario, objUsuario);
            if (usuario != null && usuario.id_usuario > 0)
            {
                return Ok(usuario);
            }
            else
            {
                return Ok();
            }

        }

    }
}

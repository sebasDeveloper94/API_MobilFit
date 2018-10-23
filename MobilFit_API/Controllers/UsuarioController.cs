using backMobilFitData.Aplicacion;
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
    public class UsuarioController : ApiController
    {
        public UsuarioController()
        {

        }

        public IEnumerable<Usuario> GetAllUsers()
        {
            UsuariosAplicacionServicios usuarioApp = new UsuariosAplicacionServicios(conexionSQL.cadenaConexion);
            var usuarios = usuarioApp.GetUsuarios();
            return usuarios;
        }

        public IHttpActionResult GetUsuario(int id_usuario)
        {
            UsuariosAplicacionServicios usuarioApp = new UsuariosAplicacionServicios(conexionSQL.cadenaConexion);
            Usuario objUsuario = new Usuario();
            objUsuario = usuarioApp.GetUsuario(id_usuario);
            if (objUsuario == null)
            {
                return NotFound();
            }
            return Ok(objUsuario);
        }
    }
}

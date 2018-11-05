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
        public string Login(string usuario, string contraseña) {

            LoginAplicacionServicios loginApp = new LoginAplicacionServicios(conexionSQL.cadenaConexion);
            bool acceso = loginApp.Acceso(usuario, contraseña);
            if (acceso)
            {
                return "ok";
            }
            else
            {
                return "no";
            }
        }

        [AcceptVerbs("POST")]
        [HttpGet]
        public string Registrar(string jsonUsuario)
        {
            Usuario objUsuario = new Usuario();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonUsuario));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Usuario));
            objUsuario = serializer.ReadObject(ms) as Usuario;
            if (objUsuario != null)
            {
                LoginAplicacionServicios loginApp = new LoginAplicacionServicios(conexionSQL.cadenaConexion);
                int registro = loginApp.RegistrarUsuario(objUsuario);
                if (registro > 0)
                {
                    return "ok";
                }
                else
                {
                    return "no";
                }
            }
            else
            {
                return "no";
            }
        }
    }
}

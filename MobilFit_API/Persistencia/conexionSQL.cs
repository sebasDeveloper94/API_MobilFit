using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace MobilFit_API.Persistencia
{
    public static class conexionSQL
    {
        public static string cadenaConexion { get { return WebConfigurationManager.ConnectionStrings["connString"].ToString(); } }
    }
}
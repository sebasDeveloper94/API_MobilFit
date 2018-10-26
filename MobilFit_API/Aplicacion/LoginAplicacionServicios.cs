using MobilFit_API.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilFit_API.Aplicacion
{
    class LoginAplicacionServicios
    {
        private string connection { get; set; }
        //contructor
        public LoginAplicacionServicios(string connection)
        {
            this.connection = connection;
        }
        //metodos
        public bool Acceso(string usuario, string contraseña)
        {
            SqlConnection connection = new SqlConnection(this.connection);
            SqlCommand sqlCommand;
            SqlDataReader reader;
            bool loginCorrecto = false;
            string sql = string.Empty;
            sql = string.Format("SELECT * FROM Usuario WHERE nombre_usuario  = '{0}' OR email = '{0}' AND Contraseña = '{1}'", usuario, contraseña);

            try
            {
                sqlCommand = new SqlCommand(sql, connection);
                connection.Open();
                reader = sqlCommand.ExecuteReader();
                loginCorrecto = reader.Read();
                connection.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return loginCorrecto;
        }
        public int RegistrarUsuario(Usuario objUsuario)
        {
            SqlConnection connection = new SqlConnection(this.connection);
            SqlCommand sqlCommand;
            SqlDataReader reader;

            int inserto = 0;
            DateTime fecha = objUsuario.fechaRegistro.ToString("dd-MM-yyyy") == "01-01-0001" ? DateTime.Parse("01-01-1900") : objUsuario.fechaRegistro;
            string sql = string.Empty;
            sql += "INSERT INTO Usuario (nombre, apellido_paterno, apellido_materno, email, contraseña, nombre_usuario, fecha_registro, peso, altura," +
                                    "id_tipocuerpo, id_nivel)  VALUES ('" + objUsuario.nombre + "', '" + objUsuario.apellido_paterno + "', '" + objUsuario.apellido_materno + "'" +
                                    ", '" + objUsuario.email + "', '" + objUsuario.contraseña + "', '" + objUsuario.nombre_usuario + "', '" + fecha + "'," +
                                    "" + objUsuario.peso + ", " + objUsuario.altura + ", " + objUsuario.id_tipoCuerpo + ", " + objUsuario.id_nivel + ")";
            try
            {
                sqlCommand = new SqlCommand(sql, connection);
                connection.Open();
                reader = sqlCommand.ExecuteReader();
                inserto = reader.RecordsAffected;
                connection.Close();
            }
            catch (Exception ex)
            {
                return 0;
            }
            return inserto;
        }
    }
}

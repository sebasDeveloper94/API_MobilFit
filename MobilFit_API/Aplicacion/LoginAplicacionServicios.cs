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
        public int Acceso(string email, string password)
        {
            SqlConnection connection = new SqlConnection(this.connection);
            SqlCommand sqlCommand;
            SqlDataReader reader;
            int idUsuario = 0;
            string sql = string.Empty;
            sql = string.Format("SELECT * FROM Usuario WHERE email = '{0}' AND Contraseña = '{1}'", email, password);

            sqlCommand = new SqlCommand(sql, connection);
            connection.Open();
            reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                idUsuario = int.Parse(reader["id_usuario"].ToString());
            }
            connection.Close();

            return idUsuario;
        }
        public int RegistrarUsuario(Usuario objUsuario)
        {
            SqlConnection connection = new SqlConnection(this.connection);
            SqlCommand sqlCommand;
            SqlDataReader reader;

            int idUsuario = 0;
            DateTime fecha = objUsuario.fechaRegistro.ToString("dd-MM-yyyy") == "01-01-0001" ? DateTime.Parse("01-01-1900") : objUsuario.fechaRegistro;
            string sql = string.Empty;
            sql += @"DECLARE @ULTIMO_ID INT
                    INSERT INTO Usuario (nombre, apellido_paterno, apellido_materno, sexo, email, contraseña, fecha_registro, peso, altura," +
                                    "id_tipocuerpo, id_nivel)  VALUES ('" + objUsuario.nombre + "', '" + objUsuario.apellido_paterno + "', '" + objUsuario.apellido_materno + "'" +
                                    ", " + objUsuario.sexo + ", '" + objUsuario.email + "', '" + objUsuario.contraseña + "', '" + fecha + "'," +
                                    "" + objUsuario.peso + ", " + objUsuario.altura + ", " + objUsuario.id_tipoCuerpo + ", " + objUsuario.id_nivel + ")" +
                                    "(SELECT @ULTIMO_ID = scope_identity())" +//Rescata el utlimo ID_usuario insertado para insertarlo en las tablas de relacionadas al usuario
                                    "INSERT INTO Usuario_Objetivo (id_objetivo, id_usuario) VALUES (" + objUsuario.id_objetivo + ", @ULTIMO_ID) "+
                                    "SELECT @ULTIMO_ID AS id_usuario";
            try
            {
                sqlCommand = new SqlCommand(sql, connection);
                connection.Open();
                reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    idUsuario = int.Parse(reader["id_usuario"].ToString());
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                return 0;
            }
            return idUsuario;
        }
    }
}

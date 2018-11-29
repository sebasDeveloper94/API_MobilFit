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
        public Usuario Acceso(string email, string password)
        {
            SqlConnection connection = new SqlConnection(this.connection);
            SqlCommand sqlCommand;
            SqlDataReader reader;
            Usuario objUsuario;
            string sql = string.Empty;
            sql = string.Format("SELECT * FROM Usuario WHERE email = '{0}' AND Contraseña = '{1}'", email, password);

            sqlCommand = new SqlCommand(sql, connection);
            connection.Open();
            reader = sqlCommand.ExecuteReader();
            objUsuario = new Usuario();
            while (reader.Read())
            {
                objUsuario.id_usuario = int.Parse(reader["id_usuario"].ToString());
                objUsuario.nombre = reader["nombre"].ToString();
                objUsuario.apellido_paterno = reader["apellido_paterno"].ToString();
                objUsuario.apellido_materno = reader["apellido_materno"].ToString();
                objUsuario.sexo = int.Parse(reader["sexo"].ToString());
                objUsuario.email = reader["email"].ToString();
                objUsuario.contraseña = reader["contraseña"].ToString();
                objUsuario.fechaRegistro = DateTime.Parse(reader["fecha_registro"].ToString());
                objUsuario.peso = decimal.Parse(reader["peso"].ToString());
                objUsuario.altura = decimal.Parse(reader["altura"].ToString());
                objUsuario.id_tipoCuerpo = int.Parse(reader["id_tipocuerpo"].ToString());
                objUsuario.id_nivel = int.Parse(reader["id_nivel"].ToString());
                objUsuario.id_objetivo = int.Parse(reader["id_objetivo"].ToString());
            }
            connection.Close();

            return objUsuario;
        }
        public Usuario RegistrarUsuario(Usuario usuario)
        {
            SqlConnection connection = new SqlConnection(this.connection);
            SqlCommand sqlCommand;
            SqlDataReader reader;

            Usuario objUsuario;
            DateTime fecha = usuario.fechaRegistro.ToString("dd-MM-yyyy") == "01-01-0001" ? DateTime.Parse("01-01-1900") : usuario.fechaRegistro;
            string sql = string.Empty;
            sql += @"DECLARE @ULTIMO_ID INT
                    INSERT INTO Usuario (nombre, apellido_paterno, apellido_materno, sexo, email, contraseña, fecha_registro, peso, altura," +
                                    "id_tipocuerpo, id_nivel)  VALUES ('" + usuario.nombre + "', '" + usuario.apellido_paterno + "', '" + usuario.apellido_materno + "'" +
                                    ", " + usuario.sexo + ", '" + usuario.email + "', '" + usuario.contraseña + "', '" + fecha + "'," +
                                    "" + usuario.peso + ", " + usuario.altura + ", " + usuario.id_tipoCuerpo + ", " + usuario.id_nivel + ")" +
                                    "(SELECT @ULTIMO_ID = scope_identity())" +//Rescata el utlimo ID_usuario insertado para insertarlo en las tablas de relacionadas al usuario
                                    "INSERT INTO Usuario_Objetivo (id_objetivo, id_usuario) VALUES (" + usuario.id_objetivo + ", @ULTIMO_ID) " +
                                    "SELECT U.nombre, U.apellido_paterno, U.apellido_materno, U.sexo, U.email, U.contraseña, U.fecha_registro, U.peso, U.altura," +
                                    " U.id_tipocuerpo, U.id_nivel, UO.id_objetivo" +
                                    " FROM Usuario U" +
                                    " INNER JOIN Usuario_Objetivo UO ON UO.id_usuario = U.id_usuario" +
                                    " WHERE U.id_usuario = @ULTIMO_ID";

            sqlCommand = new SqlCommand(sql, connection);
            connection.Open();
            reader = sqlCommand.ExecuteReader();
            objUsuario = new Usuario();
            while (reader.Read())
            {
                objUsuario.id_usuario = int.Parse(reader["id_usuario"].ToString());
                objUsuario.nombre = reader["nombre"].ToString();
                objUsuario.apellido_paterno = reader["apellido_paterno"].ToString();
                objUsuario.apellido_materno = reader["apellido_materno"].ToString();
                objUsuario.sexo = int.Parse(reader["sexo"].ToString());
                objUsuario.email = reader["email"].ToString();
                objUsuario.contraseña = reader["contraseña"].ToString();
                objUsuario.fechaRegistro = DateTime.Parse(reader["fecha_registro"].ToString());
                objUsuario.peso = decimal.Parse(reader["peso"].ToString());
                objUsuario.altura = decimal.Parse(reader["altura"].ToString());
                objUsuario.id_tipoCuerpo = int.Parse(reader["id_tipocuerpo"].ToString());
                objUsuario.id_nivel = int.Parse(reader["id_nivel"].ToString());
                objUsuario.id_objetivo = int.Parse(reader["id_objetivo"].ToString());
            }
            connection.Close();

            return objUsuario;
        }
        public int ValidarUsuario(string email)
        {
            SqlConnection connection = new SqlConnection(this.connection);
            SqlCommand sqlCommand;
            SqlDataReader reader;
            int idUsuario = 0;
            string sql = string.Empty;
            sql = string.Format("SELECT * FROM Usuario WHERE email = '{0}'", email);

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
        public Usuario Editar(int id, Usuario usuario)
        {
            SqlConnection connection = new SqlConnection(this.connection);
            SqlCommand sqlCommand;
            SqlDataReader reader;
            Usuario objUsuario;

            string sql = string.Empty;
            sql += @"UPDATE Usuario SET nombre = '" + usuario.nombre + "', apellido_paterno = '" + usuario.apellido_paterno + "', sexo = " + usuario.sexo + ", email = '" + usuario.email + "', " +
                "contraseña = '" + usuario.contraseña + "', peso = " + usuario.sexo + ", altura = " + usuario.altura + ", id_tipocuerpo = " + usuario.id_tipoCuerpo + ", id_nivel = " + usuario.id_nivel + " " +
                "WHERE id_usuario = " + id + "  " +

                "UPDATE Usuario_Objetivo SET id_objetivo = " + usuario.id_objetivo + " WHERE id_usuario = " + id + "  " +

                "SELECT U.nombre, U.apellido_paterno, U.apellido_materno, U.sexo, U.email, U.contraseña, U.fecha_registro, U.peso, U.altura, " +
                "U.id_tipocuerpo, U.id_nivel, UO.id_objetivo " +
                "FROM Usuario U " +
                "INNER JOIN Usuario_Objetivo UO ON UO.id_usuario = U.id_usuario " +
                "WHERE U.id_usuario = " + id + "  ";

            sqlCommand = new SqlCommand(sql, connection);
            connection.Open();
            reader = sqlCommand.ExecuteReader();
            objUsuario = new Usuario();
            while (reader.Read())
            {
                objUsuario.id_usuario = int.Parse(reader["id_usuario"].ToString());
                objUsuario.nombre = reader["nombre"].ToString();
                objUsuario.apellido_paterno = reader["apellido_paterno"].ToString();
                objUsuario.apellido_materno = reader["apellido_materno"].ToString();
                objUsuario.sexo = int.Parse(reader["sexo"].ToString());
                objUsuario.email = reader["email"].ToString();
                objUsuario.contraseña = reader["contraseña"].ToString();
                objUsuario.fechaRegistro = DateTime.Parse(reader["fecha_registro"].ToString());
                objUsuario.peso = decimal.Parse(reader["peso"].ToString());
                objUsuario.altura = decimal.Parse(reader["altura"].ToString());
                objUsuario.id_tipoCuerpo = int.Parse(reader["id_tipocuerpo"].ToString());
                objUsuario.id_nivel = int.Parse(reader["id_nivel"].ToString());
                objUsuario.id_objetivo = int.Parse(reader["id_objetivo"].ToString());
            }

            return objUsuario;
        }
    }
}

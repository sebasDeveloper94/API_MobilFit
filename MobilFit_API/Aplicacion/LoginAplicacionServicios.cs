﻿using MobilFit_API.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
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
            sql = @"SELECT U.id_usuario, U.nombre, U.apellido, U.edad, U.sexo, U.email, U.contraseña, U.fecha_registro, U.peso, U.altura,
                    U.id_tipocuerpo, NU.id_nivel, UO.id_objetivo
                    FROM Usuario U 
                    LEFT JOIN Usuario_Objetivo UO ON UO.id_usuario = U.id_usuario
                    LEFT JOIN Nivel_Usuario NU ON NU.id_usuario = U.id_usuario
                    WHERE U.email = '" + email + "' and U.contraseña = '" + password + "'";

            sqlCommand = new SqlCommand(sql, connection);
            connection.Open();
            reader = sqlCommand.ExecuteReader();
            objUsuario = new Usuario();
            while (reader.Read())
            {
                objUsuario.id_usuario = int.Parse(reader["id_usuario"].ToString());
                objUsuario.nombre = reader["nombre"].ToString();
                objUsuario.apellido = reader["apellido"].ToString();
                objUsuario.edad = int.Parse(reader["edad"].ToString());
                objUsuario.sexo = reader["sexo"].ToString() == "True" ? 1 : 0;
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
            DateTime fecha = DateTime.Now;
            string sql = string.Empty;
            sql += @"DECLARE @ULTIMO_ID INT
                    INSERT INTO Usuario (nombre, apellido, edad, sexo, email, contraseña, fecha_registro, peso, altura," +
                                    "id_tipocuerpo)  VALUES ('" + usuario.nombre + "', '" + usuario.apellido + "', " +
                                    +usuario.edad + ", " + usuario.sexo + ", '" + usuario.email + "', '" + usuario.contraseña + "', '" + fecha + "'," +
                                    "" + usuario.peso + ", " + usuario.altura + ", " + usuario.id_tipoCuerpo + ")" +
                                    "(SELECT @ULTIMO_ID = scope_identity())" +//Rescata el utlimo ID_usuario insertado para insertarlo en las tablas de relacionadas al usuario
                                    "INSERT INTO Usuario_Objetivo (id_objetivo, id_usuario) VALUES (" + usuario.id_objetivo + ", @ULTIMO_ID) " +
                                    "INSERT INTO Nivel_Usuario (id_usuario, id_nivel) VALUES (@ULTIMO_ID, " + usuario.id_nivel + ") " +
                                    "SELECT U.id_usuario, U.nombre, U.apellido, U.edad, U.sexo, U.email, U.contraseña, U.fecha_registro, U.peso, U.altura," +
                                    "U.id_tipocuerpo, NU.id_nivel, UO.id_objetivo" +
                                    " FROM Usuario U" +
                                    " LEFT JOIN Usuario_Objetivo UO ON UO.id_usuario = U.id_usuario" +
                                    " LEFT JOIN Nivel_Usuario NU ON NU.id_usuario = U.id_usuario" +
                                    " WHERE U.id_usuario = @ULTIMO_ID";

            sqlCommand = new SqlCommand(sql, connection);
            connection.Open();
            reader = sqlCommand.ExecuteReader();
            objUsuario = new Usuario();
            while (reader.Read())
            {
                objUsuario.id_usuario = int.Parse(reader["id_usuario"].ToString());
                objUsuario.nombre = reader["nombre"].ToString();
                objUsuario.apellido = reader["apellido"].ToString();
                objUsuario.edad = int.Parse(reader["edad"].ToString());
                objUsuario.sexo = reader["sexo"].ToString() == "True" ? 1 : 0;
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
            sql += @"UPDATE Usuario SET nombre = '" + usuario.nombre + "', apellido = '" + usuario.apellido + "', edad = " + usuario.edad + ", sexo = " + usuario.sexo + ", email = '" + usuario.email + "', " +
                    " contraseña = '" + usuario.contraseña + "', peso = " + usuario.peso + ", altura = " + usuario.altura + ", id_tipocuerpo = " + usuario.id_tipoCuerpo + " " +
                    " WHERE id_usuario = " + id + " " +
                    " UPDATE Usuario_Objetivo SET id_objetivo = " + usuario.id_objetivo + " WHERE id_usuario = " + id + " " +
                    " UPDATE Nivel_Usuario SET id_nivel = " + usuario.id_nivel + " WHERE id_usuario = " + id + " " +
                    " SELECT U.id_usuario, U.nombre, U.apellido, U.edad, U.sexo, U.email, U.contraseña, U.fecha_registro, U.peso, U.altura,U.id_tipocuerpo, NU.id_nivel, UO.id_objetivo  " +
                    " FROM Usuario U " +
                    " LEFT JOIN Usuario_Objetivo UO ON UO.id_usuario = U.id_usuario " +
                    " LEFT JOIN Nivel_Usuario NU ON NU.id_usuario = U.id_usuario WHERE U.id_usuario = " + id + " ";

            sqlCommand = new SqlCommand(sql, connection);
            connection.Open();
            reader = sqlCommand.ExecuteReader();
            objUsuario = new Usuario();
            while (reader.Read())
            {
                objUsuario.id_usuario = int.Parse(reader["id_usuario"].ToString());
                objUsuario.nombre = reader["nombre"].ToString();
                objUsuario.apellido = reader["apellido"].ToString();
                objUsuario.edad = int.Parse(reader["edad"].ToString());
                objUsuario.sexo = reader["sexo"].ToString() == "True" ? 1 : 0;
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

        public int SendEmail(string email)
        {
            string nuevaContraseña = string.Empty;
            nuevaContraseña = "ajust95H";
            SqlConnection connection = new SqlConnection(this.connection);
            SqlCommand sqlCommand;
            SqlDataReader reader;
            string sql = string.Empty;
            int actualizo = 0;

            sql += @"update Usuario set contraseña = '"+nuevaContraseña+"' where email = '"+email+"'";

            sqlCommand = new SqlCommand(sql, connection);
            connection.Open();
            reader = sqlCommand.ExecuteReader();
            actualizo = reader.RecordsAffected;
            connection.Close();

            if (actualizo == 0)
            {
                return 0;
            }

            MailMessage emailSended = new MailMessage();
            emailSended.To.Add(new MailAddress(email));
            emailSended.From = new MailAddress("MobilFit@MobilFit.com");
            emailSended.Subject = "Recuperación de constraseña ( " + DateTime.Now.ToString("dd / MMM / yyy hh:mm:ss") + " ) ";
            emailSended.Body = "Su nueva contraseña MobilFit es: " + "<strong>" + nuevaContraseña + "</strong>" + "\n" + "Por favor cambie la contraseña despues de iniciar sesión.";
            emailSended.IsBodyHtml = true;
            emailSended.Priority = MailPriority.Normal;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.sendgrid.net";
            smtp.Port = 2525;
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("azure_a7c75d7f43677a4b90996abe5d8b4273@azure.com", "Flip2009");

            string output = null;

            try
            {
                smtp.Send(emailSended);
                emailSended.Dispose();
                output = "Corre electrónico fue enviado satisfactoriamente.";
            }
            catch (Exception ex)
            {
                output = "Error enviando correo electrónico: " + ex.Message;
            }

            if (output == string.Empty)
            {
                return 0;
            }

            return 1;
        }
    }
}

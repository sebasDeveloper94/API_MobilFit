using backDataMobilFit.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;



namespace backDataMobilFit.Persistence
{
    class UsuarioData
    {
        public string conection { get; set; }
        public UsuarioData(string conection)
        {
            this.conection = conection;
        }

        public IList<Usuario> GetUsuarios()
        {

            SqlConnection sqlConnection = new SqlConnection(this.conection);
            SqlCommand sqlCommand;
            SqlDataReader reader;
            List<Usuario> listUsuarios = new List<Usuario>();
            string sql = string.Empty;
            sql = "SELECT * FROM Usuario";

            try
            {
                sqlCommand = new SqlCommand();
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    listUsuarios.Add(new Usuario()
                    {
                        id_usuario = int.Parse(reader["id_usuario"].ToString()),
                        nombre = reader["nombre"].ToString(),
                        apellido_paterno = reader["apellido_paterno"].ToString(),
                        apellido_materno = reader["apellido_materno"].ToString(),
                        email = reader["email"].ToString(),
                        contrasenha = reader["contrasenha"].ToString(),
                        nombre_usuario = reader["nombre_usuario"].ToString(),
                        peso = decimal.Parse(reader["peso"].ToString()),
                        altura = decimal.Parse(reader["altura"].ToString()),
                        contraindicacion = int.Parse(reader["contraindicacion"].ToString()),
                        objetivo = int.Parse(reader["objetivo"].ToString()),
                        id_nivel = int.Parse(reader["id_nivel"].ToString())
                    });
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("El error fue ", ex);
            }

            return listUsuarios;
        }

        public Usuario GetUsuario(int id_usuario)
        {

            SqlConnection sqlConnection = new SqlConnection(this.conection);
            SqlCommand sqlCommand;
            SqlDataReader reader;
            Usuario objUsuarios = new Usuario();
            string sql = string.Empty;
            sql = "SELECT * FROM Usuario WHERE id_usuario ="+ id_usuario;

            try
            {
                sqlCommand = new SqlCommand();
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    objUsuarios.id_usuario = int.Parse(reader["id_usuario"].ToString());
                    objUsuarios.nombre = reader["nombre"].ToString();
                    objUsuarios.apellido_paterno = reader["apellido_paterno"].ToString();
                    objUsuarios.apellido_materno = reader["apellido_materno"].ToString();
                    objUsuarios.email = reader["email"].ToString();
                    objUsuarios.contrasenha = reader["contrasenha"].ToString();
                    objUsuarios.nombre_usuario = reader["nombre_usuario"].ToString();
                    objUsuarios.peso = decimal.Parse(reader["peso"].ToString());
                    objUsuarios.altura = decimal.Parse(reader["altura"].ToString());
                    objUsuarios.contraindicacion = int.Parse(reader["contraindicacion"].ToString());
                    objUsuarios.objetivo = int.Parse(reader["objetivo"].ToString());
                    objUsuarios.id_nivel = int.Parse(reader["id_nivel"].ToString());
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("El error fue ", ex);
            }

            return objUsuarios;
        }


    }
}

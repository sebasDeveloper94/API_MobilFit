﻿using MobilFit_API.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilFit_API.Aplicacion
{
    class UsuariosAplicacionServicios
    {
        private string connection { get; set; }
        //contructor
        public UsuariosAplicacionServicios(string connection)
        {
            this.connection = connection;
        }
        //metodos
        public List<Usuario> GetUsuarios()
        {

            SqlConnection sqlConnection = new SqlConnection(this.connection);
            SqlCommand sqlCommand;
            SqlDataReader reader;
            List<Usuario> listUsuarios = new List<Usuario>();
            string sql = string.Empty;
            sql = "SELECT * FROM Usuario";

            try
            {
                sqlCommand = new SqlCommand(sql, sqlConnection);
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    listUsuarios.Add(new Usuario()
                    {
                        id_usuario = int.Parse(reader["id_usuario"].ToString()),
                        nombre = reader["nombre"].ToString(),
                        apellido = reader["apellido_paterno"].ToString(),
                        sexo = int.Parse(reader["sexo"].ToString()),
                        email = reader["email"].ToString(),
                        contraseña = reader["contrasenha"].ToString(),
                        fechaRegistro = DateTime.Parse(reader["fecha_registro"].ToString()),
                        peso = decimal.Parse(reader["peso"].ToString()),
                        altura = decimal.Parse(reader["altura"].ToString()),
                        id_tipoCuerpo = int.Parse(reader["id_tipocuerpo"].ToString()),
                        id_nivel = int.Parse(reader["id_nivel"].ToString()),
                        id_objetivo = int.Parse(reader["id_objetivo"].ToString()),
                    });
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                return null;
            }

            return listUsuarios;
        }

        public Usuario GetUsuario(int id_usuario)
        {

            SqlConnection sqlConnection = new SqlConnection(this.connection);
            SqlCommand sqlCommand;
            SqlDataReader reader;
            Usuario objUsuarios = new Usuario();
            string sql = string.Empty;
            sql = "SELECT * FROM Usuario WHERE id_usuario =" + id_usuario;

            try
            {
                sqlCommand = new SqlCommand(sql, sqlConnection);
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    objUsuarios.id_usuario = int.Parse(reader["id_usuario"].ToString());
                    objUsuarios.nombre = reader["nombre"].ToString();
                    objUsuarios.apellido = reader["apellido"].ToString();
                    objUsuarios.sexo = int.Parse(reader["sexo"].ToString());
                    objUsuarios.email = reader["email"].ToString();
                    objUsuarios.contraseña = reader["contraseña"].ToString();
                    objUsuarios.fechaRegistro = DateTime.Parse(reader["fecha_registro"].ToString());
                    objUsuarios.peso = decimal.Parse(reader["peso"].ToString());
                    objUsuarios.altura = decimal.Parse(reader["altura"].ToString());
                    objUsuarios.id_tipoCuerpo = int.Parse(reader["id_tipocuerpo"].ToString());
                    objUsuarios.id_nivel = int.Parse(reader["id_nivel"].ToString());
                    objUsuarios.id_objetivo = int.Parse(reader["id_objetivo"].ToString());
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

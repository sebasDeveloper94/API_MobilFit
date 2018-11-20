using MobilFit_API.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilFit_API.Aplicacion
{
    class RutinasAplicacionServicios
    {
        private string connection { get; set; }
        //contructor
        public RutinasAplicacionServicios(string connection)
        {
            this.connection = connection;
        }
        //metodos
        public List<Rutina> GetRutinas()
        {
            SqlConnection sqlConnection = new SqlConnection(this.connection);
            SqlCommand sqlCommand;
            SqlDataReader reader;
            List<Rutina> listRutinas = new List<Rutina>();
            string sql = string.Empty;
            sql = "SELECT * FROM Usuario";

            sqlCommand = new SqlCommand();
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                listRutinas.Add(new Rutina()
                {
                    id_rutina = int.Parse(reader["id_usuario"].ToString()),
                    nombre = reader["nombre"].ToString(),
                    meta = reader["meta"].ToString(),
                    id_tipoRutina = int.Parse(reader["id_tipo_rutina"].ToString()),
                    id_categoria = int.Parse(reader["id_categoria"].ToString()),
                });
            }
            sqlConnection.Close();

            return listRutinas;
        }
        public Rutina GetRutina(int id_rutina)
        {
            SqlConnection sqlConnection = new SqlConnection(this.connection);
            SqlCommand sqlCommand;
            SqlDataReader reader;
            Rutina objRutina = new Rutina();
            string sql = string.Empty;
            sql = "SELECT * FROM Rutina WHERE id_usuario =" + id_rutina;

            try
            {
                sqlCommand = new SqlCommand();
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    objRutina.id_rutina = int.Parse(reader["id_usuario"].ToString());
                    objRutina.nombre = reader["nombre"].ToString();
                    objRutina.meta = reader["meta"].ToString();
                    objRutina.id_tipoRutina = int.Parse(reader["id_tipo_rutina"].ToString());
                    objRutina.id_categoria = int.Parse(reader["id_categoria"].ToString());
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("El error fue ", ex);
            }

            return objRutina;
        }
    }
}

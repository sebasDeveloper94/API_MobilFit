using MobilFit_API.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilFit_API.Aplicacion
{
    class DesempeñoAplicacionServicios
    {
        private string connection { get; set; }
        //contructor
        public DesempeñoAplicacionServicios(string connection)
        {
            this.connection = connection;
        }
        //metodos
        public Desempeño GetDesempeño(int id_desempeño)
        {
            SqlConnection sqlConnection = new SqlConnection(this.connection);
            SqlCommand sqlCommand;
            SqlDataReader reader;
            Desempeño objDesempeño = new Desempeño();
            string sql = string.Empty;
            sql = "SELECT * FROM Desempeño WHERE id_usuario =" + id_desempeño;

            try
            {
                sqlCommand = new SqlCommand();
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    objDesempeño.id_desempeño = int.Parse(reader["id_desempeño"].ToString());
                    objDesempeño.fecha = DateTime.Parse(reader["fecha"].ToString());
                    objDesempeño.tiempo_entrenamiento = DateTime.Parse(reader["tiempo_entrenamieto"].ToString());
                    objDesempeño.porcentaje_rutina = decimal.Parse(reader["porcentaje_rutina"].ToString());
                    objDesempeño.distancia_recorrida = decimal.Parse(reader["distacia_recorrida"].ToString());
                    objDesempeño.id_usuario = int.Parse(reader["id_usuario"].ToString());
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                return null;
            }

            return objDesempeño;
        }
    }
}

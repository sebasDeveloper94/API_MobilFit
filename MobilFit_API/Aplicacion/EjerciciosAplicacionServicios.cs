using MobilFit_API.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilFit_API.Aplicacion
{
    class EjerciciosAplicacionServicios
    {
        private string connection { get; set; }
        //contructor
        public EjerciciosAplicacionServicios(string connection)
        {
            this.connection = connection;
        }
        //metodos
        public List<Ejercicio> GetEjercicios(int id_rutina)
        {
            SqlConnection sqlConnection = new SqlConnection(this.connection);
            SqlCommand sqlCommand;
            SqlDataReader reader;
            List<Ejercicio> ejercicios;

            string sql = string.Empty;
            sql = string.Format(@"SELECT E.id_ejercicio, E.nombre_ejercicio, E.descripcion, E.repeticiones, E.series, E.peso, E.tiempo, E.distancia, E.descanso, T.id_tips, T.descripcion
                                    FROM Rutina R, Ejercicio E
                                    INNER JOIN Ejercicio_Rutina ER ON ER.id_ejercicio = E.id_ejercicio
                                    INNER JOIN Tips_Ejercicio TE ON TE.id_ejercicio = E.id_ejercicio
                                    LEFT JOIN Tips T ON T.id_tips = TE.id_tips_ejercicio
                                    WHERE R.id_rutina = {0} AND R.id_rutina = ER.id_rutina", id_rutina);
            try
            {
                sqlCommand = new SqlCommand(sql, sqlConnection);
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();
                ejercicios = new List<Ejercicio>();
                while (reader.Read())
                {
                    ejercicios.Add(new Ejercicio()
                    {
                        id_ejercicio = int.Parse(reader["id_ejercicio"].ToString()),
                        nombre_ejercicio = reader["nombre_ejercicio"].ToString(),
                        descripcion = reader["descripcion"].ToString(),
                        repeticiones = int.Parse(reader["repeticiones"].ToString()),
                        series = int.Parse(reader["series"].ToString()),
                        peso = decimal.Parse(reader["peso"].ToString()),
                        tiempo = DateTime.Parse(reader["tiempo"].ToString()),
                        distancia = decimal.Parse(reader["distancia"].ToString()),
                        descanso = decimal.Parse(reader["descanso"].ToString()),
                        Tips = new Tips()
                        {
                            id_tips = int.Parse(reader["id_tips"].ToString()),
                            descripcion = reader["DESCRIPCION_TIP"].ToString(),
                        }
                    });

                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                return null;
            }

            return ejercicios;
        }
    }
}

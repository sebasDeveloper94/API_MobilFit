using backMobilFitData_V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backMobilFitData.Aplicacion
{
    class ReporteDesempeñoAplicacionServicios
    {
        private string connection { get; set; }
        //contructor
        public ReporteDesempeñoAplicacionServicios(string connection)
        {
            this.connection = connection;
        }

        public ReporteDesempeño GenerarReporte() {

            return null;
        }
        //metodos
        //public Ejercicio GetEjercicio(int id_rutina)
        //{
        //    SqlConnection sqlConnection = new SqlConnection(this.connection);
        //    SqlCommand sqlCommand;
        //    SqlDataReader reader;
        //    Ejercicio objEjercicio = new Ejercicio();
        //    string sql = string.Empty;
        //    sql = "SELECT * FROM Ejercicio WHERE id_usuario =" + id_rutina;

        //    try
        //    {
        //        sqlCommand = new SqlCommand();
        //        sqlConnection.Open();
        //        reader = sqlCommand.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            objEjercicio.id_ejercicio = int.Parse(reader["id_ejercicio"].ToString());
        //            objEjercicio.nombre_ejercicio = reader["nombre_ejercicio"].ToString();
        //            objEjercicio.descripcion = reader["descripcion"].ToString();
        //            objEjercicio.repeticiones = int.Parse(reader["repeticiones"].ToString());
        //            objEjercicio.series = int.Parse(reader["series"].ToString());
        //            objEjercicio.tiempo = DateTime.Parse(reader["tiempo"].ToString());
        //            objEjercicio.distancia = decimal.Parse(reader["distancia"].ToString());
        //            objEjercicio.id_rutina = int.Parse(reader["id_rutina"].ToString());
        //        }
        //        sqlConnection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }

        //    return objEjercicio;
        //}
    }
}

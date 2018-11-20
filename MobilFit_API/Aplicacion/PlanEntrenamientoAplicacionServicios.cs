using MobilFit_API.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MobilFit_API.Aplicacion
{
    public class PlanEntrenamientoAplicacionServicios
    {
        private string connection { get; set; }
        public PlanEntrenamientoAplicacionServicios(string connection)
        {
            this.connection = connection;
        }

        public PlanEntrenamiento AsignarRutinas(int id_usuario)
        {
            SqlConnection connection = new SqlConnection(this.connection);
            SqlCommand sqlCommand;
            SqlDataReader reader;
            PlanEntrenamiento objPlanEntrenamiento = new PlanEntrenamiento();
            string sql = string.Empty;
            sql = "EXEC [dbo].[SP_TRAER_PLAN] " + id_usuario;

            sqlCommand = new SqlCommand(sql, connection);
            connection.Open();
            reader = sqlCommand.ExecuteReader();

            objPlanEntrenamiento.rutinasPlan = new List<Rutina>();
            objPlanEntrenamiento.objPresional = new Profesional();
            objPlanEntrenamiento.objUsuario = new Usuario();
            while (reader.Read())
            {
                objPlanEntrenamiento.idPlan = int.Parse(reader["id_plan_entrenamiento"].ToString());
                objPlanEntrenamiento.nombre = reader["nombrePlan"].ToString();
                objPlanEntrenamiento.candidadDias = int.Parse(reader["cantidad_dias"].ToString());
                objPlanEntrenamiento.rutinasPlan.Add(new Rutina()
                {
                    id_rutina = int.Parse(reader["id_rutina"].ToString()),
                    nombre = reader["nombreRutina"].ToString(),
                });

                objPlanEntrenamiento.objPresional.idProfesional = int.Parse(reader["id_profesional"].ToString());
                objPlanEntrenamiento.objPresional.profesion = reader["profesion"].ToString();
                objPlanEntrenamiento.objPresional.nombre = reader["nombreProfesional"].ToString();
                objPlanEntrenamiento.objPresional.email = reader["email"].ToString();

                objPlanEntrenamiento.objUsuario.id_usuario = int.Parse(reader["ID_USUARIO"].ToString());
            }
            connection.Close();
            return objPlanEntrenamiento;
        }

        public int GuardarDiasRutinas(DiasEntrenamiento objDias)
        {
            SqlConnection connection = new SqlConnection(this.connection);
            SqlCommand sqlCommand;
            SqlDataReader reader;

            int inserto = 0;
            string sql = string.Empty;
            for (int i = 0; i < objDias.idRutinas.Count(); i++)
            {
                sql += string.Format(@"EXEC [dbo].[SP_GUARDAR_DIASRUTINAS] {0}, {1}, {2}", objDias.idPlan, objDias.idRutinas[i], objDias.dias[i]);
            }

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
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
            sql = @"EXEC [dbo].[SP_TRAER_PLAN] " + id_usuario;

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
                    meta = reader["meta"].ToString()
                });

                objPlanEntrenamiento.objPresional.idProfesional = int.Parse(reader["id_profesional"].ToString());
                objPlanEntrenamiento.objPresional.profesion = reader["profesion"].ToString();
                objPlanEntrenamiento.objPresional.nombre = reader["nombreProfesional"].ToString();
                objPlanEntrenamiento.objPresional.email = reader["email"].ToString();
                objPlanEntrenamiento.objUsuario.id_usuario = int.Parse(reader["ID_USUARIO"].ToString());

            }
            connection.Close();
            connection.Open();
            sql = string.Empty;
            sql += string.Format(@"DECLARE @ULTIMO_ID INT
                    IF NOT EXISTS(SELECT * FROM Plan_Usuario WHERE id_usuario = {0} AND id_plan_entrenamiento = {1})
                    BEGIN
	                    INSERT INTO Plan_Usuario (id_plan_entrenamiento, id_usuario) VALUES ({1}, {0})
	                    (SELECT @ULTIMO_ID = SCOPE_IDENTITY())
	                    SELECT @ULTIMO_ID  AS id_plan_usuario
                    END
                    ELSE
                    BEGIN
	                    SELECT PU.id_plan_usuario FROM Plan_Usuario PU WHERE id_usuario = {0} AND id_plan_entrenamiento = {1}
                    END", objPlanEntrenamiento.objUsuario.id_usuario, objPlanEntrenamiento.idPlan);

            sqlCommand = new SqlCommand(sql, connection);
            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                objPlanEntrenamiento.id_planUsuario = int.Parse(reader["id_plan_usuario"].ToString());
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
            sql += string.Format(@"EXEC [dbo].[SP_GUARDAR_DIASRUTINAS] {0}, {1}, {2}", objDias.idPlan, objDias.idRutina, objDias.dia);

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

        public RutinaSeleccionada VerDiaSeleccionado(int idRutina)
        {
            SqlConnection sqlConnection = new SqlConnection(this.connection);
            SqlCommand sqlCommand;
            SqlDataReader reader;
            RutinaSeleccionada rutinaSeleccionada;
            string sql = string.Empty;
            sql = "SELECT DR.id_plan_usuario, DR.id_rutina, DR.dia, E.nombre_ejercicio, E.descripcion"+
                   "FROM Dias_Rutina DR"+
                   "INNER JOIN Ejercicio_Rutina ER ON ER.id_rutina = DR.id_rutina"+
                   "INNER JOIN Ejercicio E ON E.id_ejercicio = ER.id_ejercicio+"+
                   "WHERE DR.id_rutina = " + idRutina;

            sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();
            rutinaSeleccionada = new RutinaSeleccionada();
            rutinaSeleccionada.Ejercicios = new List<Ejercicio>();
            while (reader.Read())
            {
                rutinaSeleccionada.DiaEntrenamientos.idPlan = int.Parse(reader["id_plan_usuario"].ToString());
                rutinaSeleccionada.DiaEntrenamientos.idRutina = int.Parse(reader["id_rutina"].ToString());
                rutinaSeleccionada.DiaEntrenamientos.dia = int.Parse(reader["dia"].ToString());
                rutinaSeleccionada.Ejercicios.Add(new Ejercicio() {
                    nombre_ejercicio = reader["nombre_ejercicio"].ToString(),
                    descripcion = reader["descripcion"].ToString()
                });
            }
            sqlConnection.Close();

            return rutinaSeleccionada;
        }

        public List<DiasEntrenamiento> VerDiasSeleccionados(int idPlanUsuario)
        {
            SqlConnection sqlConnection = new SqlConnection(this.connection);
            SqlCommand sqlCommand;
            SqlDataReader reader;
            List<DiasEntrenamiento> listDias;
            string sql = string.Empty;
            sql = "SELECT * FROM Dias_Rutina WHERE id_plan_usuario =" + idPlanUsuario;

            sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();
            listDias = new List<DiasEntrenamiento>();
            while (reader.Read())
            {
                listDias.Add(new DiasEntrenamiento()
                {
                    idPlan = int.Parse(reader["id_plan_usuario"].ToString()),
                    idRutina = int.Parse(reader["id_rutina"].ToString()),
                    dia = int.Parse(reader["dia"].ToString())
                });
            }
            sqlConnection.Close();

            return listDias;
        }
    }
}
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
	                    INSERT INTO Plan_Usuario (id_plan_entrenamiento, id_usuario, plan_completado) VALUES ({1}, {0}, 0)
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

        public RutinaSeleccionada VerDiaSeleccionado(int idRutina, int idPlanUsuario)
        {
            SqlConnection sqlConnection = new SqlConnection(this.connection);
            SqlCommand sqlCommand;
            SqlDataReader reader;
            RutinaSeleccionada rutinaSeleccionada;
            string sql = string.Empty;
            sql = "IF EXISTS(SELECT * FROM Dias_Rutina WHERE id_plan_usuario = "+idPlanUsuario+" AND id_rutina = "+idRutina+") "+
                   "BEGIN "+
                        "SELECT DR.id_plan_usuario, DR.id_rutina, DR.dia, DR.rutina_completada, E.nombre_ejercicio, E.descripcion " +
                        "FROM Dias_Rutina DR "+
                        "INNER JOIN Ejercicio_Rutina ER ON ER.id_rutina = DR.id_rutina "+
                        "INNER JOIN Ejercicio E ON E.id_ejercicio = ER.id_ejercicio "+
                        "WHERE DR.id_rutina =" + idRutina + 
                    " END"+ 
                    " ELSE"+ 
                        " BEGIN "+
                            " INSERT INTO Dias_Rutina(dia, id_rutina, rutina_completada, id_plan_usuario) VALUES(0, " + idRutina+", 0, "+idPlanUsuario+") "+
                            "SELECT DR.id_plan_usuario, DR.id_rutina, DR.dia, DR.rutina_completada, E.nombre_ejercicio, E.descripcion " +
                            " FROM Dias_Rutina DR " +
                            " INNER JOIN Ejercicio_Rutina ER ON ER.id_rutina = DR.id_rutina " +
                            " INNER JOIN Ejercicio E ON E.id_ejercicio = ER.id_ejercicio " +
                            " WHERE DR.id_rutina =" + idRutina +
                        " END";

            sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();
            rutinaSeleccionada = new RutinaSeleccionada();
            rutinaSeleccionada.DiaEntrenamientos = new DiasEntrenamiento();
            rutinaSeleccionada.Ejercicios = new List<Ejercicio>();
            while (reader.Read())
            {
                rutinaSeleccionada.DiaEntrenamientos.idPlan = int.Parse(reader["id_plan_usuario"].ToString());
                rutinaSeleccionada.DiaEntrenamientos.idRutina = int.Parse(reader["id_rutina"].ToString());
                rutinaSeleccionada.DiaEntrenamientos.dia = int.Parse(reader["dia"].ToString());
                rutinaSeleccionada.RutinaCompletada = reader["rutina_completada"].ToString() == "True" ? true : false;
                rutinaSeleccionada.Ejercicios.Add(new Ejercicio()
                {
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

            try
            {
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

                if (listDias.Count() == 0)
                {
                    return null;
                }

                sqlConnection.Close();
            }
            catch (Exception)
            {
                return null;
            }

            return listDias;
        }

        public ReporteDesempeño TerminaRutina(int idRutina, Desempeño desempeño)
        {
            SqlConnection connection = new SqlConnection(this.connection);
            SqlCommand sqlCommand;
            SqlDataReader reader;
            ReporteDesempeño objReporte;

            string sql = string.Empty;
            sql += string.Format(@"--CALCULO DE DESEMPEÑO, SE NECESITA: ID_RUTINA, ID_PLAN_USUARIO
                        DECLARE @ID_DESEMPEÑO INT
                        DECLARE @TOTAL_HORAS FLOAT
                        DECLARE @KM_RECORRIDOS FLOAT

                        UPDATE Dias_Rutina SET rutina_completada = 1 WHERE id_rutina = {0} AND id_plan_usuario = {1}
                        INSERT INTO Desempeño (fecha, horas_entrenamieto, distacia_recorrida, id_plan_usuario) values (GETDATE(), {2}, {3}, {1})
                        SELECT @ID_DESEMPEÑO = SCOPE_IDENTITY()

                        SELECT @TOTAL_HORAS = SUM(DE.horas_entrenamieto), @KM_RECORRIDOS = SUM(DE.distacia_recorrida) FROM Desempeño DE WHERE DE.id_plan_usuario = {1}

                        --calculos de imc y igc
                        DECLARE @PESO INT
                        DECLARE @ALTURA FLOAT
                        DECLARE @IMC FLOAT
                        DECLARE @IGC FLOAT
                        DECLARE @EDAD INT
                        DECLARE @SEXO BIT
                        DECLARE @TMB FLOAT --TASA METABOLICA BASAL

                        --CALCULO DE IMC Y IGC
                        DECLARE @CALORIAS_QUEMADAS FLOAT
                        DECLARE @ID_USUARIO INT
                        SELECT @ID_USUARIO = PU.id_usuario FROM Plan_Usuario PU WHERE id_plan_usuario = {1}
                        SELECT @PESO = U.peso, @ALTURA = U.altura, @EDAD = U.edad, @SEXO = U.sexo FROM Usuario U WHERE id_usuario = @ID_USUARIO
                        SELECT @IMC = @PESO / (SELECT POWER(@ALTURA, 2))--CALCULO DE IMC
                        SELECT @IGC = ((1.2 * @IMC) + (0.23 * @EDAD) - (10.8 * @SEXO) - 5.4) 

                        --CALCULO DE CALORIAS QUEMADAS
                        SELECT @TMB = (13.75 * @PESO) + (5 * @ALTURA) + 66--FORMULA DE TMB
                        SELECT @CALORIAS_QUEMADAS = @TMB * 1.55 -- EQUIVALE A LA CANTIDAD DE ACTIVIDAD FISICA DE LA PERSONA, DE 3 A 5 DIAS DE ENTRENAMIENTO

                        --CALCULOS DE CANTIDAD DE RUTINAS POR PLAN DE USUARIO Y LA CANTIDAD QUE COMPLETO
                        DECLARE @CANTIDAD_RUTINAS FLOAT
                        DECLARE @RUTINAS_COMPLETAS FLOAT
                        DECLARE @PORCENTAJERUTINA FLOAT
                        SELECT  @CANTIDAD_RUTINAS = COUNT(PR.id_rutina), @RUTINAS_COMPLETAS = (SELECT COUNT(DR.rutina_completada) from Dias_Rutina DR WHERE DR.id_plan_usuario = {1} and DR.rutina_completada > 0)
                        FROM Plan_Usuario PU
                        INNER JOIN Plan_Rutina PR ON PR.id_plan_entrenamiento = PU.id_plan_entrenamiento
                        WHERE  PU.id_plan_usuario = {1}
                        SELECT @PORCENTAJERUTINA = (@RUTINAS_COMPLETAS / @CANTIDAD_RUTINAS) * 100

                        --INSERSION EN EL REPORTE DE DESEMPEÑO
                        INSERT INTO Reporte_Desempeño (fecha_reporte, porcentaje_rutinas, total_horas_entrenamiento, calorias_quemadas, km_recorridos, imc_esperado, igc_esperado,
	                        id_desempeño) values (GETDATE(), @PORCENTAJERUTINA, @TOTAL_HORAS, @CALORIAS_QUEMADAS, @KM_RECORRIDOS, @IMC ,@IGC ,@ID_DESEMPEÑO)
                        --SACA EL RESULTADO DEL REPORTE
                        SELECT * FROM Reporte_Desempeño WHERE id_desempeño = @ID_DESEMPEÑO", idRutina, desempeño.id_plan_usuario, desempeño.horas_entrenamiento, desempeño.distancia_recorrida);

            sqlCommand = new SqlCommand(sql, connection);
            connection.Open();
            reader = sqlCommand.ExecuteReader();
            objReporte = new ReporteDesempeño();
            while (reader.Read())
            {
                objReporte.id_reporteDesempeño = int.Parse(reader["id_reporte_desempeño"].ToString());
                objReporte.fechaReporte = DateTime.Parse(reader["fecha_reporte"].ToString());
                objReporte.porcentajeRutina = float.Parse(reader["porcentaje_rutinas"].ToString());
                objReporte.tiempoEntrenamiento = float.Parse(reader["total_horas_entrenamiento"].ToString());
                objReporte.caloriasQuemadas = float.Parse(reader["calorias_quemadas"].ToString());
                objReporte.kmRecorridos = float.Parse(reader["km_recorridos"].ToString());
                objReporte.IMC = float.Parse(reader["imc_esperado"].ToString());
                objReporte.IGC = float.Parse(reader["igc_esperado"].ToString());
                objReporte.id_desempeño = int.Parse(reader["id_desempeño"].ToString());
            }
            connection.Close();

            return objReporte;
        }
    }
}
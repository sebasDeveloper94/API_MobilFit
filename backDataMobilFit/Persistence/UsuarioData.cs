using backDataMobilFit.Models;
using System;
using System.Collections.Generic;


namespace backDataMobilFit.Persistence
{
    class UsuarioData
    {
        public string conection { get; set; }
        public UsuarioData(string conection)
        {
            this.conection = conection;
        }

        public IList<Usuario> GetUsuarios() {

            //SqlConnection sqlConnection = new SqlConnection(this.conection);
            //SqlCommand sqlCommand;
            //SqlDataReader reader;
            //List<Usuario> listArticulos = new List<Usuario>();
            //string sql = string.Empty;
            //sql = "SELECT * FROM Articulo";

            //try
            //{
            //    sqlCommand = new SqlCommand();
            //    sqlConnection.Open();
            //    reader = sqlCommand.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        listArticulos.Add(new Articulo()
            //        {
            //            id_Articulo = int.Parse(reader["ID_Articulo"].ToString()),
            //            nombre = reader["Nombre"].ToString(),
            //            descripcion = reader["Descripcion"].ToString(),
            //            foto = reader["Foto"].ToString(),
            //            color = reader["Color"].ToString(),
            //            precio = float.Parse(reader["Precio"].ToString()),
            //            peso = float.Parse(reader["peso"].ToString()),
            //            marca = reader["Marca"].ToString(),
            //            codigoArticulo = reader["CodigoArticulo"].ToString(),
            //            impuestoVenta = float.Parse(reader["ImpuestoVenta"].ToString()),
            //            ID_Categoria = int.Parse(reader["ID_Categoria"].ToString())
            //        });
            //    }
            //    sqlConnection.Close();
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("El error fue ", ex);
            //}

            //return listArticulos;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Data;

namespace ASG.ASG.Data
{
    class dataAreas
    {

        public DataTable Listar_Areas(string busqueda)
        {
            using (var cnx = ASG_DB.connectionResult())
            {
                try
                {
                    OdbcDataReader resultado = null;
                    DataTable tabla = new DataTable();
                    OdbcCommand cmd = new OdbcCommand("CALL LISTAR_AREAS_PRODUCCION(?)", cnx);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@nombre", OdbcType.VarChar).Value = busqueda;
                    resultado = cmd.ExecuteReader();

                    if (resultado.HasRows)
                    {
                        tabla.Load(resultado);
                    }

                    return tabla;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public DataTable Listar_Encargados(string busqueda)
        {
            using (var cnx = ASG_DB.connectionResult())
            {
                try
                {
                    OdbcDataReader resultado = null;
                    DataTable tabla = new DataTable();
                    OdbcCommand cmd = new OdbcCommand("CALL LISTAR_ENCARGADO_AREA(?)", cnx);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@nombre", OdbcType.VarChar).Value = busqueda;
                    resultado = cmd.ExecuteReader();

                    if (resultado.HasRows)
                    {
                        tabla.Load(resultado);
                    }

                    return tabla;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }




        public bool Cambia_Estado_Area_Delete(int idArea)
        {
            using (var cnx = ASG_DB.connectionResult())
            {
                try
                {
                    bool resp = false;
                    OdbcCommand cmd = new OdbcCommand("CALL ELIMINAR_AREA_PRODUCCION(?)", cnx);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", OdbcType.Int).Value = idArea;

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        resp = true;
                    }


                    return resp;

                }
                catch (Exception ex)
                {
                    throw ex;
                }


            }
        }


        public int Crear_Area(string idEncargado, string nombre, string descripcion)
        {
            int resp = 0;

            using (var cnx = ASG_DB.connectionResult())
            {

                try
                {
                    OdbcCommand cmd = new OdbcCommand("CALL CREAR_AREA_PRODUCCION(?,?,?)", cnx);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = cnx;
                    cmd.Parameters.Add("@idEncargado", OdbcType.VarChar).Value = idEncargado;
                    cmd.Parameters.Add("@nombre", OdbcType.VarChar).Value = nombre;
                    cmd.Parameters.Add("@descripcion", OdbcType.VarChar).Value = descripcion;
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        resp = 1;
                    }
      
                    return resp;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

        }


        public int Edita_Area(string id, string idEncargado, string nombre, string descripcion)
        {
            int resp = 0;

            using (var cnx = ASG_DB.connectionResult())
            {

                try
                {
                    OdbcCommand cmd = new OdbcCommand("CALL EDITAR_AREA_PRODUCCION(?,?,?,?)", cnx);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = cnx;
                    cmd.Parameters.Add("@id", OdbcType.Int).Value = Convert.ToInt32(id);
                    cmd.Parameters.Add("@idEncargado", OdbcType.VarChar).Value = idEncargado;
                    cmd.Parameters.Add("@nombre", OdbcType.VarChar).Value = nombre;
                    cmd.Parameters.Add("@descripcion", OdbcType.VarChar).Value = descripcion;
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        resp = 1;
                    }

                    return resp;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

        }



    }

            
            
 }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Odbc;
using ASG.ASG.Entity;

namespace ASG.ASG.Data
{
    public class dataRecetas
    {

        public DataTable Listar_Mercaderia_Search(string busqueda)
        {
            using (var cnx = ASG_DB.connectionResult())
            {
                try
                {
                    OdbcDataReader resultado = null;
                    DataTable tabla = new DataTable();
                    OdbcCommand cmd = new OdbcCommand("CALL LISTAR_MERCADERIA_NOMBRE(?)", cnx);
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

         public DataTable Listar_Materia_Search(string busqueda)
        {
            using (var cnx = ASG_DB.connectionResult())
            {
                try
                {
                    OdbcDataReader resultado = null;
                    DataTable tabla = new DataTable();
                    OdbcCommand cmd = new OdbcCommand("CALL LISTAR_MATERIA_NOMBRE(?)", cnx);
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

        public DataTable Listar_Detalle_Receta(int idReceta)
        {
            using (var cnx = ASG_DB.connectionResult())
            {
                try
                {
                    OdbcDataReader resultado = null;
                    DataTable tabla = new DataTable();
                    OdbcCommand cmd = new OdbcCommand("CALL LISTAR_DETALLE_RECETA(?)", cnx);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@idReceta", OdbcType.Int).Value = idReceta;
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


        public DataTable Listar_Receta_Search(string busqueda)
        {
            using (var cnx = ASG_DB.connectionResult())
            {
                try
                {
                    OdbcDataReader resultado = null;
                    DataTable tabla = new DataTable();
                    OdbcCommand cmd = new OdbcCommand("CALL LISTAR_RECETAS_SEARCH(?)", cnx);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@busqueda", OdbcType.VarChar).Value = busqueda;
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



        public DataTable Listar_Unidad_Medida()
        {
            using (var cnx = ASG_DB.connectionResult())
            {
                try
                {
                    OdbcDataReader resultado = null;
                    DataTable tabla = new DataTable();
                    OdbcCommand cmd = new OdbcCommand("CALL LISTAR_UNIDAD_MEDIDA()", cnx);
                    cmd.CommandType = CommandType.StoredProcedure;
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

        public int Get_Id_Medida(string nombre)
        {
            using (var cnx = ASG_DB.connectionResult())
            {
                try
                {
                    int ID = 0;
                    OdbcDataReader resultado = null;
                    OdbcCommand cmd = new OdbcCommand("CALL GET_ID_UNIDAD_MEDIDA(?)", cnx);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@NOMBRE", OdbcType.VarChar).Value = nombre;
                    resultado = cmd.ExecuteReader();

                    if (resultado.HasRows)
                    {
                        ID = resultado.GetInt32(0);
                    }

                    return ID;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public int Crear_Receta(Receta receta, List<StructDetalle.SDetalleReceta> detalle)
        {
            using (var cnx = ASG_DB.connectionResult())
            {
                int respuesta = 0;
                OdbcTransaction transaccion = cnx.BeginTransaction();

                try
                {
                    OdbcCommand cmd = new OdbcCommand("CALL CREA_ENCABEZADO_RECETA(?,?,?)", cnx);
                    cmd.Transaction = transaccion;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = cnx;
                    cmd.Parameters.Add("@user", OdbcType.VarChar).Value = Permisos.Obtener_Usuario();
                    cmd.Parameters.Add("@idMerc", OdbcType.VarChar).Value = receta.Id_mercaderia;
                    cmd.Parameters.Add("@descripcion", OdbcType.VarChar).Value = receta.Descripcion;

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        //Detalle receta
                        foreach (var item in detalle)
                        {
                            OdbcCommand cmd2 = new OdbcCommand("CALL INSERTA_DETALLE_RECETA(?,?)", cnx);
                            cmd2.Transaction = transaccion;
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Parameters.Add("@idMateria", OdbcType.VarChar).Value = item.id_materia_prima;
                            cmd2.Parameters.Add("@cantidad", OdbcType.Double).Value = item.cantidad;
                            cmd2.ExecuteNonQuery();
                        }
                    }


                    respuesta = 1;
                    transaccion.Commit();

                    return respuesta;

                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    throw ex;
                }
            }
        }

        public int Editar_Encabezado_Receta(Receta receta)
        {
            using (var cnx = ASG_DB.connectionResult())
            {
                int respuesta = 0;

                try
                {
                    OdbcCommand cmd = new OdbcCommand("CALL UPDATE_ENCABEZADO_RECETA(?,?)", cnx);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = cnx;
                    cmd.Parameters.Add("@id", OdbcType.Int).Value = receta.Id_receta;
                    cmd.Parameters.Add("@descripcion", OdbcType.VarChar).Value = receta.Descripcion;
                    cmd.ExecuteNonQuery();

                    respuesta = 1;

                    return respuesta;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }



        public int Editar_Detalle_Delete(Receta receta, List<int> eliminados)
        {
            using (var cnx = ASG_DB.connectionResult())
            {
                int respuesta = 0;

                try
                {

                    foreach (var item in eliminados)
                    {
                        OdbcCommand cmd = new OdbcCommand("CALL UPDATE_DETALLE_RECETA_DELETE(?, ?)", cnx);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = cnx;
                        cmd.Parameters.Add("@id", OdbcType.Int).Value = receta.Id_receta;
                        cmd.Parameters.Add("@idMateria", OdbcType.Int).Value = item;
                        cmd.ExecuteNonQuery();

                    }

                    respuesta = 1;

                    return respuesta;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public int Editar_Detalle_Insert(Receta receta, List<StructDetalle.SDetalleReceta> insertados)
        {
            using (var cnx = ASG_DB.connectionResult())
            {
                int respuesta = 0;

                try
                {

                    foreach (var item in insertados)
                    {
                        OdbcCommand cmd = new OdbcCommand("CALL UPDATE_DETALLE_RECETA_INSERT(?, ?, ?)", cnx);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = cnx;
                        cmd.Parameters.Add("@id", OdbcType.Int).Value = receta.Id_receta;
                        cmd.Parameters.Add("@idMateria", OdbcType.Int).Value = item.id_materia_prima;
                        cmd.Parameters.Add("@cantidad", OdbcType.Double).Value = item.cantidad;
                        cmd.ExecuteNonQuery();

                    }

                    respuesta = 1;

                    return respuesta;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }



        public bool Cambia_Estado_Receta_Delete(int idReceta)
        {
            using (var cnx = ASG_DB.connectionResult())
            {
                try
                {
                    bool resp = false;
                    OdbcCommand cmd = new OdbcCommand("CALL CAMBIA_ESTADO_RECETA_DELETE(?)", cnx);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", OdbcType.Int).Value = idReceta;

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


    }




}

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
    public class dataLogin
    {
        public int Verificar_Usuario(Users _user)
        {
            using (var cnx = ASG_DB.connectionResult())
            {
                OdbcDataReader resultado;
                int verify = 0;

                try
                {
                    OdbcCommand cmd = new OdbcCommand("CALL GETAUTHENTICATION(?, ?)", cnx);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@usuario", OdbcType.VarChar).Value = _user.Id_usuario;
                    cmd.Parameters.Add("@password", OdbcType.VarChar).Value = _user.Password;
                    resultado = cmd.ExecuteReader();

                    if (resultado.HasRows)
                    {
                        while (resultado.Read())
                        {
                            verify = resultado.GetInt32(0);
                        }
                    }

                    return verify;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public bool Consultar_Permisos(Users _user)
        {

            bool resp = false;

            using (var cnx = ASG_DB.connectionResult())
            {
                try
                {
                    OdbcDataReader resultado;
                    OdbcCommand cmd = new OdbcCommand("call obtener_permisos(?)", cnx);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@usuario", OdbcType.VarChar).Value = _user.Id_usuario;
                    resultado = cmd.ExecuteReader();

                    if (resultado.HasRows)
                    {
                        string idUser = resultado.GetString(0);
                        string nombre = resultado.GetString(1);
                        string idRol = resultado.GetString(2);

                        Permisos.Id_rol = idRol;
                        Permisos.Id_user = idUser;
                        Permisos.Nombre_User = nombre;

                        while (resultado.Read())
                        {
                            int modulo = resultado.GetInt32(3);
                            int insertar = resultado.GetInt32(4);
                            int editar = resultado.GetInt32(5);
                            int eliminar = resultado.GetInt32(6);
                            int consultar = resultado.GetInt32(7);

                            Permisos.insertarPermisos(modulo, insertar, editar, eliminar, consultar);
                        }

                        resp = true;
                        return resp;
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

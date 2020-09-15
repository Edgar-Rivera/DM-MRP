using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASG.ASG.Entity
{
    public class Permisos
    {
            //Atributos
            private static string id_user;
            private static string nombre_user;
            private static string id_rol;

            private struct SPermisos
            {
                public int id_modulo;
                public int insertar;
                public int editar;
                public int eliminar;
                public int consultar;
            }

            private static List<SPermisos> lista = new List<SPermisos> { };

            //Propiedades
            public static string Id_user { set => id_user = value; }
            public static string Id_rol { set => id_rol = value; }
            public static string Nombre_User { set => nombre_user = value; }

            //Métodos
            public static void insertarPermisos(int _modulo, int _insertar, int _editar, int _eliminar, int _consultar)
            {
                SPermisos nodoPermisos = new SPermisos
                {
                    id_modulo = _modulo,
                    insertar = _insertar,
                    editar = _editar,
                    eliminar = _eliminar,
                    consultar = _consultar
                };

                lista.Add(nodoPermisos);
            }

            public static List<int> Obtener_Permisos(int _modulo)
            {

                List<int> listaResult = new List<int> { };

                var busqueda = lista.Find(modulo => modulo.id_modulo == _modulo);

                if (busqueda.id_modulo != 0)
                {
                    listaResult.Add(busqueda.insertar);
                    listaResult.Add(busqueda.editar);
                    listaResult.Add(busqueda.consultar);
                    listaResult.Add(busqueda.eliminar);
                }

                return listaResult;

            }

            public static string Obtener_Usuario()
            {
                return id_user;
            }

            public static string Obtener_Rol()
            {
                return id_rol;
            }

            public static string obtener_Nombre_Usuario()
            {
                return nombre_user;
            }


        }

    }

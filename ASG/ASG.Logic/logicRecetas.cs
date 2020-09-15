using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ASG.ASG.Data;
using ASG.ASG.Entity;


namespace ASG.ASG.Logic
{
    public class logicRecetas
    {
        public static DataTable Listar_Mercaderia_Search(string busqueda)
        {
            dataRecetas dtRecetas = new dataRecetas();
            return dtRecetas.Listar_Mercaderia_Search(busqueda);
        } 
        public static DataTable Listar_Materia_Search(string busqueda)
        {
            dataRecetas dtRecetas = new dataRecetas();
            return dtRecetas.Listar_Materia_Search(busqueda);
        }        
        
        public static DataTable Listar_Detalle_Receta(int idReceta)
        {
            dataRecetas dtRecetas = new dataRecetas();
            return dtRecetas.Listar_Detalle_Receta(idReceta);
        }

        public static DataTable Listar_Receta_Search(string busqueda)
        {
            dataRecetas dtRecetas = new dataRecetas();
            return dtRecetas.Listar_Receta_Search(busqueda);
        }

        public static DataTable Listar_Unidad_Medida()
        {
            dataRecetas dtRecetas = new dataRecetas();
            return dtRecetas.Listar_Unidad_Medida();
        }

        public static int Get_Id_Medida(string nombre)
        {
            dataRecetas dtRecetas = new dataRecetas();
            return dtRecetas.Get_Id_Medida(nombre);
        }

        public static int Crear_Receta(string idMercaderia, string descripcion, List<StructDetalle.SDetalleReceta> detalle)
        {
            dataRecetas dtRecetas = new dataRecetas();
            Receta obj = new Receta();
            obj.Id_mercaderia = idMercaderia;
            obj.Descripcion = descripcion;
            
            return dtRecetas.Crear_Receta(obj, detalle);
        }

        public static int Editar_Encabezado_Receta(int idReceta, string descripcion)
        {
            dataRecetas dtRecetas = new dataRecetas();
            Receta obj = new Receta();
            obj.Id_receta = idReceta;
            obj.Descripcion = descripcion;

            return dtRecetas.Editar_Encabezado_Receta(obj);
        }

        public static int Editar_Detalle_Delete(int idReceta, List<int> eliminados)
        {
            dataRecetas dtRecetas = new dataRecetas();
            Receta obj = new Receta();
            obj.Id_receta = idReceta;

            return dtRecetas.Editar_Detalle_Delete(obj, eliminados);
        }


        public static int Editar_Detalle_Insert(int idReceta, List<StructDetalle.SDetalleReceta> insertados)
        {
            dataRecetas dtRecetas = new dataRecetas();
            Receta obj = new Receta();
            obj.Id_receta = idReceta;

            return dtRecetas.Editar_Detalle_Insert(obj, insertados);
        }



        public static bool Cambia_Estado_Receta_Delete(int idReceta)
        {
            dataRecetas dtRecetas = new dataRecetas();
            return dtRecetas.Cambia_Estado_Receta_Delete(idReceta);
        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ASG.ASG.Data;

namespace ASG.ASG.Logic
{
    class logicAreas
    {

        public static DataTable Listar_Areas(string busqueda)
        {
            dataAreas dtAreas = new dataAreas();
            return dtAreas.Listar_Areas(busqueda);
        }

        public static DataTable Listar_Encargados(string busqueda)
        {
            dataAreas dtAreas = new dataAreas();
            return dtAreas.Listar_Encargados(busqueda);
        }

        public static bool Cambia_Estado_Area_Delete(int idArea)
        {
            dataAreas dtAreas = new dataAreas();
            return dtAreas.Cambia_Estado_Area_Delete(idArea);
        }

        public static int Crear_Area(string idEncargado, string nombreArea, string descripcionArea)
        {
            dataAreas dtAreas = new dataAreas();
            return dtAreas.Crear_Area(idEncargado, nombreArea, descripcionArea);
        }

        public static int Editar_Area(string id, string idEncargado, string nombreArea, string descripcionArea)
        {
            dataAreas dtAreas = new dataAreas();
            return dtAreas.Edita_Area(id, idEncargado, nombreArea, descripcionArea);
        }

    }
}

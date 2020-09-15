using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASG.ASG.Entity
{
    public class Receta
    {
        //Atributos
        private int id_receta;
        private string id_usuario;
        private string id_mercaderia;
        private string descripcion;
        private string fechaCreacion;
        private int estado;

        //Propiedades
        public int Id_receta { get => id_receta; set => id_receta = value; }
        public string Id_usuario { get => id_usuario; set => id_usuario = value; }
        public string Id_mercaderia { get => id_mercaderia; set => id_mercaderia = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
        public int Estado { get => estado; set => estado = value; }
    }
}

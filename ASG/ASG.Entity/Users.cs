using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASG.ASG.Entity
{
    public class Users
    {
        private string _id_usuario;
        private int _id_rol;
        private string _password;
        private string _nombre;
        private string _email;
        private int _estado_usuario;
        private string _img_usuario;
        private int estado_tupla;

        public Users(string _user)
        {
            Id_usuario = _user;
        }

        public Users(string _user , string _password)
        {
            Id_usuario = _user;
            Password = _password;
        }

        public string Id_usuario { get => _id_usuario; set => _id_usuario = value; }
        public int Id_rol { get => _id_rol; set => _id_rol = value; }
        public string Password { get => _password; set => _password = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Email { get => _email; set => _email = value; }
        public int Estado_usuario { get => _estado_usuario; set => _estado_usuario = value; }
        public string Img_usuario { get => _img_usuario; set => _img_usuario = value; }
        public int Estado_tupla { get => estado_tupla; set => estado_tupla = value; }
    }
}

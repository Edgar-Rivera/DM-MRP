using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASG.ASG.Data;
using ASG.ASG.Entity;

namespace ASG.ASG.Logic
{
    public static class logicLogin
    {
        public static int Login_Usuario(string _user, string _password)
        {

            dataLogin dlogin = new dataLogin();
            Users obj = new Users(_user, _password);

            int result = dlogin.Verificar_Usuario(obj);

            //Usuario Existente y esta activo
            if (result == 1)
            {
                bool tienePermisos = dlogin.Consultar_Permisos(obj);

                if (tienePermisos)
                {
                    return 1; //Tiene permisos existentes
                }
                else
                {
                    return 3; // No tiene permisos existentes
                }

            }
            else
            {
                return result; // Usuario inhabilitado o inexistente
            }

        }
    }
}

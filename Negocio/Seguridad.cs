using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public static class Seguridad
    {
        public static bool sesionActiva(object user)
        {
            Usuario nuevo = user != null ? (Usuario)user : null;
            if (nuevo != null && nuevo.Id != 0)
                return true;
            else
                return false;
        }

        public static bool esAdmin(object user)
        {
            Usuario nuevo = user != null ? (Usuario)user : null;
            return nuevo != null ? nuevo.Admin : false;
        }

    }
}

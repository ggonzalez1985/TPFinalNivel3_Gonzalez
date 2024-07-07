using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class UsuarioNegocio
    {


        public int NuevoUsuario(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO USERS(email, pass, nombre, apellido, urlImagenPerfil, admin) VALUES (@email, @pass, @nombre, @apellido, @urlImagenPerfil,0)");

                datos.setearParametro("@email", usuario.Email);
                datos.setearParametro("@pass", usuario.Password);
                datos.setearParametro("@nombre", usuario.Nombre);
                datos.setearParametro("@apellido", usuario.Apellido);
                datos.setearParametro("@urlImagenPerfil", "");
               
                return datos.ejecutarAccionScalar();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }


    }
}

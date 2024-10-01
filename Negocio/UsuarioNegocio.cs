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

        //public int NuevoUsuario(Usuario usuario)
        //{
        //    AccesoDatos datos = new AccesoDatos();

        //    try
        //    {
        //        datos.setearConsulta("INSERT INTO USERS(email, pass, nombre, apellido, urlImagenPerfil, admin) output inserted.id VALUES (@email, @pass, @nombre, @apellido, @urlImagenPerfil,0)");

        //        datos.setearParametro("@email", usuario.Email);
        //        datos.setearParametro("@pass", usuario.Password);
        //        datos.setearParametro("@nombre", usuario.Nombre);
        //        datos.setearParametro("@apellido", usuario.Apellido);
        //        datos.setearParametro("@urlImagenPerfil", "");

        //        return datos.ejecutarAccionScalar();

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }

        //}

        public int AgregarUsuarioSP(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Llamamos al procedimiento almacenado
                datos.setearProcedimiento("AgregarUsuarioSP");

                datos.setearParametro("@Email", usuario.Email);
                datos.setearParametro("@Password", usuario.Password);
                datos.setearParametro("@Nombre", usuario.Nombre);
                datos.setearParametro("@Apellido", usuario.Apellido);
                datos.setearParametro("@UrlImagenPerfil", "");

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

        //public bool Login(Usuario usuario)
        //{
        //    AccesoDatos datos = new AccesoDatos();

        //    try
        //    {
        //        datos.setearConsulta("Select id, email, pass, nombre, apellido, urlImagenPerfil, admin from USERS Where email = @email And pass = @pass");
        //        datos.setearParametro("@email", usuario.Email);
        //        datos.setearParametro("@pass", usuario.Password);
        //        datos.ejecutarLectura();

        //        if (datos.Lector.Read())
        //        {
        //            usuario.Id = (int)datos.Lector["id"];
        //            usuario.Admin = (bool)datos.Lector["admin"];
        //            usuario.Email = (string)datos.Lector["email"];
        //            usuario.Password = (string)datos.Lector["pass"];
        //            if (!(datos.Lector["urlImagenPerfil"] is DBNull))
        //                usuario.ImagenUrl = (string)datos.Lector["urlImagenPerfil"];
        //            if (!(datos.Lector["nombre"] is DBNull))
        //                usuario.Nombre = (string)datos.Lector["nombre"];
        //            if (!(datos.Lector["apellido"] is DBNull))
        //                usuario.Apellido = (string)datos.Lector["apellido"];

        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }

        //}

        public bool Login(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Llamamos al procedimiento almacenado
                datos.setearProcedimiento("LoginUsuarioSP");
                datos.setearParametro("@Email", usuario.Email);
                datos.setearParametro("@Password", usuario.Password);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario.Id = (int)datos.Lector["id"];
                    usuario.Admin = (bool)datos.Lector["admin"];
                    usuario.Email = (string)datos.Lector["email"];
                    usuario.Password = (string)datos.Lector["pass"];
                    usuario.ImagenUrl = datos.Lector["urlImagenPerfil"] as string;
                    usuario.Nombre = datos.Lector["nombre"] as string;
                    usuario.Apellido = datos.Lector["apellido"] as string;

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        //public bool EmailExists(string email)
        //{
        //    AccesoDatos datos = new AccesoDatos();

        //    try
        //    {
        //        datos.setearConsulta("SELECT id FROM USERS WHERE email = @Email");
        //        datos.setearParametro("@Email", email);
        //        datos.ejecutarLectura();

        //        if (datos.Lector.Read())
        //        {
        //            return true;
        //        }

        //        return false;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }
        //}

        public bool EmailExists(string email)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Llamamos al procedimiento almacenado
                datos.setearProcedimiento("EmailExistsSP");
                datos.setearParametro("@Email", email);
                datos.ejecutarLectura();

                // Verificamos si se encontró algún registro
                return datos.Lector.Read();
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

        //public bool PassExists(int userid, string pass)
        //{
        //    AccesoDatos datos = new AccesoDatos();

        //    try
        //    {
        //        datos.setearConsulta("SELECT id FROM USERS WHERE id = @userid AND pass = @pass");
        //        datos.setearParametro("@userid", userid);
        //        datos.setearParametro("@pass", pass);
        //        datos.ejecutarLectura();

        //        if (datos.Lector.Read())
        //        {
        //            return true;
        //        }

        //        return false;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }
        //}

        public bool PassExists(int userid, string pass)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Llamamos al procedimiento almacenado
                datos.setearProcedimiento("PassExistsSP");
                datos.setearParametro("@userid", userid);
                datos.setearParametro("@pass", pass);
                datos.ejecutarLectura();

                // Verificamos si se encontró algún registro
                return datos.Lector.Read();
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

        //public bool modificarDatos(Usuario usuario)
        //{
        //    AccesoDatos datos = new AccesoDatos();

        //    try
        //    {
        //        datos.setearConsulta("UPDATE Users SET Nombre = @Nombre, Apellido = @Apellido WHERE Email = @Email");
        //        datos.setearParametro("@Nombre", usuario.Nombre);
        //        datos.setearParametro("@Apellido", usuario.Apellido);
        //        datos.setearParametro("@Email", usuario.Email);
        //        datos.ejecutarLectura();

        //            return true;

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }
        //}

        public bool modificarDatos(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Llamamos al procedimiento almacenado
                datos.setearProcedimiento("ModificarDatosSP");
                datos.setearParametro("@Nombre", usuario.Nombre);
                datos.setearParametro("@Apellido", usuario.Apellido);
                datos.setearParametro("@Email", usuario.Email);

                // Ejecutamos la acción
                datos.ejecutarAccion();
                return true;
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

        //public bool modificarPassword(int usuarioId, string nuevaPassword)
        //{
        //    AccesoDatos datos = new AccesoDatos();

        //    try
        //    {
        //        datos.setearConsulta("UPDATE Users SET pass = @pass WHERE Id = @Id");
        //        datos.setearParametro("@pass", nuevaPassword);
        //        datos.setearParametro("@Id", usuarioId);
        //        datos.ejecutarLectura();

        //        return true;

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }
        //}

        public bool modificarPassword(int usuarioId, string nuevaPassword)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Llamamos al procedimiento almacenado
                datos.setearProcedimiento("ModificarPasswordSP");
                datos.setearParametro("@Id", usuarioId);
                datos.setearParametro("@pass", nuevaPassword);

                // Ejecutamos la acción
                datos.ejecutarAccion();
                return true;
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

        //public bool modificarImagen(Usuario usuario)
        //{
        //    AccesoDatos datos = new AccesoDatos();

        //    try
        //    {
        //        datos.setearConsulta("UPDATE Users SET urlImagenPerfil = @urlImagenPerfil WHERE Email = @Email");
        //        datos.setearParametro("@urlImagenPerfil", usuario.ImagenUrl);
        //        datos.setearParametro("@Email", usuario.Email);
        //        datos.ejecutarLectura();

        //        return true;

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }
        //}

        public bool modificarImagen(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Llamamos al procedimiento almacenado
                datos.setearProcedimiento("ModificarImagenPerfilSP");
                datos.setearParametro("@urlImagenPerfil", usuario.ImagenUrl);
                datos.setearParametro("@Email", usuario.Email);

                // Ejecutamos la acción
                datos.ejecutarAccion();
                return true;
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

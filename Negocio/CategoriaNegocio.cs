using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CategoriaNegocio
    {
        //public List<Categoria> listar()
        //{
        //    List<Categoria> lista = new List<Categoria>();
        //    AccesoDatos datos = new AccesoDatos();

        //    try
        //    {
        //        datos.setearConsulta("Select Id, Descripcion From CATEGORIAS order by Descripcion ASC");
        //        datos.ejecutarLectura();

        //        while (datos.Lector.Read())
        //        {
        //            Categoria aux = new Categoria();
        //            aux.Id = (int)datos.Lector["Id"];
        //            aux.Descripcion = (string)datos.Lector["Descripcion"];

        //            lista.Add(aux);
        //        }

        //        return lista;
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

        public List<Categoria> listar()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Llamamos al procedimiento almacenado
                datos.setearProcedimiento("listarSP");

                // Ejecutamos la lectura
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(aux);
                }

                return lista;
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

        //public List<Categoria> listarDisponibles()
        //{
        //    List<Categoria> lista = new List<Categoria>();
        //    AccesoDatos datos = new AccesoDatos();

        //    try
        //    {
        //        datos.setearConsulta("Select Id, Descripcion From CATEGORIAS order by Descripcion ASC");
        //        datos.ejecutarLectura();

        //        while (datos.Lector.Read())
        //        {
        //            Categoria aux = new Categoria();
        //            aux.Id = (int)datos.Lector["Id"];
        //            aux.Descripcion = (string)datos.Lector["Descripcion"];

        //            lista.Add(aux);
        //        }

        //        return lista;
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

        //public bool Agregar(string descripcion)
        //{
        //    AccesoDatos datos = new AccesoDatos();

        //    try
        //    {
        //        datos.setearConsulta("INSERT INTO CATEGORIAS(Descripcion)VALUES(@descripcion)");

        //        datos.setearParametro("@descripcion", descripcion);
                
        //        datos.ejecutarAccion();

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

        public bool AgregarCategoriaSP(string descripcion)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Llamamos al procedimiento almacenado
                datos.setearProcedimiento("AgregarCategoriaSP");

                // Establecemos el parámetro
                datos.setearParametro("@descripcion", descripcion);

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

        //public bool modificar(Categoria nuevo)
        //{
        //    AccesoDatos datos = new AccesoDatos();
        //    try
        //    {
        //        datos.setearConsulta("update CATEGORIAS set Descripcion = @desc Where Id = @id");
                
        //        datos.setearParametro("@desc", nuevo.Descripcion);
                
        //        datos.setearParametro("@id", nuevo.Id);

        //        datos.ejecutarAccion();

        //        return true ;
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

        public bool ModificarCategoriaSP(Categoria nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Llamamos al procedimiento almacenado
                datos.setearProcedimiento("ModificarCategoriaSP");

                // Establecemos los parámetros
                datos.setearParametro("@id", nuevo.Id);
                datos.setearParametro("@desc", nuevo.Descripcion);

                // Ejecutamos la acción
                datos.ejecutarAccion();

                return true;
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

        //public bool eliminar (int Id)
        //{
        //    AccesoDatos datos = new AccesoDatos ();
        //    try
        //    {
        //        datos.setearConsulta("delete from CATEGORIAS where Id = @id");
        //        datos.setearParametro("@id", Id);
        //        datos.ejecutarAccion();
        //        return true;
                
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

        public bool EliminarCategoriaSP(int Id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Llamamos al procedimiento almacenado
                datos.setearProcedimiento("EliminarCategoriaSP");

                // Establecemos el parámetro
                datos.setearParametro("@id", Id);

                // Ejecutamos la acción
                datos.ejecutarAccion();

                return true;
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


    }
}

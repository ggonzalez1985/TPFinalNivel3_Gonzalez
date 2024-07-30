using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> listar()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Select Id, Descripcion From MARCAS order by Descripcion ASC");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();
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

        public bool modificar(Marca nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update MARCAS set Descripcion = @desc Where Id = @id");
                datos.setearParametro("@desc", nuevo.Descripcion);
                datos.setearParametro("@id", nuevo.Id);

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


        public bool ExisteDescripcion(string descripcion)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Consulta para verificar si ya existe la descripción
                datos.setearConsulta("SELECT COUNT(*) FROM MARCAS WHERE Descripcion = @desc");
                datos.setearParametro("@desc", descripcion);

                // Ejecutar la consulta y obtener el resultado
                datos.ejecutarLectura();
                datos.Lector.Read();

                // Si el conteo es mayor que 0, significa que la descripción ya existe
                int cantidad = datos.Lector.GetInt32(0);
                return cantidad > 0;
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

        public bool Agregar(Marca nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO MARCAS(Descripcion)VALUES(@descripcion)");
                datos.setearParametro("@descripcion", nuevo.Descripcion);
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

        public bool eliminar(int Id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("delete from MARCAS where Id = @id");
                datos.setearParametro("@id", Id);
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

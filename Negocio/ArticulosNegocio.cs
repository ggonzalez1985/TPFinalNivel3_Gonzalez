using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dominio;

namespace Negocio
{
    public class ArticulosNegocio
    {

        public List<Articulo> Listararticulos()
        {
            List<Articulo> Listado = new List<Articulo>();
            SqlConnection conexion = new SqlConnection();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, M.Descripcion Marca, A.IdCategoria, C.Descripcion Categoria, A.ImagenUrl, A.Precio FROM ARTICULOS A, CATEGORIAS C, MARCAS M WHERE A.IdMarca = M.Id AND A.IdCategoria = C.Id order by Categoria, Marca");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.IdMarca = new Marca();
                    aux.IdMarca.Id = (int)datos.Lector["IdMarca"];
                    aux.IdMarca.Descripcion = (string)datos.Lector["Marca"];

                    aux.IdCategoria = new Categoria();
                    aux.IdCategoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.IdCategoria.Descripcion = (string)datos.Lector["Categoria"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    Listado.Add(aux);
                }

                conexion.Close();
                return Listado;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO ARTICULOS(Codigo,Nombre,Descripcion,IdMarca,IdCategoria,ImagenUrl,Precio)VALUES(@idCodigo, @Nombre, @descripcion, @IdMarca, @IdCategoria, @ImagenUrl, @Precio)");

                datos.setearParametro("@idCodigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@descripcion", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.IdMarca.Id);
                datos.setearParametro("@IdCategoria", nuevo.IdCategoria.Id);
                datos.setearParametro("@ImagenUrl", nuevo.ImagenUrl);
                datos.setearParametro("@Precio", nuevo.Precio);

                datos.ejecutarAccion();

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

        public int ObtenerIdArticulos(string CodAux)
        {
            //Articulo ElArticulo = new Articulo();
            int codigo = new int();
            SqlConnection conexion = new SqlConnection();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Select Id From ARTICULOS Where Codigo LIKE '" + CodAux + "'");

                datos.ejecutarLectura();

                if (datos.Lector != null)
                {
                    if (datos.Lector.HasRows)
                    {
                        datos.Lector.Read();
                        codigo = (int)datos.Lector["Id"];
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

            return codigo;
        }

        public void EditarArticulo(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE ARTICULOS SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria ,ImagenUrl = @ImagenUrl, Precio = @Precio Where Id = @id");

                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.IdMarca.Id);
                datos.setearParametro("@IdCategoria", nuevo.IdCategoria.Id);
                datos.setearParametro("@ImagenUrl", nuevo.ImagenUrl);
                datos.setearParametro("@Precio", nuevo.Precio);
                datos.setearParametro("@id", nuevo.Id);

                datos.ejecutarAccion();


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

        public void Eliminar(int Id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from ARTICULOS where Id = @id");
                datos.setearParametro("@id", Id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<Articulo> filtrar(string Categoria, string Marca, string PrecioMin, string PrecioMax)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            SqlConnection conexion = new SqlConnection();
            string consulta;

            try
            {
                int precioMinimo = 0; // Valor predeterminado
                int precioMaximo = 0;

                if (!string.IsNullOrEmpty(PrecioMin))
                {
                    // Eliminar puntos y convertir a entero
                    if (int.TryParse(PrecioMin.Replace(".", ""), out precioMinimo))
                    {

                    }
                    else
                    {
                        // Manejar el caso en que la conversión no sea exitosa
                        // Por ejemplo, mostrar un mensaje de error o asignar un valor predeterminado
                    }
                }

                if (!string.IsNullOrEmpty(PrecioMax))
                {
                    // Eliminar puntos y convertir a entero
                    if (int.TryParse(PrecioMax.Replace(".", ""), out precioMaximo))
                    {

                    }
                    else
                    {
                        // Manejar el caso en que la conversión no sea exitosa
                        // Por ejemplo, mostrar un mensaje de error o asignar un valor predeterminado
                    }
                }

                consulta = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, M.Descripcion Marca, A.IdCategoria, C.Descripcion Categoria, A.ImagenUrl, A.Precio FROM ARTICULOS A, CATEGORIAS C, MARCAS M WHERE A.IdMarca = M.Id AND A.IdCategoria = C.Id";

                // Añadir condiciones de filtro si se especifican los parámetros
                if (!string.IsNullOrEmpty(Categoria))
                {
                    consulta += $" AND C.Descripcion = '{Categoria}'";
                }

                if (!string.IsNullOrEmpty(Marca))
                {
                    consulta += $" AND M.Descripcion = '{Marca}'";
                }

                if (!string.IsNullOrEmpty(PrecioMin))
                {
                    consulta += $" AND A.Precio >= {precioMinimo}";
                }

                if (!string.IsNullOrEmpty(PrecioMax))
                {
                    consulta += $" AND A.Precio <= {precioMaximo}";
                }

                consulta += " ORDER BY C.Descripcion, M.Descripcion";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.IdMarca = new Marca();
                    aux.IdMarca.Id = (int)datos.Lector["IdMarca"];
                    aux.IdMarca.Descripcion = (string)datos.Lector["Marca"];

                    aux.IdCategoria = new Categoria();
                    aux.IdCategoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.IdCategoria.Descripcion = (string)datos.Lector["Categoria"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(aux);
                }

                conexion.Close();

                return lista;
            }
            catch (Exception)
            {
                throw;
            }



        }
    }
}

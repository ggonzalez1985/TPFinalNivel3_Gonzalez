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
using System.Web;

namespace Negocio
{
    public class ArticulosNegocio
    {

        public List<Articulo> Listararticulos(string id = "")
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
                    {
                        // Obtener la URL de la imagen del lector
                        string imagenUrl = (string)datos.Lector["ImagenUrl"];

                        // Verificar si la URL es null o vacía
                        if (!string.IsNullOrEmpty(imagenUrl))
                        {
                            // Verificar si la URL ya es una URL completa
                            if (imagenUrl.StartsWith("http://") || imagenUrl.StartsWith("https://"))
                            {
                                // La URL ya es completa, asignarla directamente
                                aux.ImagenUrl = imagenUrl;
                            }
                            else
                            {
                                // La URL es relativa, agregar el prefijo
                                aux.ImagenUrl = "~/Images/" + imagenUrl;
                            }
                        }
                        else
                        {
                            // Manejar el caso si la URL es null o vacía
                            aux.ImagenUrl = "~/Images/img-nd.jpg";
                        }
                    }
                    else
                    {
                        // Manejar el caso si no hay URL de imagen
                        aux.ImagenUrl = "~/Images/img-nd.jpg";
                    }


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

        public bool Agregar(Articulo nuevo)
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


        public bool Guardar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO ARTICULOS(Codigo,Nombre,Descripcion,IdMarca,IdCategoria,Precio) VALUES(@idCodigo, @Nombre, @descripcion, @IdMarca, @IdCategoria, @Precio)");

                datos.setearParametro("@idCodigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@descripcion", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.IdMarca.Id);
                datos.setearParametro("@IdCategoria", nuevo.IdCategoria.Id);
                datos.setearParametro("@Precio", nuevo.Precio);

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


        public int GuardarConImg(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO ARTICULOS(Codigo,Nombre,Descripcion,IdMarca,IdCategoria, ImagenUrl,Precio) output inserted.id VALUES(@idCodigo, @Nombre, @descripcion, @IdMarca, @IdCategoria, @ImagenUrl, @Precio)");

                datos.setearParametro("@idCodigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@descripcion", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.IdMarca.Id);
                datos.setearParametro("@IdCategoria", nuevo.IdCategoria.Id);
                datos.setearParametro("@ImagenUrl", nuevo.ImagenUrl);
                datos.setearParametro("@Precio", nuevo.Precio);

                return datos.ejecutarAccionScalar();

                //return true;

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

        public bool EditarArticulo(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE ARTICULOS SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, ImagenUrl = @ImagenUrl, Precio = @Precio Where Id = @id");

                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.IdMarca.Id);
                datos.setearParametro("@IdCategoria", nuevo.IdCategoria.Id);
                datos.setearParametro("@ImagenUrl", nuevo.ImagenUrl);
                datos.setearParametro("@Precio", nuevo.Precio);
                datos.setearParametro("@id", nuevo.Id);

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

        public bool ActualizarImagenArticulo(int id, string imagenFinal)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE ARTICULOS SET ImagenUrl = @imagenFinal Where Id = @id");

                datos.setearParametro("@imagenFinal", imagenFinal);
                datos.setearParametro("@id", id);

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

        public bool Eliminar(int Id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from ARTICULOS where Id = @id");
                datos.setearParametro("@id", Id);
                datos.ejecutarAccion();
                return true ;
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

        public Articulo ListararticuloId(string id = "")
        {
            Articulo articulo = new Articulo();
            SqlConnection conexion = new SqlConnection();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, M.Descripcion Marca, A.IdCategoria, C.Descripcion Categoria, A.ImagenUrl, A.Precio FROM ARTICULOS A, CATEGORIAS C, MARCAS M WHERE A.IdMarca = M.Id AND A.IdCategoria = C.Id and A.Id = @Id order by Categoria, Marca");

                datos.setearParametro("@Id", id);

                datos.ejecutarLectura();

                if (datos.Lector.HasRows)
                {
                    datos.Lector.Read(); // Leer la primera fila

                    articulo.Id = (int)datos.Lector["Id"];
                    articulo.Codigo = (string)datos.Lector["Codigo"];
                    articulo.Nombre = (string)datos.Lector["Nombre"];
                    articulo.Descripcion = (string)datos.Lector["Descripcion"];

                    articulo.IdMarca = new Marca();
                    articulo.IdMarca.Id = (int)datos.Lector["IdMarca"];
                    articulo.IdMarca.Descripcion = (string)datos.Lector["Marca"];

                    articulo.IdCategoria = new Categoria();
                    articulo.IdCategoria.Id = (int)datos.Lector["IdCategoria"];
                    articulo.IdCategoria.Descripcion = (string)datos.Lector["Categoria"];
                    articulo.Precio = (decimal)datos.Lector["Precio"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    {
                        articulo.ImagenUrl = (string)datos.Lector["ImagenUrl"];  
                    }
                }
                return articulo;
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

        //public bool AgregarFavorito(int userId, int idArticulo)
        //{

        //    bool operacionExitosa = false;
        //    AccesoDatos datos = new AccesoDatos();

        //    try
        //    {
        //        datos.setearConsulta("INSERT INTO FAVORITOS (IdUser, IdArticulo) VALUES (@IdUser, @IdArticulo)");

        //        datos.setearParametro("@IdUser", userId);
        //        datos.setearParametro("@IdArticulo", idArticulo);

        //        datos.ejecutarAccion();

        //        return operacionExitosa = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error al agregar favorito: " + ex.Message);
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }

        //    return operacionExitosa;
        //}

        public bool ToggleFavorito(int userId, int idArticulo)
        {
            bool operacionExitosa = false;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Verificar si el artículo ya está en favoritos
                datos.setearConsulta("SELECT COUNT(*) FROM FAVORITOS WHERE IdUser = @IdUser AND IdArticulo = @IdArticulo");
                datos.setearParametro("@IdUser", userId);
                datos.setearParametro("@IdArticulo", idArticulo);

                datos.ejecutarLectura();

                bool existeFavorito = false;
                if (datos.Lector.Read())
                {
                    existeFavorito = datos.Lector.GetInt32(0) > 0;
                }
                datos.cerrarConexion();

                datos.limpiarParametros();

                if (existeFavorito)
                {
                    // Eliminar de favoritos
                    datos.setearConsulta("DELETE FROM FAVORITOS WHERE IdUser = @IdUser AND IdArticulo = @IdArticulo");
                }
                else
                {
                    // Agregar a favoritos
                    datos.setearConsulta("INSERT INTO FAVORITOS (IdUser, IdArticulo) VALUES (@IdUser, @IdArticulo)");
                }

                datos.setearParametro("@IdUser", userId);
                datos.setearParametro("@IdArticulo", idArticulo);

                datos.ejecutarAccion();

                operacionExitosa = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al modificar favorito: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return operacionExitosa;
        }

        //public bool EsFavorito(int userId, int idArticulo)
        //{
        //    AccesoDatos datos = new AccesoDatos();
        //    try
        //    {
        //        datos.setearConsulta("SELECT COUNT(*) FROM FAVORITOS WHERE IdUser = @IdUser AND IdArticulo = @IdArticulo");
        //        datos.setearParametro("@IdUser", userId);
        //        datos.setearParametro("@IdArticulo", idArticulo);

        //        datos.ejecutarLectura();
        //        if (datos.Lector.Read())
        //        {
        //            return datos.Lector.GetInt32(0) > 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error al comprobar favorito: " + ex.Message);
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }

        //    return false;
        //}

        public bool EliminarFavorito(int userId, int idArticulo)
        {
            bool operacionExitosa = false;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM FAVORITOS WHERE IdUser = @IdUser AND IdArticulo = @IdArticulo");

                datos.setearParametro("@IdUser", userId);
                datos.setearParametro("@IdArticulo", idArticulo);

                datos.ejecutarAccion();

                operacionExitosa = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar favorito: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return operacionExitosa;
        }

        public List<int> ObtenerFavoritos(int userId)
        {
            List<int> favoritos = new List<int>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdArticulo FROM FAVORITOS WHERE IdUser = @IdUser");
                datos.setearParametro("@IdUser", userId);

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    favoritos.Add(datos.Lector.GetInt32(0));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener favoritos: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return favoritos;
        }

        public List<Articulo> ObtenerArticulosPorIds(List<int> idsArticulos)
        {
            List<Articulo> articulos = new List<Articulo>();

            SqlConnection conexion = new SqlConnection();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                foreach (int idArticulo in idsArticulos)
                {
                    datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, M.Descripcion Marca, A.IdCategoria, C.Descripcion Categoria, A.ImagenUrl, A.Precio FROM ARTICULOS A, CATEGORIAS C, MARCAS M WHERE A.IdMarca = M.Id AND A.IdCategoria = C.Id and A.Id = @IdArticulo order by Categoria, Marca");

                    datos.setearParametro("@IdArticulo", idArticulo);
                    datos.ejecutarLectura();

                    while (datos.Lector.Read())
                    {

                        Articulo articulo = new Articulo();

                        articulo.Id = (int)datos.Lector["Id"];
                        articulo.Codigo = (string)datos.Lector["Codigo"];
                        articulo.Nombre = (string)datos.Lector["Nombre"];
                        articulo.Descripcion = (string)datos.Lector["Descripcion"];

                        articulo.IdMarca = new Marca();
                        articulo.IdMarca.Id = (int)datos.Lector["IdMarca"];
                        articulo.IdMarca.Descripcion = (string)datos.Lector["Marca"];

                        articulo.IdCategoria = new Categoria();
                        articulo.IdCategoria.Id = (int)datos.Lector["IdCategoria"];
                        articulo.IdCategoria.Descripcion = (string)datos.Lector["Categoria"];


                        if (!(datos.Lector["ImagenUrl"] is DBNull))
                        {
                            // Obtener la URL de la imagen del lector
                            string imagenUrl = (string)datos.Lector["ImagenUrl"];

                            // Verificar si la URL es null o vacía
                            if (!string.IsNullOrEmpty(imagenUrl))
                            {
                                // Verificar si la URL ya es una URL completa
                                if (imagenUrl.StartsWith("http://") || imagenUrl.StartsWith("https://"))
                                {
                                    // La URL ya es completa, asignarla directamente
                                    articulo.ImagenUrl = imagenUrl;
                                }
                                else
                                {
                                    // La URL es relativa, agregar el prefijo
                                    articulo.ImagenUrl = "~/Images/" + imagenUrl;
                                }
                            }
                            else
                            {
                                // Manejar el caso si la URL es null o vacía
                                articulo.ImagenUrl = "~/Images/img-nd.jpg";
                            }
                        }
                        else
                        {
                            // Manejar el caso si no hay URL de imagen
                            articulo.ImagenUrl = "~/Images/img-nd.jpg";
                        }

                        articulo.Precio = (decimal)datos.Lector["Precio"];
                        articulos.Add(articulo);

                    }
                    datos.cerrarConexion();
                    datos.limpiarParametros();

                }

                return articulos;
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


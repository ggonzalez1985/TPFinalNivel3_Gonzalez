using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Catalogo_Web
{
    public partial class DetalleArticuloEditable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    CategoriaNegocio negocio = new CategoriaNegocio();
                    List<Categoria> lista = negocio.listar();

                    MarcaNegocio marca = new MarcaNegocio();
                    List<Marca> marcas = marca.listar();

                    DdlCategoria.DataSource = lista;
                    DdlCategoria.DataValueField = "Id";
                    DdlCategoria.DataTextField = "Descripcion";
                    DdlCategoria.DataBind();

                    DdlMarca.DataSource = marcas;
                    DdlMarca.DataValueField = "Id";
                    DdlMarca.DataTextField = "Descripcion";
                    DdlMarca.DataBind();
                }

                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";

                if (id != "" && !IsPostBack)
                {
                    ArticulosNegocio negocio = new ArticulosNegocio();
                    Articulo selecccionado = negocio.ListararticuloId(id);

                    if (selecccionado.Id != 0)
                    {

                        Session.Add("articuloSeleccionado", selecccionado);

                        txtId.Text = id;
                        txtCodigo.Text = selecccionado.Codigo;
                        txtNombre.Text = selecccionado.Nombre;
                        txtDescripcion.Text = selecccionado.Descripcion;
                        txtPrecio.Text = selecccionado.Precio.ToString("C2", new CultureInfo("es-AR"));

                        DdlCategoria.SelectedValue = selecccionado.IdCategoria.ToString();
                        DdlMarca.SelectedValue = selecccionado.IdCategoria.ToString();

                        if (!string.IsNullOrEmpty(selecccionado.ImagenUrl))
                        {

                            if (selecccionado.ImagenUrl.StartsWith("http://") || selecccionado.ImagenUrl.StartsWith("https://"))
                            {
                                imgArticulo.ImageUrl = selecccionado.ImagenUrl;
                            }
                            else
                            {
                                imgArticulo.ImageUrl = ResolveUrl("~/Images/" + selecccionado.ImagenUrl);
                            }
                        }
                        else
                        {
                            imgArticulo.ImageUrl = ResolveUrl("~/Images/img-nd.jpg");
                        }

                        if (selecccionado.IdMarca != null)
                        {
                            DdlMarca.SelectedValue = selecccionado.IdMarca.Id.ToString();

                        }
                        if (selecccionado.IdCategoria != null)
                        {
                            DdlCategoria.SelectedValue = selecccionado.IdCategoria.Id.ToString();

                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {

            try
            {

                if (ValidarFormulario(out Articulo articulo))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "successMessage",
                        $"Swal.fire('Éxito', 'Articulo guardado exitosamente!', 'success').then((result) => {{ if (result.isConfirmed) {{ window.location.href = 'Articulos.aspx'; }} }});", true);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }

        }

        private bool ValidarFormulario(out Articulo articulo)
        {
            articulo = new Articulo();
            ArticulosNegocio negocio = new ArticulosNegocio();
            Articulo seleccionado = (Articulo)Session["articuloSeleccionado"];
            bool Guardado = false;
            bool Existente = false;

            if (string.IsNullOrEmpty(txtCodigo.Text.Trim()))
            {
                return false;
            }
            articulo.Codigo = txtCodigo.Text;

            if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
            {
                return false;
            }
            articulo.Nombre = txtNombre.Text;

            if (string.IsNullOrEmpty(txtDescripcion.Text.Trim()))
            {
                return false;
            }
            articulo.Descripcion = txtDescripcion.Text;

            if (string.IsNullOrEmpty(DdlMarca.SelectedValue) || !int.TryParse(DdlMarca.SelectedValue, out int idMarca))
            {
                return false;
            }
            articulo.IdMarca = new Marca { Id = idMarca };

            if (string.IsNullOrEmpty(DdlCategoria.SelectedValue) || !int.TryParse(DdlCategoria.SelectedValue, out int idCategoria))
            {
                return false;
            }
            articulo.IdCategoria = new Categoria { Id = idCategoria };

            if (!string.IsNullOrEmpty(txtPrecio.Text.Trim()))
            {
                // Obtener el valor del campo de texto y eliminar el símbolo de dólar
                string textoPrecio = txtPrecio.Text.Trim().Replace("$", "");

                // Reemplazar el separador de miles (si es punto) y convertir la coma decimal a punto
                string textoFormatoDecimal = textoPrecio.Replace(",", "").Replace(".", ",");

                // Intentar convertir el texto a decimal
                if (decimal.TryParse(textoFormatoDecimal, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal resultado))
                {

                    // Ajustar el valor para obtener 12500,00
                    decimal valorAjustado = resultado / 100;

                    // Asignar el valor convertido a la propiedad Precio
                    articulo.Precio = valorAjustado;

                    // Actualizar el campo de texto con el formato deseado usando la cultura es-AR
                    txtPrecio.Text = resultado.ToString("#,##0.00", new CultureInfo("es-AR")).Replace(".", ",");
                }
                else
                {
                    // Manejar el caso en que la conversión falle
                    return false; // O cualquier otro manejo de errores que necesites
                }
            }
            else
            {
                // Manejar el caso en que el campo esté vacío
                return false; // O cualquier otro manejo de errores que necesites
            }

            if (!string.IsNullOrEmpty(txtId.Text.Trim()))
            {
                articulo.Id = int.Parse(txtId.Text);
                if(articulo.Id != 0)
                Existente = true;
            }

            if (!Existente) 
            {
                if (txtImagen.PostedFile.FileName != "")
                {

                    string ruta = Server.MapPath("./Images/");
                    string imagenTemporal = "articulo-" + 0 + ".jpg";

                    txtImagen.PostedFile.SaveAs(ruta + imagenTemporal);

                    articulo.ImagenUrl = imagenTemporal; //articulo-0.jpg
                    int id = negocio.GuardarConImg(articulo); //aca ya crea el ID

                    string imagenFinal = "articulo-" + id + ".jpg";
                    txtImagen.PostedFile.SaveAs(ruta + imagenFinal);
                    negocio.ActualizarImagenArticulo(id, imagenFinal);

                    Guardado = true;
                }
                else
                {
                    string imageUrl = imgArticulo.ImageUrl;

                    if (imageUrl.StartsWith("/Images/"))
                    {
                        imageUrl = imageUrl.Replace("/Images/", "");
                    }
                    articulo.ImagenUrl = imageUrl;
                }

                if (Guardado == false)
                {
                    negocio.Guardar(articulo);
                }

            }
            else //else por el lado de si existe el articulo y solo asignarle la imagen.
            {
                string defaultImageUrl = "https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg";

                // Verifica si se subió un nuevo archivo de imagen
                if (txtImagen.PostedFile != null && txtImagen.PostedFile.ContentLength > 0)
                {
                    string ruta = Server.MapPath("./Images/");
                    string imagenFinal = "articulo-" + articulo.Id + ".jpg";
                    string rutaArchivoExistente = Path.Combine(ruta, imagenFinal);

                    // Si existe una imagen con el mismo nombre, elimínala
                    if (File.Exists(rutaArchivoExistente))
                    {
                        File.Delete(rutaArchivoExistente);
                    }

                    // Guarda la nueva imagen
                    txtImagen.PostedFile.SaveAs(rutaArchivoExistente);

                    // Actualiza la URL de la imagen del artículo
                    articulo.ImagenUrl = imagenFinal;
                }
                else
                {
                    // Si no se subió una nueva imagen
                    if (imgArticulo.ImageUrl != defaultImageUrl)
                    {
                        // Mantén la URL de la imagen actual del artículo
                        //articulo.ImagenUrl = imgArticulo.ImageUrl;
                    }
                    else
                    {
                        // Si la imagen actual es la imagen por defecto, asigna un valor vacío o una imagen por defecto
                        articulo.ImagenUrl = "";
                    }
                }

                negocio.EditarArticulo(articulo);
            }

            return true;

        }



        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                //EliminarArticulo();
                ArticulosNegocio negocio = new ArticulosNegocio();
                bool resultado;
                resultado = negocio.Eliminar(int.Parse(txtId.Text));

                if (resultado)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "successMessage",
                    "Swal.fire({ title: 'Eliminado', text: 'El artículo ha sido eliminado.', icon: 'success', confirmButtonText: 'OK' }).then((result) => { if (result.isConfirmed) { window.location.href = 'articulos.aspx'; } });", true);
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorMessage",
                    $"Swal.fire('Error', '{ex.Message}', 'error');", true);
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Articulos.aspx");
        }
    }
}
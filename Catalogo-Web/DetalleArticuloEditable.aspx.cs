using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                        // Verificar si la URL ya es una URL completa
                        if (selecccionado.ImagenUrl.StartsWith("http://") || selecccionado.ImagenUrl.StartsWith("https://"))
                        {
                            // La URL ya es una URL completa, no se necesita modificarla
                            imgArticulo.ImageUrl = selecccionado.ImagenUrl;
                        }
                        else
                        {
                            // La URL es relativa, agregar el prefijo
                            imgArticulo.ImageUrl = ResolveUrl("~/Images/" + selecccionado.ImagenUrl);
                        }
                    }
                    else
                    {
                        // Imagen por defecto si no hay URL
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

                ArticulosNegocio negocio = new ArticulosNegocio();
                bool resultado = false;

                if (ValidarFormulario(out Articulo articulo))
                {
                    if (Request.QueryString["id"] != null)
                    {
                        resultado = negocio.EditarArticulo(articulo);
                    }

                    string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";

                    if (id != "" && resultado)
                    {

                        // Utiliza ScriptManager para registrar el script de SweetAlert2
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "successMessage",
                            $"Swal.fire('Éxito', 'El articulo se ha modificado exitosamente.', 'success').then((result) => {{ if (result.isConfirmed) {{ window.location.href = 'DetalleArticuloEditable.aspx?id={id}'; }} }});", true);
                    }
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
            Articulo seleccionado = (Articulo)Session["articuloSeleccionado"];

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

            articulo.ImagenUrl = imgArticulo.ImageUrl;

            if (txtImagen.PostedFile.FileName != "")
            {
                string ruta = Server.MapPath("./Images/");
                txtImagen.PostedFile.SaveAs(ruta + "articulo-" + seleccionado.Id + ".jpg");
                articulo.ImagenUrl = "articulo-" + seleccionado.Id + ".jpg";
            }


            if (txtImagen.PostedFile.FileName == "")
            {
                string imageUrl = imgArticulo.ImageUrl;

                // Eliminar "/Images/" de la URL
                if (imageUrl.StartsWith("/Images/"))
                {
                    imageUrl = imageUrl.Replace("/Images/", "");
                }

                // Asignar la URL de la imagen a la propiedad ImagenUrl del objeto articulo
                articulo.ImagenUrl = imageUrl;
            }

            if (!string.IsNullOrEmpty(txtPrecio.Text.Trim()))
            {
                string soloNumeros = Regex.Replace(txtPrecio.Text, @"[^\d,]", "");
                soloNumeros = soloNumeros.Replace(',', '.');

                if (decimal.TryParse(soloNumeros, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal resultado))
                {
                    articulo.Precio = resultado;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            articulo.Id = int.Parse(txtId.Text);

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
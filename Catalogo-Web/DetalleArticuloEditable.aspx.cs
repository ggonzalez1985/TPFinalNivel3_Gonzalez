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
                string soloNumeros = Regex.Replace(txtPrecio.Text, @"[^\d]", "");

                if (decimal.TryParse(soloNumeros, NumberStyles.None, CultureInfo.InvariantCulture, out decimal resultado))
                {
                    articulo.Precio = resultado;
                }
                else
                {
                    return false; // Manejar el caso en que la conversión falle
                }
            }
            else
            {
                return false; // Manejar el caso en que el campo esté vacío
            }

            if (!string.IsNullOrEmpty(txtId.Text.Trim()))
            {
                articulo.Id = int.Parse(txtId.Text);
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
                //if (txtImagen.PostedFile.FileName != "") //cuando se edita, te borra. pero la imagen esta si
                string defaultImageUrl = "https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg";
                if (imgArticulo.ImageUrl != defaultImageUrl)
                {
                    string ruta = Server.MapPath("./Images/");
                    string imagenFinal = "articulo-" + articulo.Id + ".jpg";

                    //txtImagen.PostedFile.SaveAs(ruta + imagenFinal);    //copia una imagen que no esta. tiene que copiar del otro!!!!

                    articulo.ImagenUrl = imagenFinal;

                    txtImagen.PostedFile.SaveAs(ruta + imagenFinal);
                    //negocio.ActualizarImagenArticulo(articulo.Id, imagenFinal); //no actualiza todos los demas campos.
                }
                else
                {
                    articulo.ImagenUrl = "";
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
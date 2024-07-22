using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Catalogo_Web
{
    public partial class DetalleArticulo : System.Web.UI.Page
    {
        public Articulo seleccionado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
                if (id != "" && !IsPostBack)
                {
                    ArticulosNegocio negocio = new ArticulosNegocio();

                    // Comprobar si el usuario está logueado
                    if (Session["Usuario"] != null)
                    {
                        seleccionado = negocio.ListararticuloId(id);
                        Session["articuloSeleccionado"] = seleccionado;

                        int userId = Convert.ToInt32(((Usuario)Session["Usuario"]).Id);

                        // Obtener la lista de favoritos del usuario desde la base de datos
                        List<int> favoritos = negocio.ObtenerFavoritos(userId);
                        Session["Favoritos"] = favoritos;

                        // Comprobar si el artículo ya es favorito y actualizar el botón
                        bool esFavorito = favoritos.Contains(seleccionado.Id);
                        btnAgregarFavoritos.Text = esFavorito ? "❤️" : "♡";

                    }
                    else
                    {
                        btnAgregarFavoritos.Text = "♡";
                    }
                        // Usuario no logueado, no se actualiza el botón de favoritos
                        seleccionado = negocio.ListararticuloId(id);
                        Session["articuloSeleccionado"] = seleccionado;
                        

                        if (!string.IsNullOrEmpty(seleccionado.ImagenUrl))
                        {
                            // Verificar si la URL ya es una URL completa
                            if (seleccionado.ImagenUrl.StartsWith("http://") || seleccionado.ImagenUrl.StartsWith("https://"))
                            {
                                // La URL ya es una URL completa, no se necesita modificarla
                                seleccionado.ImagenUrl = seleccionado.ImagenUrl;
                            }
                            else
                            {
                                // La URL es relativa, agregar el prefijo
                                seleccionado.ImagenUrl = ResolveUrl("~/Images/" + seleccionado.ImagenUrl);
                            }
                        }
                        else
                        {
                            // Imagen por defecto si no hay URL
                            seleccionado.ImagenUrl = ResolveUrl("~/Images/img-nd.jpg");
                        }



                    
                }
            }
            catch (Exception ex)
            {
                Session["error"] = ex;
                Response.Redirect("Error.aspx");
            }

        }

        protected void btnAgregarFavoritos_Click(object sender, EventArgs e)
        {

            try
            {
                if (!Seguridad.sesionActiva(Session["Usuario"]))
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "warningMessage",
    "Swal.fire({ title: 'Advertencia', text: 'Debes ingresar a tu cuenta o registrarte para agregar a favoritos.', icon: 'warning', showCancelButton: false, confirmButtonColor: '#3085d6', confirmButtonText: 'OK' }).then((result) => { if (result.isConfirmed) { window.location.href = 'Login.aspx'; } });", true);


                }
                else
                {
                    int userId = Convert.ToInt32(((Usuario)Session["Usuario"]).Id);
                    int idArticulo = ((Articulo)Session["articuloSeleccionado"]).Id;

                    ArticulosNegocio negocio = new ArticulosNegocio();

                    // Verificar si el artículo ya está en favoritos
                    List<int> favoritos = Session["Favoritos"] as List<int>;
                    if (favoritos == null)
                    {
                        favoritos = new List<int>();
                    }

                    bool esFavorito = favoritos.Contains(idArticulo);

                    bool operacionExitosa;
                    if (esFavorito)
                    {
                        // Eliminar de favoritos
                        operacionExitosa = negocio.EliminarFavorito(userId, idArticulo);
                        if (operacionExitosa)
                        {
                            favoritos.Remove(idArticulo);
                            btnAgregarFavoritos.Text = "♡";
                        }
                    }
                    else
                    {
                        // Agregar a favoritos
                        operacionExitosa = negocio.ToggleFavorito(userId, idArticulo);
                        if (operacionExitosa)
                        {
                            favoritos.Add(idArticulo);
                            btnAgregarFavoritos.Text = "❤️";
                        }
                    }

                    // Guardar la lista de favoritos de nuevo en la sesión
                    Session["Favoritos"] = favoritos;
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                throw;
            }

        }
    }
}
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

                    if (Session["Usuario"] != null)
                    {
                        seleccionado = negocio.ListararticuloId(id);
                        Session["articuloSeleccionado"] = seleccionado;

                        int userId = Convert.ToInt32(((Usuario)Session["Usuario"]).Id);

                        List<int> favoritos = negocio.ObtenerFavoritos(userId);
                        Session["Favoritos"] = favoritos;

                        bool esFavorito = favoritos.Contains(seleccionado.Id);
                        btnAgregarFavoritos.Text = esFavorito ? "❤️" : "♡";
                    }
                    else
                    {
                        btnAgregarFavoritos.Text = "♡";
                    }
                        seleccionado = negocio.ListararticuloId(id);
                        Session["articuloSeleccionado"] = seleccionado;
                        
                        if (!string.IsNullOrEmpty(seleccionado.ImagenUrl)) 
                        {
                            if (seleccionado.ImagenUrl.StartsWith("http://") || seleccionado.ImagenUrl.StartsWith("https://"))
                            {
                                seleccionado.ImagenUrl = seleccionado.ImagenUrl;
                            }
                            else
                            {
                                seleccionado.ImagenUrl = ResolveUrl("~/Images/" + seleccionado.ImagenUrl);
                            }
                        }
                        else
                        {
                            seleccionado.ImagenUrl = ResolveUrl("~/Images/img-nd.jpg");
                        }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
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

                    List<int> favoritos = Session["Favoritos"] as List<int>;
                    if (favoritos == null)
                    {
                        favoritos = new List<int>();
                    }

                    bool esFavorito = favoritos.Contains(idArticulo);

                    bool operacionExitosa;
                    if (esFavorito)
                    {
                        operacionExitosa = negocio.EliminarFavorito(userId, idArticulo);
                        if (operacionExitosa)
                        {
                            favoritos.Remove(idArticulo);
                            btnAgregarFavoritos.Text = "♡";
                        }
                    }
                    else
                    {
                        // Agrega a favoritos
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
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }

        }
    }
}
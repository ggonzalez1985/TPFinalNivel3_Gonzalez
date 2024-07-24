using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Catalogo_Web
{
    public partial class Catalogo : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulo { get; set; }
        public List<Articulo> ListaFavoritos { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack)
                {
                    ArticulosNegocio negocio = new ArticulosNegocio();
                    ListaArticulo = negocio.Listararticulos();

                    if (ListaArticulo != null)
                    {
                        Session["listaArticulos"] = ListaArticulo;
                        reiniciaControles();
                    }
                    else
                    {
                        Session.Add("error", new Exception("No se encontraron artículos."));
                        Response.Redirect("Error.aspx");
                    }

                    if (Session["Usuario"] != null)
                    {
                        int userId = Convert.ToInt32(((Usuario)Session["Usuario"]).Id);

                        // Obtener la lista de IDs de artículos favoritos del usuario desde la base de datos
                        List<int> idsArticulosFavoritos = negocio.ObtenerFavoritos(userId);

                        // Obtener los artículos correspondientes a los IDs
                        ListaFavoritos = negocio.ObtenerArticulosPorIds(idsArticulosFavoritos);

                    }

                }
                else
                {

                    ListaArticulo = Session["listaArticulos"] as List<Articulo>;

                    if (ListaArticulo == null)
                    {
                        Session.Add("error", new Exception("No se encontraron artículos en la sesión."));
                        Response.Redirect("Error.aspx");
                    }
                }

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx");
            }
        }

        protected void DdlCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtFiltro.Text.ToUpper();

            if (string.IsNullOrWhiteSpace(filtro))
            {
                ListaArticulo = new List<Articulo>(ListaArticulo);
            }
            else
            {
                List<Articulo> ListaFiltrada = ListaArticulo.FindAll(x => x.Nombre.ToUpper().Contains(filtro));
                ListaArticulo = ListaFiltrada.Count == 0 ? null : ListaFiltrada;

                // Mostrar mensaje si no se encuentran artículos
                if (ListaArticulo == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "noResultsAlert",
                    "Swal.fire({ title: 'Información', text: 'No se encontraron artículos.', icon: 'info', confirmButtonText: 'OK' }).then((result) => { if (result.isConfirmed) { window.location.href = 'Catalogo.aspx'; } });", true);
                }

                lblResultados.Text = ListaArticulo == null ? "-" : ListaArticulo.Count.ToString();
                lblRegistros.Text = txtFiltro.Text;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ListaArticulo = new List<Articulo>(ListaArticulo);
            reiniciaControles();
        }

        private void reiniciaControles()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            List<Categoria> listaCategorias = categoriaNegocio.listar();
            Session["listaCategorias"] = listaCategorias;

            MarcaNegocio marcaNegocio = new MarcaNegocio();
            List<Marca> listaMarcas = marcaNegocio.listar();
            Session["listaMarcas"] = listaMarcas;

            DdlCategorias.DataSource = listaCategorias;
            DdlCategorias.DataValueField = "Id";
            DdlCategorias.DataTextField = "Descripcion";
            DdlCategorias.DataBind();
            DdlCategorias.Items.Insert(0, new ListItem("", ""));

            DdlMarcas.DataSource = listaMarcas;
            DdlMarcas.DataValueField = "Id";
            DdlMarcas.DataTextField = "Descripcion";
            DdlMarcas.DataBind();
            DdlMarcas.Items.Insert(0, new ListItem("", ""));

            txtFiltro.Text = "";
            txtPrecioMax.Text = "";
            txtPrecioMin.Text = "";

            lblResultados.Text = ListaArticulo.Count.ToString();
            lblRegistros.Text = "-";

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ArticulosNegocio negocio = new ArticulosNegocio();
            string CategoriaArticulo = null, MarcaArticulo = null, PrecioMin = null, PrecioMax = null;

            try
            {
                if (!validarFiltro())
                    return;

                if (DdlCategorias.SelectedItem != null && DdlCategorias.SelectedItem.ToString() != "")
                {
                    CategoriaArticulo = DdlCategorias.SelectedItem.ToString();
                }

                if (DdlMarcas.SelectedItem != null && DdlMarcas.SelectedItem.ToString() != "")
                {
                    MarcaArticulo = DdlMarcas.SelectedItem.ToString();
                }

                if (txtPrecioMin.Text != "")
                {
                    PrecioMin = txtPrecioMin.Text;
                }

                if (txtPrecioMax.Text != "")
                {
                    PrecioMax = txtPrecioMax.Text;
                }

                lblResultados.Text = negocio.filtrar(CategoriaArticulo, MarcaArticulo, PrecioMin, PrecioMax).Count.ToString();

                if (negocio.filtrar(CategoriaArticulo, MarcaArticulo, PrecioMin, PrecioMax).Count > 0)
                {
                    ListaArticulo = negocio.filtrar(CategoriaArticulo, MarcaArticulo, PrecioMin, PrecioMax);
                    lblResultados.Text = ListaArticulo.Count == 0 ? "-" : ListaArticulo.Count.ToString();

                    if (DdlCategorias.SelectedItem.ToString() != "")
                    {
                        lblRegistros.Text = DdlCategorias.SelectedItem.ToString();
                    }
                    else
                    {
                        if (DdlMarcas.SelectedItem.ToString() != "")
                        {
                            lblRegistros.Text = DdlMarcas.SelectedItem.ToString();
                        }
                    }

                }
                else
                {
                    ListaArticulo = null;
                }


            }
            catch (Exception)
            {

                throw;
            }


        }

        private bool validarFiltro()
        {
            if (!string.IsNullOrEmpty(txtPrecioMin.Text) || !string.IsNullOrEmpty(txtPrecioMax.Text) || DdlCategorias.SelectedItem != null || DdlMarcas.SelectedItem != null)
            {
                return true;
            }
            else
            {
                string script = "alert('Establecer al menos un parametro para realizar el filtrado!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", script, true);

                return false;
            }
        }

        protected void chkAvanzado_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
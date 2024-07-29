using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Catalogo_Web
{
    public partial class Categorias : System.Web.UI.Page
    {
        public List<Categoria> ListaCategorias { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    CategoriaNegocio negocio = new CategoriaNegocio();

                    ListaCategorias = negocio.listar();

                    if (ListaCategorias != null)
                    {
                        Session["listaCategorias"] = ListaCategorias;

                        dgvCategorias.DataSource = ListaCategorias;
                        dgvCategorias.DataBind();

                        reiniciaControles();
                    }
                    else
                    {
                        Session.Add("error", new Exception("No se encontraron categorias."));
                        Response.Redirect("Error.aspx");
                    }

                }
                else
                {

                    ListaCategorias = Session["listaCategorias"] as List<Categoria>;

                    if (ListaCategorias == null)
                    {
                        Session.Add("error", new Exception("No se encontraron categorias en la sesión."));
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

        private void reiniciaControles()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            List<Categoria> listaCategorias = categoriaNegocio.listar();
            Session["listaCategorias"] = listaCategorias;

            MarcaNegocio marcaNegocio = new MarcaNegocio();
            List<Marca> listaMarcas = marcaNegocio.listar();
            Session["listaMarcas"] = listaMarcas;

            txtFiltro.Text = "";

            lblResultados.Text = listaCategorias.Count.ToString();
            lblRegistros.Text = "-";

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("DetalleArticuloEditable.aspx");
        }

        protected void lnkEditar_Click2(object sender, EventArgs e)
        {
            LinkButton lnkButton = sender as LinkButton;
            if (lnkButton != null)
            {
                string id = lnkButton.CommandArgument;
                Response.Redirect("DetalleArticuloEditable.aspx?id=" + id);
            }
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtFiltro.Text.ToUpper();

            if (string.IsNullOrWhiteSpace(filtro))
            {
                ListaCategorias = new List<Categoria>(ListaCategorias);
            }
            else
            {
                List<Categoria> ListaFiltrada = ListaCategorias.FindAll(x => x.Descripcion.ToUpper().Contains(filtro));
                ListaCategorias = ListaFiltrada.Count == 0 ? null : ListaFiltrada;

                if (ListaCategorias == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "noResultsAlert",
                    "Swal.fire({ title: 'Información', text: 'No se encontraron categorias.', icon: 'info', confirmButtonText: 'OK' }).then((result) => { if (result.isConfirmed) { window.location.href = 'Categorias.aspx'; } });", true);
                }

                lblResultados.Text = ListaCategorias == null ? "-" : ListaCategorias.Count.ToString();
                lblRegistros.Text = txtFiltro.Text;

                dgvCategorias.DataSource = ListaCategorias;
                dgvCategorias.DataBind();

            }
        }

        protected void btnReset_Click1(object sender, EventArgs e)
        {
            ListaCategorias = new List<Categoria>(ListaCategorias);
            dgvCategorias.DataSource = ListaCategorias;
            dgvCategorias.DataBind();
            reiniciaControles();
        }
    }
}
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
            //if (IsPostBack)
            //{
            //    if (DdlCategorias.SelectedItem.Value != "")
            //    {
            //        int id = int.Parse(DdlCategorias.SelectedItem.Value); //aca cobntrolar que si se eligio vacio que no explote

            //        if (id > 0)
            //        {
            //            List<Articulo> filteredArticulos = ((List<Articulo>)Session["listaArticulos"]).FindAll(x => x.IdCategoria.Id == id);

            //            List<Marca> listaMarcas = new List<Marca>();

            //            foreach (Articulo articulo in filteredArticulos)
            //            {
            //                listaMarcas.Add(articulo.IdMarca);
            //            }

            //            DdlMarcas.DataSource = listaMarcas;
            //            DdlMarcas.DataBind();
            //            DdlMarcas.Items.Insert(0, new ListItem("", ""));
            //        }
            //    }
            //    else
            //    {
            //        DdlMarcas.Items.Clear();
            //        MarcaNegocio marcaNegocio = new MarcaNegocio();
            //        DdlMarcas.DataSource = marcaNegocio.listar();
            //        DdlMarcas.DataBind();
            //        DdlMarcas.Items.Insert(0, new ListItem("", ""));
            //    }
            //}
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
                ListaArticulo = ListaArticulo.FindAll(x => x.Nombre.ToUpper().Contains(filtro));

                lblMostrando.Visible = true;
                lblResultados.Visible = true;
                lblResultados.Text = ListaArticulo.Count == 0 ? "-" : ListaArticulo.Count.ToString();

                lblResultadoPara.Visible = true;
                lblRegistros.Visible = true;
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

            lblResultados.Visible = false;
            lblRegistros.Visible = false;
            lblMostrando.Visible = false;
            lblResultadoPara.Visible = false;

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
                    lblMostrando.Visible = true;
                    lblResultados.Visible = true;
                    lblResultados.Text = ListaArticulo.Count == 0 ? "-" : ListaArticulo.Count.ToString();

                    lblResultadoPara.Visible = true;
                    lblRegistros.Visible = true;
                    

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
                    lblMostrando.Visible = false; lblResultados.Visible = false;
                    lblResultadoPara.Visible = false; lblRegistros.Visible = false; 
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

                //MessageBox.Show("Establecer al menos un parametro para realizar el filtrado!", "Filtrar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        protected void chkAvanzado_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
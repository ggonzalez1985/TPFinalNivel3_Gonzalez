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
    public partial class Articulos : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulo { get; set; }    
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    // Crear instancia del negocio
                    ArticulosNegocio negocio = new ArticulosNegocio();

                    // Obtener la lista de artículos
                    ListaArticulo = negocio.Listararticulos();

                    // Verificar si se obtuvieron artículos
                    if (ListaArticulo != null)
                    {
                        // Almacenar la lista de artículos en la sesión
                        Session["listaArticulos"] = ListaArticulo;

                        // Asignar la lista al DataSource del GridView y enlazar datos
                        dgvArticulos.DataSource = ListaArticulo;
                        dgvArticulos.DataBind();

                        // Reiniciar controles
                        reiniciaControles();
                    }
                    else
                    {
                        // Si no se encontraron artículos, redirigir a la página de error
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
                    "Swal.fire({ title: 'Información', text: 'No se encontraron artículos.', icon: 'info', confirmButtonText: 'OK' }).then((result) => { if (result.isConfirmed) { window.location.href = 'Articulos.aspx'; } });", true);
                }

                lblResultados.Text = ListaArticulo == null ? "-" : ListaArticulo.Count.ToString();
                lblRegistros.Text = txtFiltro.Text;

                dgvArticulos.DataSource = ListaArticulo;
                dgvArticulos.DataBind();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ListaArticulo = new List<Articulo>(ListaArticulo);
            dgvArticulos.DataSource = ListaArticulo;
            dgvArticulos.DataBind();
            reiniciaControles();
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

                    dgvArticulos.DataSource = ListaArticulo;
                    dgvArticulos.DataBind();

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

        //protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string id = dgvArticulos.SelectedDataKey.Value.ToString();
        //    Response.Redirect("DetalleArticuloEditable.aspx?id=" + id);
        //}

        protected void chkAvanzado_CheckedChanged(object sender, EventArgs e)
        {

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
 

        protected void lnkEliminar_Click1(object sender, EventArgs e)
        {
            int b = 2;
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            string id = hfDeleteId.Value;
            if (!string.IsNullOrEmpty(id))
            {
                // Lógica para eliminar el artículo con el ID obtenido
                EliminarArticulo(id);
            }
        }

        private void EliminarArticulo(string id)
        {
            // Implementa la lógica para eliminar el artículo de la base de datos aquí
            // Ejemplo:
            // using (SqlConnection conn = new SqlConnection("your_connection_string"))
            // {
            //     SqlCommand cmd = new SqlCommand("DELETE FROM Articulos WHERE Id = @Id", conn);
            //     cmd.Parameters.AddWithValue("@Id", id);
            //     conn.Open();
            //     cmd.ExecuteNonQuery();
            // }
        }

    }

}
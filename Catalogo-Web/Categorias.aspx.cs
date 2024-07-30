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
                    if (!Seguridad.esAdmin(Session["Usuario"]))
                    {
                        Session["error"] = "Se requiere permisos de admin para acceder a esta pantalla";
                        string script = @"<script type='text/javascript'>
                        Swal.fire({
                            title: 'Acceso Denegado',
                            text: 'Se requiere permisos de admin para acceder a esta pantalla.',
                            icon: 'error',
                            confirmButtonText: 'OK',
                            allowOutsideClick: false
                        }).then((result) => {
                            if (result.isConfirmed) {
                                window.location = 'Catalogo.aspx';
                            }
                        });
                      </script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "swalError", script);
                    }

                    CategoriaNegocio negocio = new CategoriaNegocio();

                    ListaCategorias = negocio.listar();

                    if (ListaCategorias != null)
                    {
                        Session["listaCategorias"] = ListaCategorias;

                        dgvCategoriass.DataSource = ListaCategorias;
                        dgvCategoriass.DataBind();

                        reiniciaControles();
                    }
                    else
                    {
                        Session.Add("error", "No se encontraron categorias.");
                        Response.Redirect("Error.aspx", false);
                    }

                }
                else
                {

                    ListaCategorias = Session["listaCategorias"] as List<Categoria>;
                    reiniciaControles();

                    if (ListaCategorias == null)
                    {
                        Session.Add("error", "No se encontraron categorias en la sesión.");
                        Response.Redirect("Error.aspx", false);
                    }

                }

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }
        }

        private void reiniciaControles()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            List<Categoria> listaCategorias = categoriaNegocio.listar();
            Session["listaCategorias"] = listaCategorias;

            lblResultados.Text = listaCategorias.Count.ToString();
            lblRegistros.Text = "-";

        }


        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string filtro = "a"; //txtFiltro.Text.ToUpper();

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

                    dgvCategoriass.DataSource = ListaCategorias;
                    dgvCategoriass.DataBind();

                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnReset_Click1(object sender, EventArgs e)
        {
            ListaCategorias = new List<Categoria>(ListaCategorias);
            dgvCategoriass.DataSource = ListaCategorias;
            dgvCategoriass.DataBind();
            reiniciaControles();
        }

        protected void lnkNuevoArticulo_Click(object sender, EventArgs e)
        {
            txtDescripcion.Enabled = true;
            lnkGuardar.Enabled = true;
            lnkCancelar.Enabled = true;
            txtDescripcion.Text = string.Empty;
        }

        protected void lnkGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string descripcion = txtDescripcion.Text;

                if (string.IsNullOrEmpty(descripcion))
                {
                    string script = "Swal.fire({ icon: 'error', title: 'Error', text: 'El campo Descripción es obligatorio.' });";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showError", script, true);
                }
                else
                {
                    CategoriaNegocio negocio = new CategoriaNegocio();
                    bool resultado = negocio.Agregar(descripcion);

                    if (resultado)
                    {
                        string script = "Swal.fire({ icon: 'success', title: 'Éxito', text: 'Categoría guardada con éxito.' });";
                        ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", script, true);

                        ListaCategorias = negocio.listar();
                        dgvCategoriass.DataSource = ListaCategorias;
                        dgvCategoriass.DataBind();

                        reiniciaControles();

                        txtDescripcion.Text = string.Empty;
                        txtDescripcion.Enabled = false;
                        lnkGuardar.Enabled = false;
                    }
                    else
                    {

                        string script = "Swal.fire({ icon: 'error', title: 'Error', text: 'Hubo un problema al guardar la categoría.' });";
                        ScriptManager.RegisterStartupScript(this, GetType(), "showError", script, true);
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void dgvCategoriass_RowEditing(object sender, GridViewEditEventArgs e)
        {

            try
            {
                CategoriaNegocio negocio = new CategoriaNegocio();

                dgvCategoriass.EditIndex = e.NewEditIndex;
                ListaCategorias = negocio.listar();
                dgvCategoriass.DataSource = ListaCategorias;
                dgvCategoriass.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void dgvCategoriass_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Cargar();
        }

        private void Cargar()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            dgvCategoriass.EditIndex = -1;
            CargaDatos(negocio);
        }

        private void CargaDatos(CategoriaNegocio negocio)
        {
            ListaCategorias = negocio.listar();
            dgvCategoriass.DataSource = ListaCategorias;
            dgvCategoriass.DataBind();
        }

        protected void dgvCategoriass_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                CategoriaNegocio negocio = new CategoriaNegocio();
                Categoria categoria = new Categoria();

                int id = Convert.ToInt32(dgvCategoriass.DataKeys[e.RowIndex].Value);
                string descripcionNueva = (dgvCategoriass.Rows[e.RowIndex].FindControl("txtDescripcion") as TextBox).Text;

                categoria.Id = id;
                categoria.Descripcion = descripcionNueva;

                bool resultado = negocio.modificar(categoria);

                if (resultado)
                {
                    string script = "Swal.fire({ icon: 'success', title: 'Éxito', text: 'Categoría editada con éxito.' });";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", script, true);
                    Cargar();
                }
                else
                {
                    string script = "Swal.fire({ icon: 'error', title: 'Error', text: 'Hubo un problema al guardar la categoría.' });";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showError", script, true);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void dgvCategoriass_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            try
            {
                CategoriaNegocio negocio = new CategoriaNegocio();
                int id = Convert.ToInt32(dgvCategoriass.DataKeys[e.RowIndex].Value);
                bool bandera = negocio.eliminar(id);
                CargaDatos(negocio);

                if (bandera)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "deleteMessage",
                    $"Swal.fire('Eliminado!', 'Se eliminó la categoria con Id {id}', 'success').then((result) => {{ if (result.isConfirmed) {{ window.location.href = 'Categorias.aspx'; }} }});", true);

                }

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void lnkCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                txtDescripcion.Text = "";
                txtDescripcion.Enabled = false;
                lnkCancelar.Enabled = false;
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }  
        }
    }
}
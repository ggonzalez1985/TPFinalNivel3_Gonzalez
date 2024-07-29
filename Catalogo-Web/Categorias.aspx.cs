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

                        dgvCategoriass.DataSource = ListaCategorias;
                        dgvCategoriass.DataBind();

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
                    reiniciaControles() ;

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

            lblResultados.Text = listaCategorias.Count.ToString();
            lblRegistros.Text = "-";

        }


        protected void txtFiltro_TextChanged(object sender, EventArgs e)
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

        protected void btnReset_Click1(object sender, EventArgs e)
        {
            ListaCategorias = new List<Categoria>(ListaCategorias);
            dgvCategoriass.DataSource = ListaCategorias;
            dgvCategoriass.DataBind();
            reiniciaControles();
        }

        protected void lnkNuevoArticulo_Click(object sender, EventArgs e)
        {
            // Habilitar el TextBox y el LinkButton para ingresar y guardar la nueva descripción
            txtDescripcion.Enabled = true;
            lnkGuardar.Enabled = true;

            // Limpiar el contenido del TextBox
            txtDescripcion.Text = string.Empty;
        }

        protected void lnkGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el valor del campo de texto
                string descripcion = txtDescripcion.Text;

                // Verificar si el campo está vacío
                if (string.IsNullOrEmpty(descripcion))
                {
                    // Mostrar un mensaje de error con SweetAlert
                    string script = "Swal.fire({ icon: 'error', title: 'Error', text: 'El campo Descripción es obligatorio.' });";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showError", script, true);
                }
                else
                {
                    // Proceder con la lógica de guardado
                    CategoriaNegocio negocio = new CategoriaNegocio();
                    bool resultado = negocio.Agregar(descripcion);

                    if (resultado)
                    {
                        // Mostrar un mensaje de éxito con SweetAlert
                        string script = "Swal.fire({ icon: 'success', title: 'Éxito', text: 'Categoría guardada con éxito.' });";
                        ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", script, true);

                        // Recargar los datos y actualizar el GridView
                        ListaCategorias = negocio.listar();
                        dgvCategoriass.DataSource = ListaCategorias;
                        dgvCategoriass.DataBind();

                        reiniciaControles();

                        // Limpiar y deshabilitar los controles después de guardar
                        txtDescripcion.Text = string.Empty;
                        txtDescripcion.Enabled = false;
                        lnkGuardar.Enabled = false;
                    }
                    else
                    {
                        // Mostrar un mensaje de error en caso de fallo en el guardado
                        string script = "Swal.fire({ icon: 'error', title: 'Error', text: 'Hubo un problema al guardar la categoría.' });";
                        ScriptManager.RegisterStartupScript(this, GetType(), "showError", script, true);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Session.Add("error", ex);
                Response.Redirect("Error.aspx");
            }
        }

        protected void dgvCategoriass_RowEditing(object sender, GridViewEditEventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();

            dgvCategoriass.EditIndex = e.NewEditIndex;

            ListaCategorias = negocio.listar();
            dgvCategoriass.DataSource = ListaCategorias;
            dgvCategoriass.DataBind();
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

                    // Mostrar un mensaje de éxito con SweetAlert
                    string script = "Swal.fire({ icon: 'success', title: 'Éxito', text: 'Categoría editada con éxito.' });";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", script, true);
                    Cargar();

                }
                else
                {
                    // Mostrar un mensaje de error en caso de fallo en el guardado
                    string script = "Swal.fire({ icon: 'error', title: 'Error', text: 'Hubo un problema al guardar la categoría.' });";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showError", script, true);
                }

               
            }
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {

                throw;
            }

        }



    }
}
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
    public partial class Marcas : System.Web.UI.Page
    {
        public List<Marca> ListaMarcas { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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

                if (!IsPostBack)
                {

                    MarcaNegocio negocio = new MarcaNegocio();

                    ListaMarcas = negocio.listar();

                    if (ListaMarcas != null)
                    {
                        Session["ListaMarcas"] = ListaMarcas;

                        dgvMarcass.DataSource = ListaMarcas;
                        dgvMarcass.DataBind();

                        reiniciaControles();
                    }
                    else
                    {
                        Session.Add("error", new Exception("No se encontraron marcas."));
                        Response.Redirect("Error.aspx");
                    }

                }
                else
                {

                    ListaMarcas = Session["ListaMarcas"] as List<Marca>;
                    reiniciaControles();

                    if (ListaMarcas == null)
                    {
                        Session.Add("error", new Exception("No se encontraron marcas en la sesión."));
                        Response.Redirect("Error.aspx");
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
            MarcaNegocio Negocio = new MarcaNegocio();
            List<Marca> ListaMarcas = Negocio.listar();
            Session["ListaMarcas"] = ListaMarcas;

            lblResultados.Text = ListaMarcas.Count.ToString();
            lblRegistros.Text = "-";
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
                    MarcaNegocio negocio = new MarcaNegocio();
                    Marca marca = new Marca();
                    marca.Descripcion = descripcion;

                    bool existe = negocio.ExisteDescripcion(descripcion);

                    if (!existe)
                    {
                        bool resultado = negocio.AgregarMarcaSP(marca);

                        if (resultado)
                        {
                            // Mostrar un mensaje de éxito con SweetAlert
                            string script = "Swal.fire({ icon: 'success', title: 'Éxito', text: 'Marca guardada con éxito.' });";
                            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", script, true);

                            // Recargar los datos y actualizar el GridView
                            ListaMarcas = negocio.listar();
                            dgvMarcass.DataSource = ListaMarcas;
                            dgvMarcass.DataBind();

                            reiniciaControles();

                            // Limpiar y deshabilitar los controles después de guardar
                            txtDescripcion.Text = string.Empty;
                            txtDescripcion.Enabled = false;
                            lnkGuardar.Enabled = false;
                        }
                        else
                        {
                            // Mostrar un mensaje de error en caso de fallo en el guardado
                            string script = "Swal.fire({ icon: 'error', title: 'Error', text: 'Hubo un problema al guardar la marca.' });";
                            ScriptManager.RegisterStartupScript(this, GetType(), "showError", script, true);
                        }
                    }
                    else
                    {
                        string script = "Swal.fire({ icon: 'error', title: 'Error', text: 'La descripción de la categoría ya está registrada.' });";
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

        protected void dgvMarcass_RowEditing(object sender, GridViewEditEventArgs e)
        {

            try
            {
                MarcaNegocio negocio = new MarcaNegocio();

                dgvMarcass.EditIndex = e.NewEditIndex;
                ListaMarcas = negocio.listar();
                dgvMarcass.DataSource = ListaMarcas;
                dgvMarcass.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void dgvMarcass_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                Cargar();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }

        }

        private void Cargar()
        {
            try
            {
                MarcaNegocio negocio = new MarcaNegocio();
                dgvMarcass.EditIndex = -1;
                CargaDatos(negocio);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }

        }

        private void CargaDatos(MarcaNegocio negocio)
        {
            try
            {
                ListaMarcas = negocio.listar();
                dgvMarcass.DataSource = ListaMarcas;
                dgvMarcass.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }

        }

        //protected void dgvMarcass_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    try
        //    {
        //        MarcaNegocio negocio = new MarcaNegocio();
        //        Marca marca = new Marca();

        //        int id = Convert.ToInt32(dgvMarcass.DataKeys[e.RowIndex].Value);
        //        string descripcionNueva = (dgvMarcass.Rows[e.RowIndex].FindControl("txtDescripcion") as TextBox).Text;

        //        marca.Id = id;
        //        marca.Descripcion = descripcionNueva;

        //        bool resultado = negocio.ExisteDescripcion(marca.Descripcion);

        //        if (resultado)
        //        {
        //            string script = "Swal.fire({ icon: 'success', title: 'Éxito', text: 'Categoría editada con éxito.' });";
        //            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", script, true);
        //            Cargar();
        //        }
        //        else
        //        {
        //            string script = "Swal.fire({ icon: 'error', title: 'Error', text: 'Hubo un problema al guardar la categoría.' });";
        //            ScriptManager.RegisterStartupScript(this, GetType(), "showError", script, true);
        //        }
        //        // }

        //    }
        //    catch (Exception ex)
        //    {
        //        Session.Add("error", ex);
        //        Response.Redirect("Error.aspx", false);
        //    }

        //}

        protected void dgvMarcass_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                MarcaNegocio negocio = new MarcaNegocio();
                Marca marca = new Marca();

                int id = Convert.ToInt32(dgvMarcass.DataKeys[e.RowIndex].Value);
                string descripcionNueva = (dgvMarcass.Rows[e.RowIndex].FindControl("txtDescripcion") as TextBox).Text;

                marca.Id = id;
                marca.Descripcion = descripcionNueva;

                bool resultado = negocio.ModificarMarcaSP(marca);

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
                // }

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }

        }

        protected void dgvMarcass_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            try
            {
                MarcaNegocio negocio = new MarcaNegocio();
                int id = Convert.ToInt32(dgvMarcass.DataKeys[e.RowIndex].Value);
                bool bandera = negocio.EliminarMarcaSP(id);
                CargaDatos(negocio);

                if (bandera)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "deleteMessage",
                    $"Swal.fire('Eliminado!', 'Se eliminó la marca con Id {id}', 'success').then((result) => {{ if (result.isConfirmed) {{ window.location.href = 'Marcas.aspx'; }} }});", true);

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
            txtDescripcion.Text = "";
            txtDescripcion.Enabled = false;
            lnkCancelar.Enabled = false;
        }

    }
}
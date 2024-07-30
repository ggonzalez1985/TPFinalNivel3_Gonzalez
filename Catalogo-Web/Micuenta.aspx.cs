using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Catalogo_Web
{
    public partial class Micuenta : System.Web.UI.Page
    {
        public string urlImagen { get; set; }
        public List<Articulo> ListaFavoritos { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["Usuario"] != null && Session["Usuario"] is Usuario usuario)
                    {
                        txtNombre.Text = usuario.Nombre;
                        txtApellido.Text = usuario.Apellido;
                        txtMail.Text = usuario.Email;
                        lblCuenta.Text = usuario.Email;
                        if (!string.IsNullOrEmpty(usuario.ImagenUrl))
                            imgNuevoPerfil.ImageUrl = "~/Images/" + usuario.ImagenUrl;
                    }

                    ArticulosNegocio negocio = new ArticulosNegocio();

                    if (Session["Usuario"] != null)
                    {
                        int userId = Convert.ToInt32(((Usuario)Session["Usuario"]).Id);

                        List<int> idsArticulosFavoritos = negocio.ObtenerFavoritos(userId);

                        // Obtener los artículos correspondientes a los IDs
                        ListaFavoritos = negocio.ObtenerArticulosPorIds(idsArticulosFavoritos);

                    }

                    if (ListaFavoritos != null)
                    {
                        dgvFavoritos.DataSource = ListaFavoritos;
                        dgvFavoritos.DataBind();
                    }

                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }


        }

        protected void lnkDatosUsuario_Click(object sender, EventArgs e)
        {
            ShowPanel(pnlUserData);
        }

        protected void lnkFoto_Click(object sender, EventArgs e)
        {
            ShowPanel(pnlProfilePhoto);
        }

        protected void lnkCambiarPass_Click(object sender, EventArgs e)
        {
            ShowPanel(pnlChangePassword);
        }

        protected void lnkFavoritos_Click(object sender, EventArgs e)
        {
            ShowPanel(pnlFavorites);
        }

        private void ShowPanel(Panel panelToShow)
        {
            pnlUserData.Visible = false;
            pnlProfilePhoto.Visible = false;
            pnlChangePassword.Visible = false;
            pnlFavorites.Visible = false;

            panelToShow.Visible = true;
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {

            string actualPassword = txtCurrentPassword.Text;
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            UsuarioNegocio negocio = new UsuarioNegocio();

            bool bandera = false;
            int usuarioId = Convert.ToInt32(((Usuario)Session["Usuario"]).Id);

            try
            {
                bandera = negocio.PassExists(usuarioId, actualPassword);

                if ((!string.IsNullOrEmpty(txtCurrentPassword.Text) || !string.IsNullOrEmpty(txtNewPassword.Text) || !string.IsNullOrEmpty(txtConfirmPassword.Text)) & bandera)
                {

                    if (newPassword == confirmPassword)
                    {
                        bool resultado = false;
                        resultado = negocio.modificarPassword(usuarioId, newPassword);

                        if (resultado)
                        {
                            if (Session["Usuario"] != null && Session["Usuario"] is Usuario usuarioSession)
                            {
                                usuarioSession.Password = txtConfirmPassword.Text;
                                Session["Usuario"] = usuarioSession;
                            }
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "successMessage",
                            "Swal.fire('Éxito', 'La contraseña se ha modificado exitosamente.', 'success').then((result) => { if (result.isConfirmed) { window.location.href = 'Micuenta.aspx'; } });", true);
                        }
                        else
                        {
                            string script = "Swal.fire('Error', 'Se produjo un error actualizando la contraseña, por favor vuelva a intentar.', 'error');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorAlert", script, true);
                            return;
                        }
                    }
                    else
                    {
                        string script = "Swal.fire('Error', 'Las nuevas contraseñas no coinciden.', 'error');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "errorMessage", script, true);
                    }
                }
                else
                {
                    string script = "Swal.fire('Error', 'La actual contraseña no coincide con la registrada para tu usuario, intenta nuevamente.', 'error');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "errorMessage", script, true);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNombre.Text) || !string.IsNullOrEmpty(txtApellido.Text) || !string.IsNullOrEmpty(txtMail.Text))
                {
                    Usuario usuario = new Usuario();
                    UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                    bool resultado = false;

                    usuario.Nombre = txtNombre.Text;
                    usuario.Apellido = txtApellido.Text;
                    usuario.Email = txtMail.Text;

                    resultado = usuarioNegocio.modificarDatos(usuario);

                    if (resultado)
                    {
                        if (Session["Usuario"] != null && Session["Usuario"] is Usuario usuarioSession)
                        {
                            usuarioSession.Nombre = usuario.Nombre;
                            usuarioSession.Apellido = usuario.Apellido;
                            usuarioSession.Email = usuario.Email;

                            Session["Usuario"] = usuarioSession;
                        }

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "successMessage",
                        "Swal.fire('Éxito', 'El usuario se ha modificado exitosamente.', 'success').then((result) => { if (result.isConfirmed) { window.location.href = 'Micuenta.aspx'; } });", true);
                    }
                    else
                    {
                        string script = "alert('Se produjo un error actualizando, por favor vuelva a intentar.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", script, true);
                        return;
                    }

                }
                else
                {
                    string script = "alert('Por favor verificar que los campos correspondientes tengan datos');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", script, true);

                }

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }


        }

        protected void btnCambiarImagen_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario user = (Usuario)Session["usuario"];
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                bool resultado = false;

                user.Nombre = txtNombre.Text;
                user.Apellido = txtApellido.Text;
                user.Email = txtMail.Text;

                if (txtImagen.PostedFile.FileName != "")
                {
                    string ruta = Server.MapPath("./Images/");
                    txtImagen.PostedFile.SaveAs(ruta + "perfil-" + user.Id + ".jpg");
                    user.ImagenUrl = "perfil-" + user.Id + ".jpg";
                }

                resultado = usuarioNegocio.modificarImagen(user);

                Image img = (Image)Master.FindControl("imgAvatar");
                if (img != null)
                {
                    img.ImageUrl = "~/Images/" + user.ImagenUrl;
                }

                if (resultado)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "successMessage",
                    "Swal.fire('Éxito', 'La imagen se ha modificado exitosamente.', 'success');", true);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }

        }

        protected void dgvFavoritos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvFavoritos.SelectedDataKey.Value.ToString();
            Response.Redirect("DetalleArticulo.aspx?id=" + id);
        }
    }
}
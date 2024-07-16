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

        protected void Page_Load(object sender, EventArgs e)
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
            // Implementar lógica para cambiar la contraseña
            string currentPassword = txtCurrentPassword.Text;
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (newPassword == confirmPassword)
            {
                // Lógica para cambiar la contraseña
            }
            else
            {
                // Mostrar mensaje de error
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

                }
                else
                {
                    string script = "alert('Por favor verificar que los campos correspondientes tengan datos');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", script, true);

                }

            }
            catch (Exception)
            {

                throw;
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorMessage",
                    $"alert('Error: {ex.Message}');", true);
            }

        }
    }
}
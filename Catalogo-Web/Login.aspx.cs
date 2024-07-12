using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Catalogo_Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "errorMessage", "Swal.fire('Error', 'Existen campos sin completar.', 'error');", true);
                    return;
                }

                Usuario usuario = new Usuario();
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

                if (usuarioNegocio.EmailExists(txtEmail.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "errorMessage",
                    "Swal.fire({ " +
                    "title: 'Error', " +
                    "text: 'El correo electrónico ya está registrado.', " +
                    "icon: 'error', " +
                    "confirmButtonText: 'OK'" +
                    "}).then((result) => { " +
                    "if (result.isConfirmed) { " +
                    "document.getElementById('" + txtEmail.ClientID + "').value = '';" +
                    "document.getElementById('" + txtEmail.ClientID + "').focus(); " +
                    "}});", true);

                    return;
                }

                usuario.Nombre = txtNombre.Text;
                usuario.Apellido = txtApellido.Text;
                usuario.Email = txtEmail.Text;
                usuario.Password = txtPass.Text;

                usuario.Id = usuarioNegocio.NuevoUsuario(usuario);

                if (usuario.Id > 0)
                {
                    Session.Add("Usuario", usuario);

                    // Mensaje de éxito con redirección
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "successMessage",
                        "Swal.fire('Éxito', 'El usuario se ha creado exitosamente.', 'success').then((result) => { if (result.isConfirmed) { window.location.href = 'Catalogo.aspx'; } });", true);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "clearFields",
                        "setTimeout(function() { document.getElementById('" + txtNombre.ClientID + "').value = ''; document.getElementById('" + txtApellido.ClientID + "').value = ''; document.getElementById('" + txtEmail.ClientID + "').value = ''; document.getElementById('" + txtPass.ClientID + "').value = ''; }, 100);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "errorMessage",
                        "Swal.fire('Error', 'Hubo un problema en la registración del usuario.', 'error');", true);
                }

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                if (string.IsNullOrEmpty(txtEmailLogin.Text) || string.IsNullOrEmpty(txtPassLogin.Text))
                {
                    Session.Add("error", "Debes completar ambos campos...");
                    Response.Redirect("Error.aspx");
                }

                usuario.Email = txtEmailLogin.Text;
                usuario.Password = txtPassLogin.Text;

                if (negocio.Login(usuario))
                {
                    Session.Add("Usuario", usuario);
                    Response.Redirect("Catalogo.aspx", false);
                }
                else
                {
                    Session.Add("error", "User o Pass incorrectos");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "errorMessage",
                        "Swal.fire({ title: 'Error', text: 'Usuario o contraseña incorrectos.', icon: 'error' }).then((result) => { if (result.isConfirmed) { window.location.href = 'Login.aspx'; } });", true);
                }
            }
            catch (System.Threading.ThreadAbortException ex) { }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Login.aspx");
            }
        }
    }
}
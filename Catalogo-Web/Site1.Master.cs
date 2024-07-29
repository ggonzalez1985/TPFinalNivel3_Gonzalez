using Dominio;
using Microsoft.Win32;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Catalogo_Web
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string rutaImagen = "~/Images/PerfilGral.jpg";
            imgPerfil.ImageUrl = rutaImagen;

            if (Seguridad.sesionActiva(Session["Usuario"]))
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                lnkUser.Text = usuario.Nombre;
               if (!string.IsNullOrEmpty(usuario.ImagenUrl))
                    imgPerfil.ImageUrl = "~/Images/" + usuario.ImagenUrl;
            }

            if (!(Page is Login || Page is Catalogo || Page is Default || Page is DetalleArticulo || Page is Categorias || Page is Marcas))
            {
                if (!Seguridad.sesionActiva(Session["Usuario"]))
                {
                    Response.Redirect("Login.aspx", false);
                }
            }

        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Default.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPass.Text))
                {
                    Session.Add("error", "Debes completar ambos campos...");
                    Response.Redirect("Error.aspx");
                }

                usuario.Email = txtEmail.Text;
                usuario.Password = txtPass.Text;

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

        protected void btnCuenta_Click(object sender, EventArgs e)
        {
            Response.Redirect("Micuenta.aspx");
        }

        protected void lnkSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Default.aspx");
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {

        }
    }
}
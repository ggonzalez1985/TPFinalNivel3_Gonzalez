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
    public partial class Login : System.Web.UI.Page
    {
        public bool ConfirmaRegistracion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ConfirmaRegistracion = false;
            }

        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
                {
                    //lblMensaje.Text = "Por favor, completa todos los campos.";
                    //return; /// ESTO IMPLEMENTAR
                }

                Usuario usuario = new Usuario();
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

                usuario.Nombre = txtNombre.Text;
                usuario.Apellido = txtApellido.Text;
                usuario.Email = txtEmail.Text;
                usuario.Password = txtPass.Text;

                usuario.Id = usuarioNegocio.NuevoUsuario(usuario);
                ConfirmaRegistracion = true;

                //if (usuario.Id > 0)
                //{
                //    Session.Add("Usuarios", usuario);
                //}
                //else
                //{                  

                //}
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnRegistroOk_Click(object sender, EventArgs e)
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPass.Text = string.Empty;
        }
    }
}
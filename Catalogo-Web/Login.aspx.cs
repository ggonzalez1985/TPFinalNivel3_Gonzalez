﻿using Dominio;
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

                usuario.Nombre = txtNombre.Text;
                usuario.Apellido = txtApellido.Text;
                usuario.Email = txtEmail.Text;
                usuario.Password = txtPass.Text;

                usuario.Id = usuarioNegocio.NuevoUsuario(usuario);

                if (usuario.Id > 0)
                {
                    Session.Add("Usuario", usuario);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "successMessage", "Swal.fire('Éxito', 'El usuario se ha creado exitosamente.', 'success');", true);


                    // Evitar el postback adicional
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "clearFields", "setTimeout(function() { document.getElementById('" + txtNombre.ClientID + "').value = ''; document.getElementById('" + txtApellido.ClientID + "').value = ''; document.getElementById('" + txtEmail.ClientID + "').value = ''; document.getElementById('" + txtPass.ClientID + "').value = ''; }, 100);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "errorMessage", "Swal.fire('Error', 'Hubo un problema en la registración del usuario.', 'error');", true);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }


    }
}
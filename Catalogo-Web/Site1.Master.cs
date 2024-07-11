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
            imgPerfil.ImageUrl = "https://simg.nicepng.com/png/small/202-2022264_usuario-annimo-usuario-annimo-user-icon-png-transparent.png";

            if (Seguridad.sesionActiva(Session["Usuario"]))
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                lblUser.Text = usuario.Nombre;

                // TODO: revisar esta parte de la imagen de perfil de usuario
            }

            if (!(Page is Login || Page is Catalogo || Page is Default))
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
    }
}
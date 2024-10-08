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
    public partial class Favoritos : System.Web.UI.Page
    {

        public List<Articulo> ListaArticulo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ArticulosNegocio negocio = new ArticulosNegocio();

                    if (Session["Usuario"] != null)
                    {
                        int userId = Convert.ToInt32(((Usuario)Session["Usuario"]).Id);
                        List<int> idsArticulosFavoritos = negocio.ObtenerFavoritosSP(userId);
                        ListaArticulo = negocio.ObtenerArticulosPorIds(idsArticulosFavoritos);

                        if (idsArticulosFavoritos.Count == 0)
                        {
                            ListaArticulo = null;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorMessage", "Swal.fire('Advertencia', 'No tenes artículos favoritos.', 'warning');", true);
                        }

                        if (ListaArticulo == null)
                        {
                            lblResultados.Text = "-";
                            lblRegistros.Text = "-";
                        }
                        else
                        {
                            lblResultados.Text = ListaArticulo.Count.ToString();
                            lblRegistros.Text = "Favoritos";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ListaArticulo = new List<Articulo>(ListaArticulo);
        }
    }
}
using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Catalogo_Web
{
    public partial class DetalleArticulo : System.Web.UI.Page
    {
        public Articulo seleccionado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
                if (id != "" && !IsPostBack)
                {
                    ArticulosNegocio negocio = new ArticulosNegocio();
                    //Articulo seleccionado = new Articulo();

                    seleccionado = (negocio.Listararticulos(id))[0];

                    //TIENE QUE AGREGAR UN SOLO ARTICULO - ESE METODO QUE USA NO ESTA BIEN

                    //Session.Add("articuloSeleccionado", seleccionado);



                    


                }



            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx");
            }
        }
    }
}
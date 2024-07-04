using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Catalogo_Web
{
    public partial class Catalogo : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            ArticulosNegocio negocio = new ArticulosNegocio();
            ListaArticulo = negocio.Listararticulos();
            
            try
            {
                

                if (!IsPostBack)
                { 
                    Session["listaArticulos"] = ListaArticulo;

                    CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                    List<Categoria> listaCategorias = categoriaNegocio.listar();
                    Session["listaCategorias"] = listaCategorias;

                    MarcaNegocio marcaNegocio = new MarcaNegocio();
                    List<Marca> listaMarcas = marcaNegocio.listar();
                    Session["listaMarcas"] = listaMarcas;

                    DdlCategorias.DataSource = listaCategorias;
                    DdlCategorias.DataValueField = "Id";
                    DdlCategorias.DataTextField = "Descripcion";
                    DdlCategorias.DataBind();
                    DdlCategorias.Items.Insert(0, new ListItem("", ""));

                    DdlMarcas.DataSource = listaMarcas;
                    DdlMarcas.DataValueField = "Id";
                    DdlMarcas.DataTextField = "Descripcion";
                    DdlMarcas.DataBind();
                    DdlMarcas.Items.Insert(0, new ListItem("", ""));
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx");
            }
        }

        protected void DdlCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                int id = int.Parse(DdlCategorias.SelectedItem.Value); //aca cobntrolar que si se eligio vacio que no explote
                List<Articulo> filteredArticulos = ((List<Articulo>)Session["listaArticulos"]).FindAll(x => x.IdCategoria.Id == id);

                List<Marca> listaMarcas = new List<Marca>();

                foreach (Articulo articulo in filteredArticulos)
                {   
                        listaMarcas.Add(articulo.IdMarca);
                }

                DdlMarcas.DataSource= listaMarcas;
                DdlMarcas.DataBind();
                DdlMarcas.Items.Insert(0, new ListItem("", ""));

            }
        }
    }
}
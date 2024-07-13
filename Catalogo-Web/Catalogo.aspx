<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Catalogo.aspx.cs" Inherits="Catalogo_Web.Catalogo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .fixed-size-card {
            max-width: 254px;
            height: 254px;
        }

            .fixed-size-card img {
                width: 250px;
                height: 250px;
                object-fit: contain;
            }
    </style>

    <style>
        .custom-container {
            max-width: 100%;
            margin: auto;
        }
    </style>

    <style>
        .form-control::-webkit-input-placeholder {
            color: #fff;
        }
    </style>

    <style>
        body {
            background-color: lightgray;
            height: 100vh;
            width: 100%;
        }
    </style>

    <style>
        .card:hover .card-title,
        .card:hover .card-text {
            text-decoration: underline;
        }
    </style>

    <style>
        .favorite-font {
            color: green;
        }
    </style>

</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="container-fluid" style="background-color: #000;">

        <div class="row justify-content-center">
            <div class="col-6 ">
                <form class="card card-sm" style="background-color: #000; color: #fff; border-color: #fff;">
                    <div class="card-body row no-gutters align-items-center">
                        <div class="col-auto">
                            <i class="fas fa-search h4 text-body"></i>
                        </div>
                        <div class="col">
                            <asp:TextBox runat="server" ID="txtFiltro" AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged" class="form-control form-control-lg form-control-borderless" type="search" placeholder="Search..." Style="color: #fff; background-color: #000;" />
                        </div>

                        <div class="col-auto">
                            <asp:Button runat="server" ID="btnReset" Text="🔄" OnClick="btnReset_Click" class="btn btn-lg btn-outline-light" Style="color: white; background-color: black; border-style: none; margin-left: -86px; border-width: 0;" />
                        </div>
                    </div>
                    <br />
                </form>
            </div>
            <div class="col-2 ">
                <asp:CheckBox Text="Filtro Avanzado" CssClass="position-relative" ID="chkAvanzado" Style="color: #FFFFFF; top: 15px;" runat="server" AutoPostBack="true" OnCheckedChanged="chkAvanzado_CheckedChanged" />
            </div>
        </div>


        <%if (chkAvanzado.Checked)
            { %>
        <div class="row">

            <div class="col-3">
                <div class="mb-3">
                    <asp:Label Text="Categoria:" Style="color: #fff;" runat="server" />
                    <asp:DropDownList ID="DdlCategorias" Style="color: #fff; background-color: #000;" CssClass="form-select" runat="server" OnSelectedIndexChanged="DdlCategorias_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-3">
                <div class="mb-3">
                    <asp:Label Text="Marca:" Style="color: #fff;" runat="server" />
                    <asp:DropDownList ID="DdlMarcas" Style="color: #fff; background-color: #000;" CssClass="form-select" runat="server"></asp:DropDownList>
                </div>
            </div>

            <div class="col-2">
                <div class="mb-3">
                    <asp:Label Text="Precio Min.:" Style="color: #fff;" runat="server" />
                    <asp:TextBox runat="server" ID="txtPrecioMin" Style="color: #fff; background-color: #000;" CssClass="form-control" />
                </div>
            </div>

            <div class="col-2">
                <div class="mb-3">
                    <asp:Label Text="Precio Max.:" Style="color: #fff;" runat="server" />
                    <asp:TextBox runat="server" ID="txtPrecioMax" Style="color: #fff; background-color: #000;" CssClass="form-control" />
                </div>
            </div>

            <div class="col-2">
                <div class="mb-3">
                    <asp:Button Text="🔍" runat="server" ID="btnBuscar" OnClick="btnBuscar_Click" class="btn btn-outline-light" Style="color: white; background-color: #000; border: 2px solid blue; height: 38px; margin-top: 24px; text-align: center" />
                </div>
            </div>

        </div>
        <%} %>
    </div>


    <div class="container custom-container">
        <br />

        <div class="row">

            <div class="col-2">

                <div class="mb-3">
                    <asp:Label ID="lblMostrando" Text="Mostrando:" Visible="false" runat="server" class="form-label fw-bold fs-4" />
                    <div>
                        <asp:Label ID="lblResultados" Visible="false" runat="server" class="form-label fs-5" />
                    </div>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblResultadoPara" Text="Resultados para:" Visible="false" runat="server" class="form-label fw-bold fs-4" />
                    <div>
                        <asp:Label ID="lblRegistros" Visible="false" runat="server" class="form-label fs-5" />
                    </div>
                </div>

            </div>


            <div class="col">
                <ul class="nav nav-tabs">
                    <li class="nav-item">
                        <a class="nav-link" style="color: white; background-color: black; border: 2px black; border-bottom-color: black;" aria-current="page">Nuestros Artículos</a>
                    </li>
                </ul>
                <br />
                <div class="row row-cols-1 row-cols-md-3 g-4">

                    <% if (ListaArticulo != null)
                        { %>
                    <% foreach (Dominio.Articulo articulo in ListaArticulo)
                        { %>
                    <div class="col">
                        <div class="card h-100 fixed-size-card">
                            <a href="DetalleArticulo.aspx?id=<%: articulo.Id %>" style="text-decoration: none; display: block; height: 100%; color: black;">
                                <img src="<%: articulo.ImagenUrl %>" onerror="this.onerror=null; this.src='Images/img-nd.jpg'" class="card-img-top" alt="Imagen Articulos">
                                <div class="card-body text-center">
                                    <% 
                                        // Verificar si el artículo está en la lista de favoritos
                                        bool esFavorito = ListaFavoritos != null && ListaFavoritos.Any(fav => fav.Id == articulo.Id);
                        %>
                                    <h5 class="card-title">
                                        <%: articulo.Nombre %>
                                        <% if (esFavorito)
                                        { %> ❤️ <% } %>
                        </h5>
                                    <p class="card-text">$<%: string.Format("{0:#,##0.00}", articulo.Precio) %></p>
                                </div>
                            </a>
                        </div>
                    </div>
                    <% } %>
                    <% }
                    else
                    { %>
                    <p>No se encontraron artículos.</p>
                    <% } %>
                </div>
            </div>



        </div>
    </div>






</asp:Content>

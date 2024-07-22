<%@ Page Title="Favoritos" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Favoritos.aspx.cs" Inherits="Catalogo_Web.Favoritos" %>

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
        .group-box {
            border: 1px solid #ddd;
            border-radius: 4px;
            margin-bottom: 20px;
            padding: 15px;
            background-color: #f9f9f9;
        }

        .group-box-header {
            border-bottom: 1px solid #ddd;
            margin-bottom: 10px;
            padding-bottom: 5px;
        }

            .group-box-header h4 {
                margin: 0;
            }

        .group-box-body {
            padding: 10px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="container-fluid" style="background-color: #000;">

        <div class="row justify-content-center">
            <div class="col-6 ">
                <form class="card card-sm" style="background-color: #000; color: #fff; border-color: #fff;">
                    <div class="card-body row no-gutters align-items-center">
                        <div class="col-auto">
                            <i class="fas fa-search h4 text-body"></i>
                        </div>

                    </div>
                    <br />
                </form>
            </div>
        </div>

    </div>


    <div class="container custom-container">
        <br />

        <div class="row">

            <div class="col-2">

                <asp:Panel ID="pnlResultados" runat="server" CssClass="group-box">
                    <div class="group-box-header">
                        <h4>Resultados</h4>
                    </div>
                    <div class="group-box-body">
                        <div class="mb-3">
                            <asp:Label ID="lblMostrando" Text="Mostrando:" runat="server" CssClass="form-label fw-bold fs-4" />
                            <div>
                                <asp:Label ID="lblResultados" runat="server" CssClass="form-label fs-5" />
                            </div>
                        </div>
                        <div class="mb-3">
                            <asp:Label ID="lblResultadoPara" Text="Resultados para:" runat="server" CssClass="form-label fw-bold fs-4" />
                            <div>
                                <asp:Label ID="lblRegistros" runat="server" CssClass="form-label fs-5" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>

            </div>


            <div class="col">
                <ul class="nav nav-tabs">
                    <li class="nav-item">
                        <a class="nav-link" style="color: white; background-color: black; border: 2px black; border-bottom-color: black;" aria-current="page">Tus favoritos</a>
                    </li>
                </ul>
                <br />
                <div class="row row-cols-1 row-cols-md-3 g-4">

                    <%if (ListaArticulo != null)
                        { %>

                    <% foreach (Dominio.Articulo articulo in ListaArticulo)
                        { %>

                    <div class="col">
                        <div class="card h-100 fixed-size-card">

                            <a href="DetalleArticulo.aspx?id=<%: articulo.Id %>" style="text-decoration: none; display: block; height: 100%; color: black;">

                                <%--   <img src="<%: articulo.ImagenUrl %>" onerror="this.onerror=null; this.src='Images/img-nd.jpg'"
                                    class="card-img-top" alt="Imagen Articulos">--%>

                                <img src="<%: ResolveUrl(articulo.ImagenUrl) %>" onerror="this.onerror=null; this.src='<%= ResolveUrl("~/Images/img-nd.jpg") %>'"
                                    class="figure-img fixed-size-img" alt="Imagen Articulos">

                                <div class="card-body text-center">
                                    <h5 class="card-title"><%: articulo.Nombre %></h5>
                                    <p class="card-text">$<%: string.Format("{0:#,##0.00}", articulo.Precio) %></p>
                                </div>
                            </a>
                        </div>
                    </div>

                    <% }%>

                    <%}
                        else
                        {  %>
                    <p>No tenes artículos favoritos</p>
                    <% }%>
                </div>
            </div>



        </div>
    </div>


</asp:Content>

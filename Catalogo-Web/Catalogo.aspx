<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Catalogo.aspx.cs" Inherits="Catalogo_Web.Catalogo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="container-fluid" style="background-color: #000;">

        <div class="row justify-content-center">
            <div class="col-12 col-md-10 col-lg-8">
                <form class="card card-sm" style="background-color: #000; color: #fff; border-color: #fff;">
                    <div class="card-body row no-gutters align-items-center">
                        <div class="col-auto">
                            <i class="fas fa-search h4 text-body"></i>
                        </div>
                        <div class="col">
                            <input class="form-control form-control-lg form-control-borderless" type="search" placeholder="Search..." style="color: #fff; background-color: #000;">
                        </div>

                    </div>
                    <br />
                </form>
            </div>
        </div>

        <div class="row">

            <div class="col-3">
                <div class="mb-3">
                    <asp:Label Text="Categoria:" Style="color: #fff;" runat="server" />
                    <asp:DropDownList ID="DdlCategorias" Style="color: #fff; background-color: #000;" CssClass="form-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlCategorias_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-3">
                <div class="mb-3">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Label Text="Marca:" Style="color: #fff;" runat="server" />
                            <asp:DropDownList ID="DdlMarcas" Style="color: #fff; background-color: #000;" CssClass="form-select" runat="server"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
                <button class="btn btn-lg btn-success" type="submit" style="color: white; background-color: black; border: 1px solid white; margin-top: 15px">Buscar</button>
            </div>
        </div>

    </div>
 </div>


    <div class="container custom-container">
        <br />
        <div class="row">
            <div class="col">
                <ul class="nav nav-tabs">
                    <li class="nav-item">
                        <a class="nav-link active" style="color: white; background-color: black; border: 1px solid white;" aria-current="page" href="#">Nuestros Artículos</a>
                    </li>
                </ul>
                <br />
                <div class="row row-cols-1 row-cols-md-3 g-4">
                    <% foreach (Dominio.Articulo articulo in ListaArticulo)
                        { %>
                    <div class="col">
                        <div class="card h-100 fixed-size-card">
                            <img src="<%: articulo.ImagenUrl %>" onerror="this.onerror=null; this.src='Images/img-no-disponible.jpg'" class="card-img-top" alt="Imagen Articulos">
                            <div class="card-body text-center">
                                <h5 class="card-title"><%: articulo.Nombre %></h5>
                                <p class="card-text">$<%: string.Format("{0:#,##0.00}", articulo.Precio) %></p>
                            </div>
                        </div>
                    </div>
                    <% } %>
                </div>
            </div>
        </div>
    </div>

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
            max-width: 900px;
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
            /*background-color: black;*/
            height: 100vh;
            width: 100%;
        }
    </style>



</asp:Content>

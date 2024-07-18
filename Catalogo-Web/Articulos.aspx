﻿<%@ Page Title="Articulos" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Articulos.aspx.cs" Inherits="Catalogo_Web.Articulos" %>

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
    <style>
    #<%= txtFiltro.ClientID %>::-webkit-input-placeholder 
    { /* Chrome, Safari, Opera */
        color: blue;
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
                        <div class="col">
                            <asp:TextBox runat="server" ID="txtFiltro" AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged" 
                            class="form-control form-control-lg form-control-borderless" type="search" placeholder="Search..." 
                            Style="color: blue; background-color: #000;" />

                        </div>

                        <div class="col-auto">
                            <asp:Button runat="server" ID="btnReset" Text="🔄" OnClick="btnReset_Click" class="btn btn-lg btn-outline-light"
                                Style="color: white; background-color: black; border-style: none; margin-left: -86px; border-width: 0;" />
                        </div>
                    </div>
                    <br />
                </form>
            </div>
            <div class="col-2 ">
                <asp:CheckBox Text="Filtro Avanzado" CssClass="position-relative" ID="chkAvanzado" Style="color: #FFFFFF; top: 15px;"
                    runat="server" AutoPostBack="true" OnCheckedChanged="chkAvanzado_CheckedChanged" />
            </div>

        </div>

        <%if (chkAvanzado.Checked)
            { %>
        <div class="row">

            <div class="col-3">
                <div class="mb-3">
                    <asp:Label Text="Categoria:" Style="color: #fff;" runat="server" />
                    <asp:DropDownList ID="DdlCategorias" Style="color: #fff; background-color: #000;" CssClass="form-select" runat="server">
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
                    <asp:Button Text="🔍" runat="server" ID="btnBuscar" OnClick="btnBuscar_Click" class="btn btn-outline-light"
                        Style="color: white; background-color: #000; border: 2px solid blue; height: 38px; margin-top: 24px; text-align: center" />
                </div>
            </div>

        </div>
        <%} %>
    </div>


    <div class="container custom-container">
        <br />

        <asp:Label ID="Label1" runat="server" Text="📋...Listado de Articulos...📋" class="form-label fw-bold fs-4" style="display: block; text-align: center;"></asp:Label>

        <div class="row">

            <div class="col-2">

                <div class="mb-3">
                    <asp:Label ID="lblMostrando" Text="Mostrando:" runat="server" class="form-label fw-bold fs-4" />
                    <div>
                        <asp:Label ID="lblResultados" runat="server" class="form-label fs-5" />
                    </div>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblResultadoPara" Text="Resultados para:" runat="server" class="form-label fw-bold fs-4" />
                    <div>
                        <asp:Label ID="lblRegistros" runat="server" class="form-label fs-5" />
                    </div>
                </div>

            </div>


            <div class="col">

                <asp:GridView ID="dgvArticulos" DataKeyNames="Id" CssClass="table" AutoGenerateColumns="false"
                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged" runat="server">
                    <Columns>
                        <asp:BoundField HeaderText="Id" DataField="Id" />
                        <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
                        <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                        <asp:BoundField HeaderText="Categoria" DataField="IdCategoria.Descripcion" />
                        <asp:BoundField HeaderText="Marca" DataField="IdMarca.Descripcion" />
<%--                        <asp:CheckBoxField HeaderText="Imagen(Si/No)" DataField="TieneImagen" />--%>
                        <asp:CommandField HeaderText="Detalles..." ShowSelectButton="true" SelectText="📝" />
                    </Columns>
                </asp:GridView> 

            </div>



        </div>
    </div>

</asp:Content>

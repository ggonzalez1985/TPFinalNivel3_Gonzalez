<%@ Page Title="Articulos" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Articulos.aspx.cs" Inherits="Catalogo_Web.Articulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" integrity="sha384-xxxx" crossorigin="anonymous">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert2/11.4.19/sweetalert2.min.css">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/sweetalert2/11.4.19/sweetalert2.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11.7.4/dist/sweetalert2.all.min.js"></script>

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
    #<%= txtFiltro.ClientID %>::placeholder { /* Modern browsers */
        color: blue;
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

                <asp:Panel ID="pnlBotones" runat="server" CssClass="group-box">
                <div class="group-box-header">
                 <h4>Acciones</h4>
                </div>
                <div class="group-box-body">
                
                <div class="mb-3">

                <asp:LinkButton ID="lnkNuevoArticulo" runat="server" CssClass="btn btn-dark text-white" 
                    OnClick="LinkButton1_Click">➕ Nuevo</asp:LinkButton>
                    <br /><br />
                    <asp:LinkButton ID="lnkEliminar" runat="server" CssClass="btn btn-dark text-white"
                    OnClientClick="return confirmarEliminacion();" OnClick="lnkEliminar_Click">❌ Eliminar</asp:LinkButton>


                </div>
                </div>
                </asp:Panel>

            </div>


            <div class="col">

                <ul class="nav nav-tabs">
                <li class="nav-item">
                 <a class="nav-link" style="color: white; background-color: black; border: 2px black; border-bottom-color: black;" aria-current="page">Listado de Articulos</a>
                </li>
                </ul>

                <br />

                <asp:GridView ID="dgvArticulos" DataKeyNames="Id" CssClass="table" AutoGenerateColumns="false"
                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged" runat="server">
                    <Columns>
                        <asp:BoundField HeaderText="Id" DataField="Id" />
                        <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
                        <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                        <asp:BoundField HeaderText="Categoria" DataField="IdCategoria.Descripcion" />
                        <asp:BoundField HeaderText="Marca" DataField="IdMarca.Descripcion" />
                        <asp:CommandField HeaderText="Editar" ShowSelectButton="true" SelectText="📝" />
                        <asp:TemplateField HeaderText="Eliminar">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkEliminar" runat="server" />
                        </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

               
            </div>

            


        </div>
    </div>


    


</asp:Content>
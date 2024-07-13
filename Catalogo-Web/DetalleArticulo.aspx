<%@ Page Title="Detalle de Articulo" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DetalleArticulo.aspx.cs" Inherits="Catalogo_Web.DetalleArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Detalle de Articulo</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" integrity="sha384-k6RqeWeci5ZR/Lv4MR0sA0FfDOMiW6mLczcI5o5Lo6r9oBXAz5rs7tOXc7pB8KZ0" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <style>
        .fixed-size-img {
            width: 300px;
            height: 300px;
            object-fit: contain;
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
        .favorite-btn {
            background-color: transparent;
            border: none;
            color: #ff0000;
            font-size: 2em; /* Tamaño del texto del botón */
            cursor: pointer;
            padding: 0.1em 1em; /* Relleno del botón */
            text-align: left; /* Alineación del texto a la izquierda */
        }

            .favorite-btn:hover {
                color: #ff6666;
            }
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


    <% if (seleccionado != null)
        { %>
    <div class="container">
        <div class="row">
            <div class="col-4">
                <figure class="figure">
                    <img src="<%: seleccionado.ImagenUrl %>" class="figure-img fixed-size-img" alt="alt" onerror="this.onerror=null; this.src='Images/img-nd.jpg'">
                    <figcaption class="figure-caption" style="text-align: center;">ID del producto: <%: seleccionado.Id %></figcaption>
                </figure>
            </div>
            <div class="col-6">
                <div class="card" style="width: 18rem; margin-top: 50px;">
                    <div class="card-body">
                        <h3 class="card-title"><%: seleccionado.Nombre %></h3>
                        <br />
                        <h6 class="card-subtitle mb-2 text-body-secondary"><%: seleccionado.Descripcion %></h6>
                        <br />
                        <h3 class="card-title">$<%: string.Format("{0:#,##0.00}", seleccionado.Precio) %></h3>
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnAgregarFavoritos" runat="server" CssClass="favorite-btn" Text="♡" OnClick="btnAgregarFavoritos_Click" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnAgregarFavoritos" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <% }
        else
        { %>
    <p>No se encontró el artículo seleccionado.</p>
    <% } %>
</asp:Content>

<%@ Page Title="Edita Tu Articulo" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DetalleArticuloEditable.aspx.cs" Inherits="Catalogo_Web.DetalleArticuloEditable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="container-fluid">

        <div class="row">

            <div class="col-4">
                <br />
                <label class="form-label" style="font-weight: bold; font-size: 18px; display: block; margin: 0 auto; text-align: center;">Datos asociados a al articulo</label>
                <br />

                <div>
                    <label class="form-label" style="font-weight: bold;">Id:</label>
                    <asp:TextBox ID="txtId" ReadOnly="true" Style="cursor: not-allowed; width: 100px;" class="form-control" runat="server"></asp:TextBox>
                </div>

                <div>
                    <label class="form-label" style="font-weight: bold;">Codigo:</label>
                    <asp:TextBox ID="txtCodigo" CssClass="form-control" runat="server" ></asp:TextBox>
                </div>

                <div>
                    <label class="form-label" style="font-weight: bold;">Nombre:</label>
                    <asp:TextBox ID="txtNombre" class="form-control" runat="server"></asp:TextBox>
                </div>

                <div>
                    <label class="form-label" style="font-weight: bold;">Descripcion:</label>
                    <asp:TextBox ID="txtDescripcion" TextMode="MultiLine" class="form-control" runat="server"></asp:TextBox>
                </div>

                <div style="display: flex; align-items: center;">
                    <div style="flex: 1;">
                        <label class="form-label" style="font-weight: bold;">Marca:</label>
                        <asp:DropDownList ID="DdlMarca" CssClass="form-select" runat="server"></asp:DropDownList>
                    </div>
                    <asp:Button ID="btnAgregarMarca" Text="+" class="btn btn-primary" runat="server" Style="margin-left: 20px; margin-top: 30px;" />
                </div>

                <div style="display: flex; align-items: center;">
                    <div style="flex: 1;">
                        <label class="form-label" style="font-weight: bold;">Categoria:</label>
                        <asp:DropDownList ID="DdlCategoria" CssClass="form-select" runat="server"></asp:DropDownList>
                    </div>
                    <asp:Button ID="btnAgregarCategoria" Text="+" class="btn btn-primary" runat="server" Style="margin-left: 20px; margin-top: 30px;" />
                </div>

                <div>
                    <label class="form-label" style="font-weight: bold;">Precio:</label>
                    <asp:TextBox ID="TextBox1" class="form-control" runat="server" Style="width: 50%;"></asp:TextBox>
                </div>




                <br />
                <div>
                    <asp:Button ID="btnModificar" Text="Cambiar datos personales" class="btn btn-primary" runat="server" />
                </div>
            </div>





            <div class="col-4">


                <div class="mb-3">
                    <label class="form-label">Imagen del articulo</label>
                    <input type="file" id="txtImagen" runat="server" class="form-control" onchange="previewImage(this);" />
                </div>

                <asp:Image ID="imgArticulo" ImageUrl="https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg"
                    runat="server" CssClass="img-fluid mb-3" />

                <div>
                    <br />
                    <asp:Button ID="btnCambiarImagen" Text="Cambiar imagen" class="btn btn-primary" runat="server" />
                </div>


            </div>

        </div>

    </div>

</asp:Content>

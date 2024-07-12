<%@ Page Title="Detalle de articulo" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DetalleArticulo.aspx.cs" Inherits="Catalogo_Web.DetalleArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="container-fluid">

        <div class="row">

            <div class="col -6">
                <figure class="figure">
                    <img src="..." class="figure-img img-fluid rounded" alt="alt">
                    <figcaption class="figure-caption">A caption for the above image.</figcaption>
                </figure>

            </div>

            <div class="col -6">


                <div class="card" style="width: 18rem;">
                    <div class="card-body">
                        <h5 class="card-title"><%: seleccionado.Nombre %></h5>
                        <br />  
                        <h6 class="card-subtitle mb-2 text-body-secondary"><%: seleccionado.Descripcion %></h6>
                        <br />  
                        <h5 class="card-title"><%: seleccionado.Precio.ToString("C") %></h5>
                    </div>
                </div>


            </div>


        </div>

    </div>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Marcas.aspx.cs" Inherits="Catalogo_Web.Marcas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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

    <style>
        .custom-container {
            max-width: 100%;
            margin: auto;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">



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
                            <asp:LinkButton ID="lnkNuevaMarca" runat="server" CssClass="btn btn-dark text-white">➕ Nuevo</asp:LinkButton>
                            <br />
                            <br />

                            <div>
                                <label for="txtDescripcion" class="form-label">Descripción</label>
                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control mb-3" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                <asp:LinkButton ID="lnkGuardar" Text="Guardar" runat="server" OnClick="lnkGuardar_Click" CssClass="btn btn-primary btn-inline small-button" />
                            </div>
                        </div>

                    </div>
                </asp:Panel>



            </div>

        </div>

    </div>
</asp:Content>

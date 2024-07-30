<%@ Page Title="Categorias" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="Catalogo_Web.Categorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" integrity="sha384-xxxx" crossorigin="anonymous">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert2/11.4.19/sweetalert2.min.css">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/sweetalert2/11.4.19/sweetalert2.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11.7.4/dist/sweetalert2.all.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.js" type="text/javascript"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.css" rel="stylesheet" />

    <!-- Incluye SweetAlert CSS y JavaScript -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.all.min.js"></script>

    <script type="text/javascript">
        var object = { status: false, ele: null };
        function conFunction(ev) {
            var evnt = ev;
            if (object.status) { return true; }
            Swal.fire({
                title: "¿Está seguro?",
                text: "¡No podrá recuperar esta categoria!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#d33",
                cancelButtonColor: "#3085d6",
                confirmButtonText: "¡Sí, elimínelo!",
                closeOnConfirm: true
            }).then((result) => {
                if (result.isConfirmed) {
                    object.status = true;
                    object.ele = evnt;
                    evnt.click();
                }
            })

            return object.status;
        };
    </script>

    <script>    
        function validarCampos() {
            var descripcion = document.getElementById('<%= txtDescripcion.ClientID %>');
            var isValid = true;

            if (descripcion.value.trim() === '') {
                descripcion.classList.remove('is-valid');
                descripcion.classList.add('is-invalid');
                isValid = false;
            } else {
                descripcion.classList.remove('is-invalid');
                descripcion.classList.add('is-valid');
            }

            return isValid; // Solo se enviará el formulario si isValid es true
        }

    </script>

    <style>
        .is-valid {
            border-color: #28a745;
        }

        .is-invalid {
            border-color: #dc3545;
        }
    </style>

    \
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
        .btn-inline {
            display: inline-block;
        }

        .btn-margin-left {
            margin-left: 20px; /* Aumenta este valor para más espacio */
        }
    </style>

    <style>
        /* Clase para botones pequeños */
        .small-button {
            padding: 5px 10px; /* Ajusta el padding si es necesario */
            font-size: 0.875rem; /* Tamaño de fuente más pequeño */
        }

        /* Clase para el botón de cancelar con margen superior */
        .btn-cancelar {
            margin-top: 10px;
        }

        /* Opcional: Estilo para el botón de guardar */
        .btn-success {
            background-color: #28a745; /* Verde más tranquilo */
            border-color: #28a745;
        }

        /* Opcional: Estilo para el botón de cancelar */
        .btn-secondary {
            background-color: #6c757d; /* Gris más neutro */
            border-color: #6c757d;
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
                            <asp:LinkButton ID="lnkNuevoArticulo" runat="server" CssClass="btn btn-dark text-white" OnClick="lnkNuevoArticulo_Click">➕ Nuevo</asp:LinkButton>
                            <br />
                            <br />

                            <div>
                                <label for="txtDescripcion" class="form-label">Descripción</label>
                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control mb-3" TextMode="MultiLine" Rows="2" Enabled="false"></asp:TextBox>

                                <asp:LinkButton ID="lnkGuardar" runat="server" Text="Guardar"
                                    CssClass="btn btn-success btn-inline small-button"
                                    OnClientClick="return validarCampos();" OnClick="lnkGuardar_Click" Enabled="false" />

                                <asp:LinkButton ID="lnkCancelar" runat="server" Text="Cancelar"
                                    CssClass="btn btn-secondary btn-cancelar"
                                    OnClientClick="" OnClick="lnkCancelar_Click" Enabled="false" />

                            </div>
                        </div>
                    </div>
                </asp:Panel>



            </div>


            <div class="col">

                <ul class="nav nav-tabs">
                    <li class="nav-item">
                        <a class="nav-link" style="color: white; background-color: black; border: 2px black; border-bottom-color: black;" aria-current="page">Listado de Categorias</a>
                    </li>
                </ul>

                <br />


                <asp:GridView ID="dgvCategoriass" DataKeyNames="Id"
                    OnRowEditing="dgvCategoriass_RowEditing"
                    OnRowCancelingEdit="dgvCategoriass_RowCancelingEdit"
                    OnRowDeleting="dgvCategoriass_RowDeleting"
                    OnRowUpdating="dgvCategoriass_RowUpdating"
                    CssClass="table" AutoGenerateColumns="False" runat="server">

                    <Columns>

                        <asp:BoundField HeaderText="Id" DataField="Id" />
                        <asp:TemplateField HeaderText="Descripcion">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDescripcion" runat="server" Text='<%# Bind("Descripcion") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDescripcion" runat="server" Text='<%# Bind("Descripcion") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:CommandField ShowEditButton="true" HeaderText="Edicion" ControlStyle-CssClass="btn btn-primary" />

                        <asp:TemplateField HeaderText="Accion">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEliminar" CommandName="Delete" runat="server" CssClass="btn btn-secondary text-white"
                                    Text="🗑️" OnClientClick="return conFunction(this);" />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>


                </asp:GridView>


            </div>


        </div>
    </div>





</asp:Content>

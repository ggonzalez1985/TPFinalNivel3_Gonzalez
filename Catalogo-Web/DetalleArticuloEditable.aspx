<%@ Page Title="Edita Tu Articulo" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DetalleArticuloEditable.aspx.cs" Inherits="Catalogo_Web.DetalleArticuloEditable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <script type="text/javascript">
        function confirmarCancelacion() {
            Swal.fire({
                title: '¿Desea cancelar la edición?',
                text: "¡Los cambios no guardados se perderán!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Sí, cancelar',
                cancelButtonText: 'No, seguir editando'
            }).then((result) => {
                if (result.isConfirmed) {
                    // Recargar la página si el usuario confirma
                    window.location.reload();
                }
                // No hacer nada si el usuario cancela
            });
        }
    </script>


    <script type="text/javascript">
        function confirmarEliminacion() {
            Swal.fire({
                title: '¿Está seguro de eliminar?',
                text: "¡No podrá revertir esto!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Sí, eliminarlo!',
                cancelButtonText: 'Cancelar'
            }).then((result) => {
                if (result.isConfirmed) {
                    __doPostBack('<%= btnEliminar.UniqueID %>', '');
                }
            });
        }
    </script>

    <style>
        .fixed-size-img {
            width: 300px;
            height: 300px;
            object-fit: contain;
        }
    </style>

    <script>
        function previewImage(input) {
            var imgArticulo = document.getElementById('<%= imgArticulo.ClientID %>');
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    imgArticulo.src = e.target.result;
                }
                reader.readAsDataURL(input.files[0]);
            } else {
                imgArticulo.src = 'Images/img-nd.jpg';
            }
        }
    </script>


    <script>
        function validarFormulario() {
            // Obtener referencias a los campos
            var txtCodigo = document.getElementById('<%= txtCodigo.ClientID %>');
            var txtNombre = document.getElementById('<%= txtNombre.ClientID %>');
            var txtDescripcion = document.getElementById('<%= txtDescripcion.ClientID %>');
            var ddlCategoria = document.getElementById('<%= DdlCategoria.ClientID %>');
            var ddlMarca = document.getElementById('<%= DdlMarca.ClientID %>');
            var txtPrecio = document.getElementById('<%= txtPrecio.ClientID %>');

            // Variables para controlar la validez
            var isValid = true;

            // Validar txtCodigo
            if (txtCodigo.value.trim() === '') {
                txtCodigo.classList.add('is-invalid');
                txtCodigo.classList.remove('is-valid');
                isValid = false;
            } else {
                txtCodigo.classList.remove('is-invalid');
                txtCodigo.classList.add('is-valid');
            }

            // Validar txtNombre
            if (txtNombre.value.trim() === '') {
                txtNombre.classList.add('is-invalid');
                txtNombre.classList.remove('is-valid');
                isValid = false;
            } else {
                txtNombre.classList.remove('is-invalid');
                txtNombre.classList.add('is-valid');
            }

            // Validar txtDescripcion
            if (txtDescripcion.value.trim() === '') {
                txtDescripcion.classList.add('is-invalid');
                txtDescripcion.classList.remove('is-valid');
                isValid = false;
            } else {
                txtDescripcion.classList.remove('is-invalid');
                txtDescripcion.classList.add('is-valid');
            }

            // Validar ddlCategoria
            if (ddlCategoria.value === '' || ddlCategoria.value === null) {
                ddlCategoria.classList.add('is-invalid');
                ddlCategoria.classList.remove('is-valid');
                isValid = false;
            } else {
                ddlCategoria.classList.remove('is-invalid');
                ddlCategoria.classList.add('is-valid');
            }

            // Validar ddlMarca
            if (ddlMarca.value === '' || ddlMarca.value === null) {
                ddlMarca.classList.add('is-invalid');
                ddlMarca.classList.remove('is-valid');
                isValid = false;
            } else {
                ddlMarca.classList.remove('is-invalid');
                ddlMarca.classList.add('is-valid');
            }

            // Validar txtPrecio
            if (txtPrecio.value.trim() === '') {
                txtPrecio.classList.add('is-invalid');
                txtPrecio.classList.remove('is-valid');
                isValid = false;
            } else {
                txtPrecio.classList.remove('is-invalid');
                txtPrecio.classList.add('is-valid');
            }

            // Devolver el resultado de la validación
            return isValid;
        }
    </script>

    <script type="text/javascript">
        function confirmarEliminacion() {
            Swal.fire({
                title: '¿Está seguro de eliminar?',
                text: "¡No podrá revertir esto!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Sí, eliminarlo!'
            }).then((result) => {
                if (result.isConfirmed) {
                    // Llamar al servidor para realizar la eliminación
                    __doPostBack('<%= btnEliminar.UniqueID %>', '');
                }
            });
        }
    </script>

    <style>
        .group-box {
            border: 1px solid #ddd;
            border-radius: 4px;
            background-color: #f9f9f9;
            padding: 10px;
            margin-top: 20px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }

        .button-group {
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

            .button-group .btn {
                margin: 0 5px; /* Espaciado entre los botones */
            }

            .button-group .btn-primary {
                background-color: #007bff;
                border-color: #007bff;
            }

            .button-group .btn-warning {
                background-color: #ffc107;
                border-color: #ffc107;
            }

                .button-group .btn-primary:hover,
                .button-group .btn-warning:hover {
                    opacity: 0.8; /* Efecto de opacidad al pasar el mouse sobre los botones */
                }
    </style>

    <style>
        .group-box {
            border: 1px solid #ddd;
            border-radius: 4px;
            background-color: #f9f9f9;
            padding: 10px;
            margin-top: 20px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }

        .button-group {
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

            .button-group .btn {
                margin: 0 10px; /* Espaciado entre los botones */
                padding: 10px 20px;
                font-size: 16px;
                border: none;
                border-radius: 4px;
                cursor: pointer;
            }

            .button-group .btn-success {
                background-color: #28a745;
                border-color: #28a745;
            }

            .button-group .btn-danger {
                background-color: #dc3545;
                border-color: #dc3545;
            }

                .button-group .btn-success:hover,
                .button-group .btn-danger:hover {
                    opacity: 0.8; /* Efecto de opacidad al pasar el mouse sobre los botones */
                }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="container-fluid">



        <br />

        <ul class="nav nav-tabs">
            <li class="nav-item">
                <a class="nav-link" style="color: white; background-color: black; border: 2px black; border-bottom-color: black;" aria-current="page">Datos asociados al articulo</a>
            </li>
        </ul>

        <br />

        <div class="row">




            <div class="col-4">



                <div>
                    <label class="form-label">Id:</label>
                    <asp:TextBox ID="txtId" ReadOnly="true" Style="cursor: not-allowed; width: 100px;" class="form-control" runat="server"></asp:TextBox>
                </div>

                <div>
                    <label class="form-label">Codigo:</label>
                    <asp:TextBox ID="txtCodigo" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div>
                    <label class="form-label">Nombre:</label>
                    <asp:TextBox ID="txtNombre" class="form-control" runat="server"></asp:TextBox>
                </div>

                <div>
                    <label class="form-label">Descripcion:</label>
                    <asp:TextBox ID="txtDescripcion" TextMode="MultiLine" class="form-control" runat="server"></asp:TextBox>
                </div>

                <div style="display: flex; align-items: center;">
                    <div style="display: flex; flex-direction: column; width: 50%;">
                        <label class="form-label">Marca:</label>
                        <asp:DropDownList ID="DdlMarca" CssClass="form-select" Style="width: 100%;" runat="server"></asp:DropDownList>
                    </div>
                    <asp:Button ID="btnAgregarMarca" Text="+" class="btn btn-primary" runat="server" Style="margin-left: 10px; margin-top: 30px;" />
                </div>


                <div style="display: flex; align-items: center;">
                    <div style="display: flex; flex-direction: column; width: 50%;">
                        <label class="form-label">Categoria:</label>
                        <asp:DropDownList ID="DdlCategoria" CssClass="form-select" Style="width: 100%;" runat="server"></asp:DropDownList>
                    </div>
                    <asp:Button ID="btnAgregarCategoria" Text="+" class="btn btn-primary" runat="server" Style="margin-left: 10px; margin-top: 30px;" />
                </div>

                <div>
                    <label class="form-label">Precio: ($)</label>
                    <asp:TextBox ID="txtPrecio" class="form-control" runat="server" Style="width: 62%;"></asp:TextBox>
                </div>

                <div class="group-box">
                    <div class="button-group">
                        <asp:Button ID="btnRegresar" Text="Regresar" CssClass="btn btn-primary" runat="server" OnClick="btnRegresar_Click" />
                        <asp:Button ID="btnEliminar" Text="Eliminar" CssClass="btn btn-warning" runat="server" OnClientClick="confirmarEliminacion(); return false;" OnClick="btnEliminar_Click" />
                    </div>
                </div>


            </div>




            <div class="col-4">


                <div class="mb-3">
                    <label class="form-label">Imagen:</label>
                    <input type="file" id="txtImagen" runat="server" class="form-control" onchange="previewImage(this);" style="width: 90%;" />
                </div>

                <asp:Image ID="imgArticulo" runat="server" ImageUrl="https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg"
                    CssClass="fixed-size-img" Style="width: 90%;" onerror="this.onerror=null; this.src='Images/img-nd.jpg'" onchange="previewImage(this);" />

                <br />


                <div class="group-box">
                    <div class="button-group">
                        <asp:Button ID="btnAceptar" Text="Aceptar" CssClass="btn btn-success" runat="server"
                            OnClientClick="return validarFormulario();" OnClick="btnAceptar_Click" />
                        <asp:Button ID="btnCancelar" Text="Cancelar" CssClass="btn btn-danger" runat="server"
                            OnClientClick="confirmarCancelacion(); return false;" />
                    </div>
                </div>




            </div>

        </div>

    </div>

</asp:Content>

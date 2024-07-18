<%@ Page Title="MI CUENTA" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Micuenta.aspx.cs" Inherits="Catalogo_Web.Micuenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>

        function validarDatosUsuario() {

            var nombre = document.getElementById('<%= txtNombre.ClientID %>').value;
            var apellido = document.getElementById('<%= txtApellido.ClientID %>').value;

            var txtNombre = document.getElementById('<%= txtNombre.ClientID %>');
            var txtApellido = document.getElementById('<%= txtApellido.ClientID %>');

            var focusSet = false;

            if (nombre === "") {
                txtNombre.classList.add("is-invalid");
                txtNombre.classList.remove("is-valid");
                if (!focusSet) {
                    txtNombre.focus();
                    focusSet = true;
                }
            } else {
                txtNombre.classList.remove("is-invalid");
                txtNombre.classList.add("is-valid");
            }

            if (apellido === "") {
                txtApellido.classList.add("is-invalid");
                txtApellido.classList.remove("is-valid");
                if (!focusSet) {
                    txtApellido.focus();
                    focusSet = true;
                }
            } else {
                txtApellido.classList.remove("is-invalid");
                txtApellido.classList.add("is-valid");
            }


            if (focusSet) {
                return false;
            }

            return true;

        }


    </script>

    <script>
        function previewImage(input) {
            var imgNuevoPerfil = document.getElementById('<%= imgNuevoPerfil.ClientID %>');
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    imgNuevoPerfil.src = e.target.result;
                }
                reader.readAsDataURL(input.files[0]);
            } else {
                imgNuevoPerfil.src = 'Images/PerfilGral.jpg';
            }
        }
    </script>

    <meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="0" />

    <script type="text/javascript">
        function clearCache() {
            if ('caches' in window) {
                caches.keys().then(function (cacheNames) {
                    cacheNames.forEach(function (cacheName) {
                        caches.delete(cacheName);
                    });
                });
            }
        }

        window.onload = function () {
            clearCache();
        };
    </script>

    <script type="text/javascript">
        function validateAndSubmit() {
            var fileUpload = document.getElementById('<%= txtImagen.ClientID %>');
            var focusSet = false;

            if (fileUpload.value === "") {
                fileUpload.classList.add("is-invalid");
                fileUpload.classList.remove("is-valid");
                if (!focusSet) {
                    fileUpload.focus();
                    focusSet = true;
                }
                return false; // Evita que se ejecute el evento OnClick del botón
            } else {
                fileUpload.classList.remove("is-invalid");
                fileUpload.classList.add("is-valid");
                return true; // Permite que se ejecute el evento OnClick del botón
            }
        }   
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


    <div class="container">
        <div class="row">
            <div class="col-4">
                <br />
                <div class="row">
                    <label class="form-label" style="font-weight: bold; text-align: center; font-size: 20px;">MI CUENTA</label>
                    <asp:Label ID="lblCuenta" class="form-label" Style="color: #4682B4; text-align: center;" runat="server" Text="Agregar nombre de la cuenta del usuario"></asp:Label>
                </div>
                <hr />
                <div class="row">
                    <asp:LinkButton ID="lnkDatosUsuario" Text="Mis datos personales" runat="server" Style="text-decoration: none;" OnClick="lnkDatosUsuario_Click" />
                </div>
                <hr />
                <div class="row">
                    <asp:LinkButton ID="lnkFoto" Text="Mi foto de perfil" runat="server" Style="text-decoration: none;" OnClick="lnkFoto_Click" />
                </div>
                <hr />
                <div class="row">
                    <asp:LinkButton ID="lnkCambiarPass" Text="Cambiar contraseña" runat="server" Style="text-decoration: none;" OnClick="lnkCambiarPass_Click" />
                </div>
                <hr />
                <div class="row">
                    <asp:LinkButton ID="lnkFavoritos" Text="Mis favoritos" runat="server" Style="text-decoration: none;" OnClick="lnkFavoritos_Click" />
                </div>
                <hr />
            </div>

            <div class="col-4">



                <asp:Panel ID="pnlUserData" runat="server" Visible="true">
                    <br />
                    <label class="form-label" style="font-weight: bold; font-size: 18px; display: block; margin: 0 auto; text-align: center;">Mis datos personales</label>
                    <br />
                    <p>Estos son los datos asociados a tu cuenta</p>
                    <div>
                        <label class="form-label" style="font-weight: bold;">NOMBRE:</label>
                        <asp:TextBox ID="txtNombre" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div>
                        <label class="form-label" style="font-weight: bold;">APELLIDO:</label>
                        <asp:TextBox ID="txtApellido" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div>
                        <label class="form-label" style="font-weight: bold;">E-MAIL:</label>
                        <label id="emailErrorLabel" class="form-label" style="font-weight: bold; color: red; font-size: 10px; display: none;">El formato del mail proporcionado no es válido</label>
                        <asp:TextBox ID="txtMail" class="form-control" runat="server" ReadOnly="true" Style="background-color: #f0f0f0; cursor: not-allowed;"></asp:TextBox>
                    </div>
                    <br />
                    <div>
                        <asp:Button ID="btnModificar" Text="Cambiar datos personales" OnClientClick="return validarDatosUsuario();" OnClick="btnModificar_Click" class="btn btn-primary" runat="server" />
                    </div>
                </asp:Panel>


                <asp:Panel ID="pnlChangePassword" runat="server" Visible="false">
                    <br />
                    <label class="form-label" style="font-weight: bold; font-size: 18px; display: block; margin: 0 auto; text-align: center;">Cambiar contraseña</label>
                    <br />
                    <p>Ingresa tu contraseña actual para registrar una nueva</p>
                    <div>
                        <label class="form-label" style="font-weight: bold;">Contraseña actual:</label>
                        <asp:TextBox ID="txtCurrentPassword" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div>
                        <label class="form-label" style="font-weight: bold;">Nueva contraseña:</label>
                        <asp:TextBox ID="txtNewPassword" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div>
                        <label class="form-label" style="font-weight: bold;">Confirmar nueva contraseña:</label>
                        <asp:TextBox ID="txtConfirmPassword" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <br />
                    <div>
                        <asp:Button ID="btnChangePassword" Text="Cambiar contraseña" class="btn btn-primary" runat="server" OnClick="btnChangePassword_Click" />
                    </div>
                </asp:Panel>



















                <asp:Panel ID="pnlProfilePhoto" runat="server" Visible="false">
                    <br />
                    <label class="form-label" style="font-weight: bold; font-size: 18px; display: block; margin: 0 auto; text-align: center;">Mi foto de perfil</label>
                    <br />
                    <p>Aquí puedes cambiar tu foto de perfil</p>

                    <div class="mb-3">
                        <label class="form-label">Imagen de perfil</label>
                        <input type="file" id="txtImagen" runat="server" class="form-control" onchange="previewImage(this);" />
                    </div>

                    <asp:Image ID="imgNuevoPerfil" ImageUrl="https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg"
                        runat="server" CssClass="img-fluid mb-3" />

                    <div>
                        <br />
                        <asp:Button ID="btnCambiarImagen" Text="Cambiar imagen" class="btn btn-primary" runat="server" OnClientClick="return validateAndSubmit();"
                            OnClick="btnCambiarImagen_Click" />
                    </div>
                </asp:Panel>












                <asp:Panel ID="pnlFavorites" runat="server" Visible="false">

                    <br />
                    <label class="form-label" style="font-weight: bold; font-size: 18px; display: block; margin: 0 auto; text-align: center;">Mis favoritos</label>
                    <br />
                    <p>Aquí puedes ver tus favoritos</p>
                    <!-- Más contenido relacionado con favoritos -->
                </asp:Panel>


            </div>
        </div>
    </div>


</asp:Content>

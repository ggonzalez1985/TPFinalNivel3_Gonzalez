<%@ Page Title="Formulario de Login & Registro" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Catalogo_Web.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <style>
        body {
            background-color: lightgray;
            height: 100vh;
            width: 100%;
        }

        }
    </style>
    <style>
        .label-small-celeste {
            font-size: 12px; /* Ajusta el tamaño del texto según sea necesario */
            color: red; /* Color celeste */
            display: block; /* Asegura que el label sea un bloque para alinear correctamente */
            text-align: right; /* Alineación a la derecha */
            position: relative;
            top: 10px; /* Ajusta según sea necesario para alinear hacia abajo */
            font-weight: bold;
        }
    </style>
    <script>
        function validarTerrenos() {
            var nombre = document.getElementById("<%= txtNombre.ClientID %>").value.trim();
            var apellido = document.getElementById("<%= txtApellido.ClientID %>").value.trim();
            var email = document.getElementById("<%= txtEmail.ClientID %>").value.trim();
            var pass = document.getElementById("<%= txtPass.ClientID %>").value.trim();
            var passConfirmar = document.getElementById("<%= txtPassConfirmar.ClientID %>").value.trim();

            var txtNombre = document.getElementById("<%= txtNombre.ClientID %>");
            var txtApellido = document.getElementById("<%= txtApellido.ClientID %>");
            var txtEmail = document.getElementById("<%= txtEmail.ClientID %>");
            var txtPass = document.getElementById("<%= txtPass.ClientID %>");
            var txtPassConfirmar = document.getElementById("<%= txtPassConfirmar.ClientID %>");
            var lblPassDistintas = document.getElementById("lblPassDistintas");
            var focusSet = false;

            var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

            // Reinicia el estado de visibilidad del mensaje de error
            lblPassDistintas.style.display = "none";

            // Validación del nombre
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

            // Validación del apellido
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

            // Validación del email
            if (email === "" || !emailPattern.test(email)) {
                txtEmail.classList.add("is-invalid");
                txtEmail.classList.remove("is-valid");
                if (!focusSet) {
                    txtEmail.focus();
                    focusSet = true;
                }
            } else {
                txtEmail.classList.remove("is-invalid");
                txtEmail.classList.add("is-valid");
            }

            // Validación de la contraseña
            if (pass === "") {
                txtPass.classList.add("is-invalid");
                txtPass.classList.remove("is-valid");
                if (!focusSet) {
                    txtPass.focus();
                    focusSet = true;
                }
            } else {
                txtPass.classList.remove("is-invalid");
                txtPass.classList.add("is-valid");
            }

            // Validación de la confirmación de contraseña
            if (passConfirmar === "") {
                txtPassConfirmar.classList.add("is-invalid");
                txtPassConfirmar.classList.remove("is-valid");
                if (!focusSet) {
                    txtPassConfirmar.focus();
                    focusSet = true;
                }
            } else if (pass !== passConfirmar) {
                txtPassConfirmar.classList.add("is-invalid");
                txtPassConfirmar.classList.remove("is-valid");
                if (!focusSet) {
                    txtPassConfirmar.focus();
                    focusSet = true;
                }
                lblPassDistintas.style.display = "block"; // Muestra el mensaje de contraseñas no coinciden
            } else {
                txtPassConfirmar.classList.remove("is-invalid");
                txtPassConfirmar.classList.add("is-valid");
            }

            return !focusSet; // Return false if focus was set, true otherwise
        }

    </script>
    <script>
        function validarCamposLogin() {
            var email = document.getElementById("<%= txtEmailLogin.ClientID %>").value.trim();
            var pass = document.getElementById("<%= txtPassLogin.ClientID %>").value.trim();

            var txtEmailLogin = document.getElementById("<%= txtEmailLogin.ClientID %>");
            var txtPassLogin = document.getElementById("<%= txtPassLogin.ClientID %>");
            var focusSet = false;

            var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

            if (email === "" || !emailPattern.test(email)) {
                txtEmailLogin.classList.add("is-invalid");
                txtEmailLogin.classList.remove("is-valid");
                if (!focusSet) {
                    txtEmailLogin.focus();
                    focusSet = true;
                }
            } else {
                txtEmailLogin.classList.remove("is-invalid");
                txtEmailLogin.classList.add("is-valid");
            }

            if (pass === "") {
                txtPassLogin.classList.add("is-invalid");
                txtPassLogin.classList.remove("is-valid");
                if (!focusSet) {
                    txtPassLogin.focus();
                    focusSet = true;
                }
            } else {
                txtPassLogin.classList.remove("is-invalid");
                txtPassLogin.classList.add("is-valid");
            }

            // Devuelve false si hay un error para evitar el envío del formulario
            return !focusSet;
        }
</script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="container-fluid">

        <div class="row ">

            <div class="col">
                <div class="card mx-auto shadow-5-strong" style="margin-top: 50px; backdrop-filter: blur(30px); border: 1px solid black; background-color: lightgray; width: 80%;">
                    <div class="card-body py-2 px-md-5">
                        <h2 class="fw-bold mb-5" style="text-align: center">¿Tenes una cuenta?</h2>
                        <div class="form-outline mb-2">
                            <asp:TextBox ID="txtEmailLogin" type="email" class="form-control" runat="server"></asp:TextBox>
                            <label class="form-label" for="txtEmailLogin">Ingresa tu E-mail</label>
                        </div>
                        <div class="form-outline mb-4">
                            <asp:TextBox ID="txtPassLogin" runat="server" type="password" class="form-control"></asp:TextBox>
                            <label class="form-label" for="txtPassLogin">Ingresa tu contraseña</label>
                        </div>
                    </div>
                </div>
                <br />
                <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" type="submit" CssClass="btn btn-primary mb-3 mx-auto"
                    Style="display: block; border: 2px solid black;" OnClientClick="return validarCamposLogin();"
                    OnClick="btnIngresar_Click" />
            </div>

            <div class="col-7">

                <div class="card mx-auto shadow-5-strong"
                    style="margin-top: 50px; backdrop-filter: blur(30px); border: 1px solid black; background-color: lightgray; width: 80%;">

                    <div class="card-body py-2 px-md-5">

                        <h2 class="fw-bold mb-5" style="text-align: center">Crea tu cuenta y compra desde donde estes</h2>

                        <div class="row">
                            <p><strong>DATOS PERSONALES</strong></p>
                            <div class="col-md-6 mb-2">
                                <div class="form-outline">
                                    <asp:TextBox ID="txtNombre" type="text" class="form-control" runat="server"></asp:TextBox>
                                    <label class="form-label" for="txtNombre">Tu nombre*</label>
                                </div>
                            </div>
                            <div class="col-md-6 mb-2">
                                <div class="form-outline">
                                    <asp:TextBox ID="txtApellido" type="text" class="form-control" runat="server"></asp:TextBox>
                                    <label class="form-label" for="txtApellido">Tu apellido*</label>
                                </div>
                            </div>
                        </div>

                        <p><strong>DATOS DE ACCESO</strong></p>

                        <div class="form-outline mb-2">
                            <asp:TextBox ID="txtEmail" type="email" class="form-control" runat="server"></asp:TextBox>
                            <label class="form-label" for="txtEmail">E-mail*</label>
                        </div>
                        <div class="form-outline mb-4">
                            <asp:TextBox ID="txtPass" runat="server" type="password" class="form-control"></asp:TextBox>
                            <label class="form-label" for="txtPass">Contraseña*</label>
                        </div>


                        <div class="form-outline mb-4">
                            <asp:TextBox ID="txtPassConfirmar" runat="server" type="password" class="form-control"></asp:TextBox>

                            <div class="row ">

                                <div class="col -6">
                                    <label class="form-label" for="txtPassConfirmar">Confirma tu contraseña*</label>
                                </div>

                                <div class="col -6">
                                    <label id="lblPassDistintas" style="display: none; color: #FF5733;">Las contraseñas ingresadas no coinciden</label>
                                </div>

                            </div>

                            <label class="form-label label-small-celeste" for="txtPass">* Campos Obligatorios</label>
                        </div>



                    </div>
                </div>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <asp:Button ID="btnRegistrarse" runat="server" Text="Crear cuenta y continuar"
                    type="submit" CssClass="btn btn-primary mb-3 "
                    Style="display: block; margin: 20px; border: 2px solid black; margin-left: 980px;"
                    OnClientClick="return validarTerrenos()" OnClick="btnRegistrarse_Click" />

            </ContentTemplate>
        </asp:UpdatePanel>

    </div>

</asp:Content>

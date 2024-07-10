<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Catalogo_Web.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <style>
        body {
            background-color: lightgray;
            height: 100vh;
            width: 100%;
        }

        }
    </style>
    <script>
        function validarTerrenos() {
            var nombre = document.getElementById("<%= txtNombre.ClientID %>").value.trim();
            var apellido = document.getElementById("<%= txtApellido.ClientID %>").value.trim();
            var email = document.getElementById("<%= txtEmail.ClientID %>").value.trim();
            var pass = document.getElementById("<%= txtPass.ClientID %>").value.trim();

            var txtNombre = document.getElementById("<%= txtNombre.ClientID %>");
            var txtApellido = document.getElementById("<%= txtApellido.ClientID %>");
            var txtEmail = document.getElementById("<%= txtEmail.ClientID %>");
            var txtPass = document.getElementById("<%= txtPass.ClientID %>");
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

            if (email === "") {
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

            return !focusSet; // Return false if focus was set, true otherwise
        }
</script>


</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="container">

        <div class="card mx-auto shadow-5-strong"
            style="margin-top: 50px; backdrop-filter: blur(30px); border: 1px solid black; background-color: lightgray; width: 80%;">

            <div class="card-body py-2 px-md-5">
                <div class="row d-flex justify-content-center">
                    <div class="col-lg-6">
                        <h2 class="fw-bold mb-5" style="text-align: center">Sign Up!!!</h2>

                        <div class="row">
                            <div class="col-md-6 mb-2">
                                <div class="form-outline">
                                    <asp:TextBox ID="txtNombre" ClientIDMode="Static" type="text" class="form-control" runat="server"></asp:TextBox>
                                    <label class="form-label" for="txtNombre">Nombre</label>
                                </div>
                            </div>
                            <div class="col-md-6 mb-2">
                                <div class="form-outline">

                                    <asp:TextBox ID="txtApellido" ClientIDMode="Static" type="text" class="form-control" runat="server"></asp:TextBox>
                                    <label class="form-label" for="txtApellido">Apellido</label>

                                </div>
                            </div>
                        </div>
                        <div class="form-outline mb-2">

                            <asp:TextBox ID="txtEmail" ClientIDMode="Static" type="email" class="form-control" runat="server"></asp:TextBox>
                            <label class="form-label" for="txtEmail">Email</label>

                        </div>

                        <div class="form-outline mb-4">

                            <asp:TextBox ID="txtPass" ClientIDMode="Static" runat="server" type="password" class="form-control"></asp:TextBox>
                            <label class="form-label" for="txtPass">Contraseña</label>

                        </div>


                    </div>
                </div>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <asp:Button ID="btnRegistrarse" runat="server" Text="Registrarse"
                    type="submit" CssClass="btn btn-block mb-4"
                    Style="display: block; margin: 20px; border: 2px solid black; margin-left: 700px;"
                    OnClientClick="return validarTerrenos()" OnClick="btnRegistrarse_Click" />

            </ContentTemplate>
        </asp:UpdatePanel>

    </div>

</asp:Content>

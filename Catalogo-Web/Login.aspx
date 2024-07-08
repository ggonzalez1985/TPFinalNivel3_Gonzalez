<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Catalogo_Web.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


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

            if (nombre === "" || apellido === "" || email === "" || pass === "") {
                alert("Por favor, completa todos los campos.");
                return false;
            } else {
                return true;
            }
        }

    </script>


</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

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

    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:Button ID="btnRegistrarse" runat="server" Text="Registrarse"
                type="submit" CssClass="btn btn-block mb-4"
                Style="display: block; margin: 20px; border: 2px solid black; margin-left: 700px;"
                data-bs-toggle="modal" data-bs-target="#staticBackdrop" OnClientClick="return validarTerrenos()" OnClick="btnRegistrarse_Click" />

    <%if (ConfirmaRegistracion)
        { %>

    <div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false"
        tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5 text-center" id="staticBackdropLabel">Registracion</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body text-center">
                    Usuario registrado con éxito! ✅
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnRegistroOk" runat="server" Text="Aceptar" OnClick="btnRegistroOk_Click" type="button"
                        CssClass="btn btn-primary" data-bs-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

    <%} %>

</ContentTemplate>
    </asp:UpdatePanel>--%>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnRegistrarse" runat="server" Text="Registrarse"
                type="submit" CssClass="btn btn-block mb-4"
                Style="display: block; margin: 20px; border: 2px solid black; margin-left: 700px;"
                OnClientClick="return validarTerrenos()" OnClick="btnRegistrarse_Click" />

            <% if (ConfirmaRegistracion)
                { %>
            <script type="text/javascript">
                $(document).ready(function () {
                    $('#staticBackdrop').modal('show');
                });
            </script>
            <% } %>

            <div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false"
                tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h1 class="modal-title fs-5 text-center" id="staticBackdropLabel">Registracion</h1>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body text-center">
                            Usuario registrado con éxito! ✅
                   
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnRegistroOk" runat="server" Text="Aceptar" OnClick="btnRegistroOk_Click" type="button"
                                CssClass="btn btn-primary" data-bs-dismiss="modal" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Catalogo_Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="container">
        <div class="row">
            <div class="col text-start" style="margin-top: 500px;">
                <ul class="nav nav-underline flex-grow-1">
                    <li class="nav-item" style="margin-left: 40px;">
                        <a class="nav-link" href="#">Ver Catalogo</a>
                    </li>
                </ul>
            </div>
        </div>
    </div>

    <style>
        body {
            background-image: url('Images/BraviaFondo.jpg');
            background-size: cover;
            background-position: top left;
            background-attachment: fixed;
            height: 100vh;
            width: 100vw;
        }
    </style>
</asp:Content>







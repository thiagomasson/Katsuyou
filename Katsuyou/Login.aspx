<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Katsuyou.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">
    <div class="row margin-top-60">
        <div class="col-3">
            <div class="box border">
                <h1>Login</h1>
                <asp:Label ID="txtMsg" runat="server"></asp:Label>
                <br />
                <label>Username</label>
                <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                <label>Password</label>
                <asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="btnLogin" OnClick="btnLogin_Click" runat="server" Text="Login" />
                <br />
            </div>
        </div>
    </div>
</asp:Content>

<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="WIF_Claims.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <asp:Button ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" />
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
</asp:Content>

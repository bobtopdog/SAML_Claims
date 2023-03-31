<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WIF_Claims._Default" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <p>1 <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" /></p>
   <p>2.  <asp:Label ID="username1" runat="server" Text=""></asp:Label></p>
    <p>3. UserName <asp:Label ID="Label1" runat="server" Text=""></asp:Label></p>
    <p>4 <asp:Label ID="Label2" runat="server" Text=""></asp:Label></p>
    <p>5. 
        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></p>
     <p>6. 
        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label></p>
     <p>7. 
        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label></p>
     <p>8. 
        <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label></p>
    <p>9. getNameID 
        <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label></p>

    <p>10.Admin:  
        <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label></p>

    <asp:PlaceHolder ID="PlaceHolder1" runat="server">Is In Admin</asp:PlaceHolder>

    <hr />

   IsInClaimsAdmin <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>

    <a runat="server" href="~/error">Follow the white Rabit</a>

</asp:Content>

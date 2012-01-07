<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>
        Administration</h2>
    <p>
        Please select from one of the following options:</p>
    <ul>
        <li><asp:HyperLink runat="server" ID="lnkManageGenres" NavigateUrl="~/Admin/ManageGenres.aspx">Manage Genres</asp:HyperLink></li>
        <li><asp:HyperLink runat="server" ID="lnkManageAuthors" NavigateUrl="~/Admin/ManageAuthors.aspx">Manage Authors</asp:HyperLink></li>
        <li><asp:HyperLink runat="server" ID="lnkManageReviews" NavigateUrl="~/Admin/ManageReviews.aspx">Manage Reviews</asp:HyperLink></li>
    </ul>
</asp:Content>

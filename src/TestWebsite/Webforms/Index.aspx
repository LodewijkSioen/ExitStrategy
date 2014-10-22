<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="TestWebsite.Webforms.Index" MasterPageFile="~/Layout.Master" Title="This is a Webforms page" %>
<%@ Register TagPrefix="mvc" Namespace="ExitStrategy.ForWebforms" Assembly="ExitStrategy.ForWebforms" %>

<asp:Content ContentPlaceHolderID="Body" runat="server">
    <h2>This is a Webforms Page</h2>
    <mvc:Partial Model="<%$ l:() => View %>" PartialViewName="ViewModel" runat="server" />
    <p>
        <asp:Button runat="server" Text="Postback to webforms" OnClick="WebFormsClick" />
    </p>
    <p>
        <asp:ValidationSummary runat="server" ShowModelStateErrors="True"/>
    </p>
</asp:Content>
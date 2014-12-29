<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ExitStrategy.TestWebsite.Webforms.ModelBinding.Index" MasterPageFile="~/Webforms/Layout.Master" Title="Webforms Modelbinding" %>

<asp:Content ContentPlaceHolderID="Body" runat="server">
<div class="webforms">
    <h1>Webforms Modelbinding list</h1>
    <p>
        This is a Webforms page that uses a Razor DisplayTemplate to show a property defined in the codebehind.
    </p>
    <div class="mvc">
        <mvc:Display SelectMethod="GetPersons" TemplateName="Table" runat="server" />
    </div>
</div>
</asp:Content>
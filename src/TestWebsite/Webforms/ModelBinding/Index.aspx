<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="TestWebsite.Webforms.ModelBinding.Index" MasterPageFile="~/Webforms/Layout.Master" Title="Webforms Modelbinding" %>
<%@ Register TagPrefix="mvc" Namespace="ExitStrategy.ForWebforms" Assembly="ExitStrategy.ForWebforms" %>

<asp:Content ContentPlaceHolderID="Body" runat="server">
<div class="webforms">
    <h1>Webforms Modelbinding list</h1>
    <p>
        This is a Webforms page that uses a Razor DisplayTemplate to show a property defined in the codebehind.
    </p>
    <div class="mvc">
        <mvc:Display Model="<%$l:() => Model %>" TemplateName="Table" runat="server" />
    </div>
</div>
</asp:Content>
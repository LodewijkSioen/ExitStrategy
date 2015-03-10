<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ExitStrategy.TestWebsite.Webforms.ModelBinding.Index" MasterPageFile="~/Webforms/Layout.Master" Title="Webforms Modelbinding" %>

<asp:Content ContentPlaceHolderID="Body" runat="server">
<div class="webforms">
    <h1>Webforms Modelbinding list</h1>
    <p>
        This is a Webforms page that uses a Razor DisplayTemplate to show a property defined in the codebehind.<br />
        This page uses an &lt;mcv:Display /&gt; control, which can be used in two ways:
    </p>
    <ul>
        <li><asp:HyperLink NavigateUrl="<%$RouteUrl:routename=Webforms-ModelBinding%>" runat="server">As a top-level control using ModelBinding</asp:HyperLink> (current page)</li>
        <li><asp:HyperLink NavigateUrl="<%$RouteUrl:routename=Webforms-ModelBinding-listview%>" runat="server">As a nested control within an &lt;asp:ListView /&gt;</asp:HyperLink> that also supports adding new items</li>
    </ul>
    <div class="mvc">
        <mvc:Display SelectMethod="GetPersons" TemplateName="Table" runat="server" />
    </div>
</div>
</asp:Content>
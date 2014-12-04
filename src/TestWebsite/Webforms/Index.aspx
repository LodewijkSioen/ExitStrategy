<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="TestWebsite.Webforms.Index" MasterPageFile="~/Webforms/Layout.Master" Title="This is a Webforms page" %>

<asp:Content ContentPlaceHolderID="Body" runat="server">
<div class="webforms">
    <h1>This is a Webforms Page</h1>
    <p>
        This is a simple webforms page with a masterpage. However, the header and the footer on the masterpage are two razor partial views.
        These are shared with the layout that is used on the mcv page. This way, every page can have the same layout with minimal duplication.        
    </p>
    <p>
        For a more complicated example, look at <asp:HyperLink NavigateUrl="<%$ RouteUrl:routename=Webforms-ModelBinding%>" runat="server">ModelBinding</asp:HyperLink>.
    </p>
</div>
</asp:Content>
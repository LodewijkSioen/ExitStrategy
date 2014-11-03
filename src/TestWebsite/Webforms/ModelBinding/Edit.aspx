<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="TestWebsite.Webforms.ModelBinding.Edit" MasterPageFile="~/Webforms/Layout.Master" Title="Webforms Modelbinding Edit" %>
<%@ Register TagPrefix="mvc" Namespace="ExitStrategy.ForWebforms" Assembly="ExitStrategy.ForWebforms" %>
<asp:Content ContentPlaceHolderID="Body" runat="server">
<div class="webforms">
    <h1>Webforms Modelbinding Editor</h1>
    <p>
        This is a webforms page with the same Razor template from the Mvc Editor sample. You can post back to a codebehind method. 
        Leaving First Name or Last Name blank will trigger the validation in Webforms and the result will be shown on the Razor template.
    </p>
    <div class="form-horizontal">
        <div class="mvc">
            <mvc:Editor SelectMethod="GetModel" AdditionalViewData='<%$l:()=> new {htmlAttributes = new {@class = "form-control"},} %>' runat="server" />
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:Button Text="Edit" CssClass="btn btn-primary" OnClick="Button_Click" runat="server" />
                <asp:HyperLink NavigateUrl="<%$RouteUrl:routename=Webforms-Modelbinding%>" Text="Cancel" CssClass="btn btn-link" runat="server" />
            </div>
        </div>
    </div>
</div>
</asp:Content>
﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="TestWebsite.Webforms.ModelBinding.Edit" MasterPageFile="~/Webforms/Layout.Master" Title="Webforms Modelbinding Edit" %>
<%@ Register TagPrefix="mvc" Namespace="ExitStrategy.ForWebforms" Assembly="ExitStrategy.ForWebforms" %>
<asp:Content ContentPlaceHolderID="Body" runat="server">
<div class="webforms">
    <h1>Webforms Modelbinding Editor</h1>
    <p>
        This is a webforms page with the same Razor template from the Mvc Editor sample. You can post back to a codebehind method. 
        Leaving First Name or Last Name blank will trigger the validation in Webforms and the result will be shown on the Razor template.
    </p>
    <asp:Panel runat="server" ID="FormPanel">
    <div class="form-horizontal">
        <div class="mvc">
            <mvc:Editor ID="ModelBoundEditor" SelectMethod="GetModel" AdditionalViewData='<%$Object:new {htmlAttributes = new {@class = "form-control"},} %>' runat="server" />
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:Button Text="Edit" CssClass="btn btn-primary" OnClick="Button_Click" runat="server" />
                <asp:HyperLink NavigateUrl="<%$RouteUrl:routename=Webforms-Modelbinding%>" Text="Cancel" CssClass="btn btn-link" runat="server" />
                <div class="checkbox-inline">
                    <label for="disableValidation">
                        <input type="checkbox" id="disableValidation" /> Disable client-side validation
                    </label>
                </div>
            </div>
        </div>
    </div>
    
    <asp:FormView ID="FormView1" runat="server" SelectMethod="GetModel" UpdateMethod="SetModel" DefaultMode="Edit" RenderOuterTable="false" EnableViewState="False">
        <EditItemTemplate>
            <div class="form-horizontal">
                <mvc:Editor ID="FormViewEditor" runat="server" DataSource="<%# Container.DataItem %>" AdditionalViewData='<%$Object:new {htmlAttributes = new {@class = "form-control"},} %>'/>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" CommandName="Update" Text="Edit"/>
                        <asp:HyperLink ID="HyperLink1" NavigateUrl="<%$RouteUrl:routename=Webforms-Modelbinding%>" Text="Cancel" CssClass="btn btn-link" runat="server" />
                        <div class="checkbox-inline">
                            <label for="disableValidation">
                                <input type="checkbox" id="disableValidation" /> Disable client-side validation
                            </label>
                        </div>
                    </div>
                </div>
            </div>
        </EditItemTemplate>
    </asp:FormView>
    </asp:Panel>
    <asp:Panel runat="server" ID="ResultPanel" Visible="false">
        <p>These are the values you posted to the form:</p>
        <div class="form-horizontal">
        <div class="mvc">
        <mvc:Display runat="server" ID="ResultDisplay"/>
        </div>
        </div>
    </asp:Panel>
</div>
</asp:Content>
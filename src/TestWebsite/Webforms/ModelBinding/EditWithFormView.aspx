<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditWithFormView.aspx.cs" Inherits="ExitStrategy.TestWebsite.Webforms.ModelBinding.EditWithFormView" MasterPageFile="~/Webforms/Layout.Master" Title="Webforms Modelbinding Edit With FormView" %>
<asp:Content ContentPlaceHolderID="Body" runat="server">
    <h1>Webforms Modelbinding Editor</h1>
    <p>
        This is a webforms page with the same Razor template from the Mvc Editor sample. You can post back to a codebehind method.
        Leaving First Name or Last Name blank will trigger the validation in Webforms and the result will be shown on the Razor template.<br />
        This page uses an &lt;mcv:Editor /&gt; control, which can be used in two ways:
    </p>
    <ul>
        <li><asp:HyperLink NavigateUrl="<%$RouteUrl:routename=Webforms-Modelbinding-edit%>" runat="server">As a top-level control using ModelBinding</asp:HyperLink></li>
        <li><asp:HyperLink NavigateUrl="<%$RouteUrl:routename=Webforms-Modelbinding-edit-formview%>" runat="server">As a nested control within an &lt;asp:FormView /&gt; (current page)</asp:HyperLink></li>
    </ul>

    <asp:Panel runat="server" ID="ValidationSummary" ClientIDMode="Static" CssClass="panel panel-danger" Visible="false">
        <div class="panel-heading">Your input is not valid:</div>
        <div class="panel-body">
            <asp:ValidationSummary runat="server" />
        </div>
    </asp:Panel>

    <asp:FormView ID="FormView" runat="server" SelectMethod="GetModel" UpdateMethod="SetModel" DefaultMode="Edit" RenderOuterTable="false" EnableViewState="False">
        <EditItemTemplate>
            <div class="form-horizontal">
                <div class="mvc">
                    <mvc:Editor ID="FormViewEditor" runat="server" DataField="BirthDate" AdditionalViewData='<%$new: {htmlAttributes = new {@class = "form-control"},} %>'/>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <asp:Button CssClass="btn btn-primary" runat="server" CommandName="Update" Text="Edit"/>
                        <asp:HyperLink NavigateUrl="<%$RouteUrl:routename=Webforms-Modelbinding%>" Text="Cancel" CssClass="btn btn-link" runat="server" />
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

    <asp:Panel runat="server" ID="ResultPanel" Visible="false">
        <p>These are the values you posted to the form:</p>
        <div class="form-horizontal">
        <div class="mvc">
            <mvc:Display runat="server" ID="ResultDisplay"/>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:HyperLink NavigateUrl="<%$RouteUrl:routename=Webforms-Modelbinding%>" Text="&#8592; Back to list" CssClass="btn btn-link" runat="server" />
            </div>
        </div>
        </div>
    </asp:Panel>
</asp:Content>
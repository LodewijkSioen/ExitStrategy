<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndexWithListView.aspx.cs" Inherits="ExitStrategy.TestWebsite.Webforms.ModelBinding.IndexWithListView" MasterPageFile="~/Webforms/Layout.Master" Title="Webforms Modelbinding" %>

<asp:Content ContentPlaceHolderID="Body" runat="server">
<div class="webforms">
    <h1>Webforms Modelbinding list</h1>
    <p>
        This is a Webforms page that uses a Razor DisplayTemplate to show a property defined in the codebehind.
    </p>
    <asp:ListView runat="server" SelectMethod="GetPersons" UpdateMethod="UpdatePerson">
        <LayoutTemplate>
            <table class="table table-condensed table-striped table-hover">
                <thead>
                    <tr>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server"></tr>
                </tbody>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td><mvc:Display DataField="FirstName" runat="server" /></td>
                <td><mvc:Display DataField="LastName" runat="server" /></td>
                <td><mvc:Display DataField="EditLink" runat="server" /></td>
                <td><asp:LinkButton runat="server" Text="Edit Inline" CommandName="Edit" /></td>
            </tr>
        </ItemTemplate>
        <EditItemTemplate>
            <tr>
                <td><mvc:Editor DataField="FirstName" AdditionalViewData='<%$new: {htmlAttributes = new {@class = "form-control"},} %>' runat="server" /></td>
                <td><mvc:Editor DataField="LastName" AdditionalViewData='<%$new: {htmlAttributes = new {@class = "form-control"},} %>' runat="server" /></td>
                <td>
                    <asp:Button runat="server" Text="Update" CommandName="Update" CssClass="btn btn-primary btn-sm" />
                    <input type="checkbox" id="disableValidation" title="Disable client-side validation" />
                </td>
                <td><asp:LinkButton runat="server" Text="Cancel" CommandName="cancel" CssClass="btn btn-link" /></td>
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <tr>
                <td colspan="3">No items in collection</td>
            </tr>
        </EmptyDataTemplate>
    </asp:ListView>
</div>
</asp:Content>
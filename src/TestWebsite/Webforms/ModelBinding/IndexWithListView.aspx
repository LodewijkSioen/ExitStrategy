<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndexWithListView.aspx.cs" Inherits="ExitStrategy.TestWebsite.Webforms.ModelBinding.IndexWithListView" MasterPageFile="~/Webforms/Layout.Master" Title="Webforms Modelbinding" %>

<asp:Content ContentPlaceHolderID="Body" runat="server">
<div class="webforms">
    <h1>Webforms Modelbinding list</h1>
    <p>
        This is a Webforms page that uses a Razor DisplayTemplate to show a property defined in the codebehind.
    </p>
    <asp:ListView ID="List" runat="server" EnableViewState="False" 
        SelectMethod="GetPersons" 
        UpdateMethod="UpdatePerson" 
        InsertMethod="InsertPerson"
        OnItemCommand="ListItemCommand">
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
                <tfoot>
                    <tr>
                        <td colspan="4">
                            <asp:LinkButton runat="server" CommandName="InitInsert" Text="Add new" CssClass="btn btn-link" />
                        </td>
                    </tr>
                </tfoot>
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
                <td><asp:LinkButton runat="server" Text="Cancel" CommandName="Cancel" CssClass="btn btn-link" /></td>
            </tr>
        </EditItemTemplate>
        <InsertItemTemplate>
            <tr>
                <td><mvc:Editor DataField="FirstName" AdditionalViewData='<%$new: {htmlAttributes = new {@class = "form-control"},} %>' runat="server" /></td>
                <td><mvc:Editor DataField="LastName" AdditionalViewData='<%$new: {htmlAttributes = new {@class = "form-control"},} %>' runat="server" /></td>
                <td>
                    <asp:Button runat="server" Text="Insert" CommandName="Insert" CssClass="btn btn-primary btn-sm" />
                    <input type="checkbox" id="disableValidation" title="Disable client-side validation" />
                </td>
                <td><asp:LinkButton runat="server" Text="Cancel" CommandName="Cancel" CssClass="btn btn-link" /></td>
            </tr>
        </InsertItemTemplate>
        <EmptyDataTemplate>
            <tr>
                <td colspan="4">No items in collection</td>
            </tr>
        </EmptyDataTemplate>
    </asp:ListView>
</div>
</asp:Content>
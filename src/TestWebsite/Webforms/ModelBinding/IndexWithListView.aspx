<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndexWithListView.aspx.cs" Inherits="ExitStrategy.TestWebsite.Webforms.ModelBinding.IndexWithListView" MasterPageFile="~/Webforms/Layout.Master" Title="Webforms Modelbinding" %>

<asp:Content ContentPlaceHolderID="Body" runat="server">
<div class="webforms">
    <h1>Webforms Modelbinding list</h1>
    <p>
        This is a Webforms page that uses a Razor DisplayTemplate to show a property defined in the codebehind.This page uses an &lt;mcv:Display /&gt; control, which can be used in two ways:
    </p>
    <ul>
        <li><asp:HyperLink NavigateUrl="<%$RouteUrl:routename=Webforms-ModelBinding%>" runat="server">As a top-level control using ModelBinding</asp:HyperLink></li>
        <li><asp:HyperLink NavigateUrl="<%$RouteUrl:routename=Webforms-ModelBinding-listview%>" runat="server">As a nested control within an &lt;asp:ListView /&gt;</asp:HyperLink> that also supports adding new items (current page)</li>
    </ul>
    
    <asp:Panel runat="server" ID="ValidationSummary" ClientIDMode="Static" CssClass="panel panel-danger" Visible="false">
        <div class="panel-heading">Your input is not valid:</div>
        <div class="panel-body">
            <asp:ValidationSummary runat="server" />
        </div>
    </asp:Panel>

    <asp:Panel runat="server" ID="AdmitDefeat" CssClass="panel panel-warning" Visible="false">
        <div class="panel-heading">Help Me!</div>
        <div class="panel-body">
            This is one thing that I cannot get working. When used in a Modelbound Formview in Insert Mode, the Editor does not use the correct Shared View. 
            As you can see, the birthdate is not using the DatePicker, the Enum is not a list of radiobuttons,...<br />
            I don't find what's different about this use-case. If you can find what's wrong here, feel free to send me a Pull Request for <a href="https://github.com/LodewijkSioen/ExitStrategy/issues/8" target="_blank">issue #8 on Github</a>.
        </div>
    </asp:Panel>

    <asp:ListView ID="List" runat="server" EnableViewState="False"
        ItemType="ExitStrategy.TestWebsite.Models.PersonListItem"
        SelectMethod="GetPersons" 
        UpdateMethod="UpdatePerson" 
        InsertMethod="InsertPerson">
        <LayoutTemplate>
            <table class="table table-condensed table-striped table-hover">
                <thead>
                    <tr>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Birthdate</th>
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
                            <asp:HyperLink runat="server" NavigateUrl="<%$RouteUrl:routename=Webforms-Modelbinding-ListView,Mode=Insert%>" Text="Add new inline" CssClass="btn btn-link" />
                            <asp:HyperLink runat="server" NavigateUrl="<%$RouteUrl:routename=Webforms-Modelbinding-Insert%>" Text="Add new" CssClass="btn btn-link" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td class="mvc"><mvc:Display DataField="FirstName" runat="server" /></td>
                <td class="mvc"><mvc:Display DataField="LastName" runat="server" /></td>
                <td class="mvc"><mvc:Display DataField="BirthDate" runat="server" /></td>
                <td class="mvc"><mvc:Display DataField="EditLink" runat="server" /></td>
                <td><asp:LinkButton runat="server" Text="Edit Inline" CommandName="Edit" /></td>
            </tr>
        </ItemTemplate>
        <EditItemTemplate>
            <tr>
                <td class="mvc<%= ModelState.IsValidField("FirstName") ? "" : " has-error" %>">
                    <mvc:Editor ID="FirstNameEditor" DataField="FirstName" AdditionalViewData='<%$new: {htmlAttributes = new {@class = "form-control"},} %>' runat="server" />
                    <asp:ModelErrorMessage runat="server" AssociatedControlID="FirstNameEditor" ModelStateKey="FirstName" CssClass="help-block"/>
                </td>
                <td class="mvc<%= ModelState.IsValidField("LastName") ? "" : " has-error" %>">
                    <mvc:Editor ID="LastNameEditor" DataField="LastName" AdditionalViewData='<%$new: {htmlAttributes = new {@class = "form-control"},} %>' runat="server" />
                    <asp:ModelErrorMessage runat="server" AssociatedControlID="LastNameEditor" ModelStateKey="LastName" CssClass="help-block"/>
                </td>
                <td class="mvc<%= ModelState.IsValidField("BirthDate") ? "" : " has-error" %>">
                    <mvc:Editor ID="BirthDateEditor" DataField="BirthDate" AdditionalViewData='<%$new: {htmlAttributes = new {@class = "form-control"},} %>' runat="server" />
                    <asp:ModelErrorMessage runat="server" AssociatedControlID="BirthDateEditor" ModelStateKey="BirthDate" CssClass="help-block"/>
                </td>
                <td>
                    <asp:Button runat="server" Text="Update" CommandName="Update" CssClass="btn btn-primary btn-sm" />
                    <input type="checkbox" id="disableValidation" title="Disable client-side validation" />
                </td>
                <td><asp:LinkButton runat="server" Text="Cancel" CommandName="Cancel" CssClass="btn btn-link" /></td>
            </tr>
        </EditItemTemplate>
        <InsertItemTemplate>
            <tr>
                <td class="form-group<%= ModelState.IsValidField("FirstName") ? "" : " has-error" %>">
                    <mvc:Editor DataField="FirstName" ID="FirstNameEditor" AdditionalViewData='<%$new: {htmlAttributes = new {@class = "form-control"},} %>' runat="server" />
                    <asp:ModelErrorMessage runat="server" AssociatedControlID="FirstNameEditor" ModelStateKey="FirstName" CssClass="help-block"/>
                </td>
                <td class="form-group<%= ModelState.IsValidField("FirstName") ? "" : " has-error" %>">
                    <mvc:Editor DataField="LastName" ID="LastNameEditor" AdditionalViewData='<%$new: {htmlAttributes = new {@class = "form-control"},} %>' runat="server" />
                    <asp:ModelErrorMessage runat="server" AssociatedControlID="LastNameEditor" ModelStateKey="LastName" CssClass="help-block"/>
                </td>
                <td class="mvc<%= ModelState.IsValidField("BirthDate") ? "" : " has-error" %>">
                    <mvc:Editor ID="BirthDateEditor" DataField="BirthDate" AdditionalViewData='<%$new: {htmlAttributes = new {@class = "form-control"},} %>' runat="server" />
                    <asp:ModelErrorMessage runat="server" AssociatedControlID="BirthDateEditor" ModelStateKey="BirthDate" CssClass="help-block"/>
                </td>
                <td>
                    <asp:Button runat="server" Text="Insert" CommandName="Insert" CssClass="btn btn-primary btn-sm" />
                    <input type="checkbox" id="disableValidation" title="Disable client-side validation" />
                </td>
                <td><asp:HyperLink runat="server" Text="Cancel" NavigateUrl="<%$RouteUrl:routename=Webforms-ModelBinding-listview%>" CssClass="btn btn-link" /></td>
            </tr>
        </InsertItemTemplate>
        <EmptyDataTemplate>
            <tr>
                <td colspan="4">No items in collection</td>
            </tr>
        </EmptyDataTemplate>
    </asp:ListView>

    <asp:Panel runat="server" ID="ResultPanel" Visible="false">
        <p>These are the values you posted to the form:</p>
        <div class="form-horizontal">
        <div class="mvc">
            <mvc:Display runat="server" ID="ResultDisplay"/>
        </div>
            <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:HyperLink NavigateUrl="<%$RouteUrl:routename=Webforms-Modelbinding-ListView%>" Text="&#8592; Back to list" CssClass="btn btn-link" runat="server" />
            </div>
        </div>
        </div>
    </asp:Panel>
</div>
</asp:Content>
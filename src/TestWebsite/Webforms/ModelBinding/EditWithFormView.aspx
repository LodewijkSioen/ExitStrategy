<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditWithFormView.aspx.cs" Inherits="ExitStrategy.TestWebsite.Webforms.ModelBinding.EditWithFormView" MasterPageFile="~/Webforms/Layout.Master" Title="Webforms Modelbinding Edit With FormView" %>
<asp:Content ContentPlaceHolderID="Body" runat="server">
    <h1>Webforms Modelbinding Editor</h1>
    <p>
        This is a webforms page with the same Razor template from the Mvc Editor sample. You can post back to a codebehind method.
        Leaving First Name or Last Name blank will trigger the validation in Webforms and the result will be shown on the Razor template.<br />
        This page uses an &lt;mcv:Editor /&gt; control, which can be used in two ways:
    </p>
    <ul>
        <li><asp:HyperLink ID="LinkNormal" NavigateUrl="<%$RouteUrl:routename=Webforms-Modelbinding-edit%>" runat="server">As a top-level control using ModelBinding</asp:HyperLink></li>
        <li><asp:HyperLink ID="LinkFormView" NavigateUrl="<%$RouteUrl:routename=Webforms-Modelbinding-edit-formview%>" runat="server">As a nested control within an &lt;asp:FormView /&gt;</asp:HyperLink> (current page)</li>
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

    <asp:FormView ID="FormView" runat="server" SelectMethod="GetPerson" 
                                               UpdateMethod="UpdatePerson" 
                                               InsertMethod="InsertPerson" 
                                               DefaultMode="Edit"
                                               RenderOuterTable="false" 
                                               EnableViewState="False"
                                               OnInit="FormViewInit"
                                               OnDataBound="FormViewModeDataBound"
                                               ItemType="ExitStrategy.TestWebsite.Models.Person">
        <EditItemTemplate>
            <div class="form-horizontal">
                    <div class="form-group<%= ModelState.IsValidField("FirstName") ? "" : " has-error" %>">
                        <asp:Label Text="First Name" AssociatedControlID="FirstNameEditor" CssClass="col-sm-2 control-label" runat="server"/>
                        <div class="col-sm-4">
                            <mvc:Editor ID="FirstNameEditor" runat="server" DataField="FirstName" AdditionalViewData='<%$new: {htmlAttributes = new {@class = "form-control mvc"},} %>'/>
                            <asp:ModelErrorMessage runat="server" AssociatedControlID="FirstNameEditor" ModelStateKey="FirstName" CssClass="help-block"/>
                        </div>
                    </div>
                    <div class="form-group<%= ModelState.IsValidField("LastName") ? "" : " has-error" %>">
                        <asp:Label Text="Last Name" AssociatedControlID="LastNameEditor" CssClass="col-sm-2 control-label" runat="server"/>
                        <div class="col-sm-4">
                            <asp:BoundField DataField="BirthDate" />
                            <mvc:Editor ID="LastNameEditor" runat="server" DataField="LastName" AdditionalViewData='<%$new: {htmlAttributes = new {@class = "form-control mvc"},} %>'/>
                            <asp:ModelErrorMessage runat="server" AssociatedControlID="LastNameEditor" ModelStateKey="LastName" CssClass="help-block"/>
                        </div>
                    </div>
                    <div class="form-group<%= ModelState.IsValidField("BirthDate") ? "" : " has-error" %>">
                        <asp:Label Text="BirthDate" AssociatedControlID="BirthDateEditor" CssClass="col-sm-2 control-label" runat="server"/>
                        <div class="col-sm-4">
                            <mvc:Editor ID="BirthDateEditor" runat="server" DataField="BirthDate" AdditionalViewData='<%$new: {htmlAttributes = new {@class = "form-control mvc"},} %>'/>
                            <asp:ModelErrorMessage runat="server" AssociatedControlID="BirthDateEditor" ModelStateKey="BirthDate" CssClass="help-block"/>
                        </div>
                    </div>
                    <div class="form-group<%= ModelState.IsValidField("Gender") ? "" : " has-error" %>">
                        <asp:Label Text="Gender" AssociatedControlID="GenderEditor" CssClass="col-sm-2 control-label" runat="server"/>
                        <div class="col-sm-4">
                            <mvc:Editor ID="GenderEditor" runat="server" DataField="Gender" AdditionalViewData='<%$new: {htmlAttributes = new {@class = "form-control mvc"},} %>'/>
                            <asp:ModelErrorMessage runat="server" AssociatedControlID="GenderEditor" ModelStateKey="Gender" CssClass="help-block"/>
                        </div>
                    </div>
                    <div class="form-group<%= ModelState.IsValidField("IsDeceased") ? "" : " has-error" %>">
                        <asp:Label Text="IsDeceased" AssociatedControlID="IsDeceasedEditor" CssClass="col-sm-2 control-label" runat="server"/>
                        <div class="col-sm-4">
                            <mvc:Editor ID="IsDeceasedEditor" runat="server" DataField="IsDeceased" AdditionalViewData='<%$new: {htmlAttributes = new {@class = "form-control mvc"},} %>'/>
                            <asp:ModelErrorMessage runat="server" AssociatedControlID="IsDeceasedEditor" ModelStateKey="IsDeceased" CssClass="help-block"/>
                        </div>
                    </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <asp:Button ID="SubmitButton" CssClass="btn btn-primary" runat="server" CommandName="Update" Text="Edit"/>
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
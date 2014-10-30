<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="TestWebsite.Webforms.ModelBinding.Index" MasterPageFile="~/Webforms/Layout.Master" Title="Webforms Modelbinding" %>
<asp:Content ContentPlaceHolderID="Body" runat="server">
<div class="webforms">
    <h1>Modelbinding</h1>
    <p>How Modelbinding works</p>
    <asp:ListView runat="server" SelectMethod="Unnamed_GetData">
        <LayoutTemplate>
            <table>
                <thead>
                    <tr>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr runat="server" id="itemPlaceholder" />
                </tbody>
          </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr runat="server">
                <td></td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</div>
</asp:Content>
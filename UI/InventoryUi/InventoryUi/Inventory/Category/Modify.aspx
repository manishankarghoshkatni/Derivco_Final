<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Modify.aspx.cs" MasterPageFile="~/Site.Master" Inherits="InventoryUi.Inventory.Category.Modify" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center">
        <h3>Modify Category</h3>
        <asp:Label ID="LblErrorMsg" runat="server" Text="Label" ForeColor="Red"></asp:Label>
        <asp:Label ID="LblMsg" runat="server" Text="Label" ForeColor="#009900"></asp:Label><br />
        <br />

        <table class="table-condensed">
            <tr>
                <td>Id</td>
                <td>
                    <asp:TextBox ID="TxtId" runat="server" ReadOnly="True" ForeColor="#999966"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="TxtId" Display="Dynamic" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">Category Id is required</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Name</td>
                <td>
                    <asp:TextBox ID="TxtCategoryName" runat="server" MaxLength="100"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TxtCategoryName" Display="Dynamic" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">Category Name is required</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Description</td>
                <td>
                    <asp:TextBox ID="TxtDescription" runat="server" MaxLength="300"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="TxtDescription" ErrorMessage="RequiredFieldValidator" ForeColor="Red">Category Description is required</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>

        <br />
        <br />

        <asp:Button ID="CmdSave" runat="server" Text="Save" OnClick="CmdSave_Click" />
        &nbsp;&nbsp;
        <a href="/Inventory/Category/Search">Back to Search Page</a>

        <script type="text/javascript">
            function confirmSubmit() {
                return confirm('Are you sure to submit the form ?');
            }
            document.getElementById('ctl01').onsubmit = function () {
                return confirmSubmit();
            };
        </script>
    </div>
</asp:Content>

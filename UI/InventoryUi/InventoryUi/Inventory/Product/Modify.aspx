<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Modify.aspx.cs" MasterPageFile="~/Site.Master" Inherits="InventoryUi.Inventory.Product.Modify" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center">
        <h3>Modify Product</h3>
        <asp:Label ID="LblErrorMsg" runat="server" Text="Label" ForeColor="Red"></asp:Label>
        <asp:Label ID="LblMsg" runat="server" Text="Label" ForeColor="#009900"></asp:Label><br />
        <br />
        <table class="table-condensed">
            <tr>
                <td>Id</td>
                <td>
                    <asp:TextBox ID="TxtId" runat="server" Enabled="false"></asp:TextBox></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Name</td>
                <td>
                    <asp:TextBox ID="TxtProductName" runat="server" MaxLength="100"></asp:TextBox>
                </td>

                <td>
                    <asp:RequiredFieldValidator ID="RfvProductName" ForeColor="Red" runat="server" ControlToValidate="TxtProductName"  Display="Dynamic" ErrorMessage="RequiredFieldValidator">Product name is Required</asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td>Description</td>
                <td>
                    <asp:TextBox ID="TxtDescription" runat="server" MaxLength="300" TextMode="MultiLine" Rows="3" Width="100%" Style="overflow: auto;"></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator ID="RfvProductDescription" ForeColor="Red" runat="server" ControlToValidate="TxtDescription" Display="Dynamic" ErrorMessage="RequiredFieldValidator">Product description is Required</asp:RequiredFieldValidator></td>
            </tr>

            <tr>
                <td>Category</td>
                <td>
                    <asp:DropDownList ID="CboCategory" Width="100%" runat="server"></asp:DropDownList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ForeColor="Red" InitialValue="0" ID="RfvProductCategory" Display="Dynamic"
                        runat="server" ControlToValidate="CboCategory"
                        Text="*" ErrorMessage="ErrorMessage">Please select a Category</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Unit</td>
                <td>
                    <asp:DropDownList ID="CboUnit" runat="server" Width="100%"></asp:DropDownList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ForeColor="Red" InitialValue="0" ID="RfvProductUnit" Display="Dynamic"
                        runat="server" ControlToValidate="CboUnit"
                        Text="*" ErrorMessage="ErrorMessage">Please select a Unit</asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td>Price</td>
                <td>
                    <asp:TextBox ID="TxtPrice" runat="server" MaxLength="300" Width="100%"></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator ID="RfvProductPrice" ForeColor="Red" runat="server" ControlToValidate="TxtPrice"  Display="Dynamic" ErrorMessage="RequiredFieldValidator">Product description is Required</asp:RequiredFieldValidator>
                </td>

            </tr>
            <tr>
                <td>Currency</td>
                <td>
                    <asp:DropDownList ID="CboCurrency" runat="server" Width="100%">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ForeColor="Red" InitialValue="Select" ID="RequiredFieldValidator1" Display="Dynamic"
                        runat="server" ControlToValidate="CboCurrency"
                        Text="*" ErrorMessage="ErrorMessage">Please select a Currency</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>

        <br />
        <br />

        <asp:Button ID="CmdSave" runat="server" Text="Save" OnClick="CmdSave_Click" />
        &nbsp;&nbsp;
        <a href="/Inventory/Product/Search">Back to Search Page</a>

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



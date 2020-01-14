<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" MasterPageFile="~/Site.Master" Inherits="InventoryUi.Inventory.Category.Search" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center">
        <h3>Search/Update/Delete Category</h3>
        <asp:Label ID="LblErrorMsg" runat="server" Text="Label" ForeColor="Red"></asp:Label>
        <asp:Label ID="LblMsg" runat="server" Text="Label" ForeColor="#009900"></asp:Label><br />

        <table class="table-condensed">
            <tr>
                <td>Id</td>
                <td>
                    <asp:TextBox ID="TxtId" runat="server"></asp:TextBox></td>
                <td>
                    <asp:Button ID="CmdSearchById" runat="server" Text="🔍" OnClick="CmdSearchById_Click" Font-Names="Arial Unicode MS" /></td>
            </tr>
            <tr>
                <td>Name</td>
                <td>
                    <asp:TextBox ID="TxtCategoryName" runat="server" MaxLength="100"></asp:TextBox></td>
                <td>
                    <asp:Button ID="CmdSearchByName" runat="server" Text="🔍" OnClick="CmdSearchByName_Click" /></td>
            </tr>
            <tr>
                <td>Description</td>
                <td colspan="2">
                    <asp:TextBox ID="TxtDescription" runat="server" ReadOnly="True" ForeColor="#999966" MaxLength="300"></asp:TextBox></td>

            </tr>

        </table>

        <br />

        <div>
            <asp:GridView class="table-condensed" ID="CategoryGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="CategoryId" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowEditing="CategoryGridView_RowEditing" OnRowDeleting="CategoryGridView_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="CategoryId" HeaderText="Id" />
                    <asp:BoundField DataField="CategoryName" HeaderText="Name" />
                    <asp:BoundField DataField="CategoryDescription" HeaderText="Description" />
                    <asp:CommandField ShowEditButton="true" />
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return confirm('Are you sure you want to delete?'); " CausesValidation="False" CommandName="Delete" Text="Delete"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>

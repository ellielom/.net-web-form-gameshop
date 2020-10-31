<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ManageCategories.aspx.cs" Inherits="FinalProject.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="categoryID" DataSourceID="CategoryDataSource">
            <Columns>
                <asp:BoundField DataField="categoryID" HeaderText="ID" ReadOnly="True" SortExpression="categoryID" />
                <asp:BoundField DataField="categoryName" HeaderText="Name" SortExpression="categoryName" />
                <asp:CommandField ShowEditButton="True" ButtonType="Button" HeaderText="Edit" />
                <asp:CommandField ShowDeleteButton="True" ButtonType="Button" HeaderText="Delete" />
            </Columns>
        </asp:GridView>
    </p>
    <p>
        <asp:Label ID="lblCategoryHeader" runat="server" Text="Add new category:" Font-Bold="True"></asp:Label><br />
        <asp:Label ID="Label1" runat="server" Text="Name:"></asp:Label>
        <asp:TextBox ID="txtCategory" runat="server" ValidationGroup="ValidateCategory"></asp:TextBox>
        <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" ValidationGroup="ValidateCategory" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCategory" ErrorMessage="RequiredFieldValidator" ValidationGroup="ValidateCategory">Error: Category Name Required</asp:RequiredFieldValidator>
        <asp:SqlDataSource ID="CategoryDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Category]"  UpdateCommand="UPDATE Category SET categoryName = @categoryName WHERE categoryID = @categoryID" DeleteCommand="DELETE FROM Category WHERE categoryID = @categoryID"></asp:SqlDataSource>
    </p>
</asp:Content>

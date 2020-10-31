<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="FinalProject.Admin.ManageUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="userID" DataSourceID="SqlDataSource1">
        <Columns>
            <asp:BoundField DataField="userID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="userID" />
            <asp:BoundField DataField="userFullName" HeaderText="FullName" SortExpression="userFullName" />
            <asp:BoundField DataField="username" HeaderText="Username" SortExpression="username" />
            <asp:BoundField DataField="password" HeaderText="Password" InsertVisible="False" ReadOnly="True" SortExpression="password" />
            <asp:BoundField DataField="shippingAddress" HeaderText="Shipping_Add" SortExpression="shippingAddress" />
            <asp:BoundField DataField="isAdmin" HeaderText="isAdmin" SortExpression="isAdmin" />
            <asp:CommandField ShowEditButton="True" ButtonType="Button" HeaderText="Edit" />
            <asp:CommandField ShowDeleteButton="True" ButtonType="Button" HeaderText="Delete" />
        </Columns>
    </asp:GridView>
    <br />
    <br />
    <br />
    <br />
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [User]" 
        UpdateCommand="UPDATE [User] SET userFullName = @userFullName, username = @username, shippingAddress = @shippingAddress, isAdmin = @isAdmin WHERE userID = @userID" DeleteCommand="DELETE FROM [User] 
        WHERE userID = @userID" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>"></asp:SqlDataSource>
    
</asp:Content>

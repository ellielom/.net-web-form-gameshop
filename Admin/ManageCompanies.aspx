<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ManageCompanies.aspx.cs" Inherits="FinalProject.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="companyID" DataSourceID="SqlDataSource1">
        <Columns>
            <asp:BoundField DataField="companyID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="companyID" />
            <asp:BoundField DataField="companyName" HeaderText="Name" SortExpression="companyName" />
            <asp:BoundField DataField="location" HeaderText="Location" SortExpression="location" />
            <asp:CommandField ShowEditButton="True" ButtonType="Button" HeaderText="Edit" />
            <asp:CommandField ShowDeleteButton="True" ButtonType="Button" HeaderText="Delete" />
        </Columns>
    </asp:GridView>
    <br />
    <br />
    <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="Add new company:"></asp:Label>
    <br />
    <asp:Label ID="Label1" runat="server" Text="Name:"></asp:Label>
    <asp:TextBox ID="txtCompany" runat="server" ValidationGroup="grpCompany"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Error: Company Name Required" ControlToValidate="txtCompany" ValidationGroup="grpCompany"></asp:RequiredFieldValidator>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Location:"></asp:Label>
    <asp:TextBox ID="txtLocation" runat="server" ValidationGroup="grpCompany"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLocation" ErrorMessage="Error: Location Required" ValidationGroup="grpCompany"></asp:RequiredFieldValidator>
    <br />
    <asp:Button ID="btnAdd" runat="server" style="margin-bottom: 0px" Text="Add" OnClick="btnAdd_Click" ValidationGroup="grpCompany" />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Company]" UpdateCommand="UPDATE [Company] SET companyName = @companyName, location = @location WHERE companyID = @companyID" DeleteCommand="DELETE FROM [Company] WHERE companyID = @companyID"></asp:SqlDataSource>
    
</asp:Content>

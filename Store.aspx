<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Store.aspx.cs" Inherits="FinalProject.Store" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%@ MasterType VirtualPath="~/Site1.Master" %>

  

    <asp:Label ID="lblSearch" runat="server" Text="Search by: "></asp:Label>
    <asp:DropDownList ID="ddlSearchType" runat="server">
        <asp:ListItem>Product</asp:ListItem>
        <asp:ListItem>Company</asp:ListItem>
        <asp:ListItem>Category</asp:ListItem>
    </asp:DropDownList>
    <asp:TextBox ID="txtSearch" runat="server" ValidationGroup="grpSearch"></asp:TextBox>
    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" ValidationGroup="grpSearch" />
    <asp:Button ID="btnResetSearch" runat="server" Text="Reset Search" OnClick="btnResetSearch_Click" />
    <asp:RequiredFieldValidator ID="reqSearch" runat="server" ControlToValidate="txtSearch" ErrorMessage="Error: Search value required" ValidationGroup="grpSearch"></asp:RequiredFieldValidator>
    <br />
    <br />

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="743px" OnRowCommand="GridView1_RowCommand" DataKeyNames="productID" >
        <Columns>
            
            <asp:BoundField DataField="productName" HeaderText="Name" SortExpression="productName" />
            <asp:BoundField DataField="productDescription" HeaderText="Description" SortExpression="productDescription" />
            <asp:BoundField DataField="productPrice" HeaderText="Price" SortExpression="productPrice" />
            <asp:BoundField DataField="productQuantity" HeaderText="Quantity" SortExpression="productQuantity" />
            <asp:BoundField DataField="companyName" HeaderText="Company" SortExpression="companyName" />
            <asp:BoundField DataField="categoryName" HeaderText="Category" SortExpression="categoryName" />
            <asp:TemplateField HeaderText="Order Quantity">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="txtOrderQuantity" TextMode="Number"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>

            
            <asp:TemplateField HeaderText="Add to Cart">
                <ItemTemplate>
                    <asp:Button ID="btnAdd" runat="server" Text="Add to Cart" CommandName="AddToCart" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                </ItemTemplate>
            </asp:TemplateField>

            
        </Columns>
    </asp:GridView>
    &nbsp;
    

    

    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
    <br />
    

    

</asp:Content>

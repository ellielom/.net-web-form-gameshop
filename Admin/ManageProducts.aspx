<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ManageProducts.aspx.cs" Inherits="FinalProject.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%@ MasterType VirtualPath="~/Site1.Master" %>

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="743px" OnRowCommand="GridView1_RowCommand">
        <Columns>
            <asp:BoundField DataField="ProductID" HeaderText="ID" SortExpression="ProductID" />
            <asp:BoundField DataField="productName" HeaderText="Name" SortExpression="productName" />
            <asp:BoundField DataField="productDescription" HeaderText="Description" SortExpression="productDescription" />
            <asp:BoundField DataField="productPrice" HeaderText="Price" SortExpression="productPrice" />
            <asp:BoundField DataField="productQuantity" HeaderText="Quantity" SortExpression="productQuantity" />
            <asp:BoundField DataField="companyName" HeaderText="Company" SortExpression="companyName" />
            <asp:BoundField DataField="categoryName" HeaderText="Category" SortExpression="categoryName" />
            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="edit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete">
                <ItemTemplate>
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                </ItemTemplate>
            </asp:TemplateField>

            
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT Product.productName, Product.productDescription, Product.productPrice, Product.productQuantity, Company.companyName, Category.categoryName FROM Product 	LEFT JOIN ProductCompany ON Product.productID = ProductCompany.productID LEFT JOIN Company ON ProductCompany.companyID = Company.companyID LEFT JOIN ProductCategory ON product.productID = ProductCategory.productID LEFT JOIN Category ON ProductCategory.categoryID = Category.categoryID" ></asp:SqlDataSource>
    &nbsp;<table>
        <tr>
            <td><asp:Label ID="Label7" runat="server" Text="Name"></asp:Label>
                :</td>
            <td><asp:TextBox ID="txtName" runat="server" ></asp:TextBox></td>
            <td><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Error: Name required" ValidationGroup="grpProducts" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="CustomValidator"></asp:CustomValidator>
            </td>
        </tr>
         <tr>
            <td><asp:Label ID="Label8" runat="server" Text="Description"></asp:Label>
                :</td>
            <td><asp:TextBox ID="txtDesc" runat="server" ></asp:TextBox></td>
            <td><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Error: Description required" ValidationGroup="grpProducts" ControlToValidate="txtDesc"></asp:RequiredFieldValidator></td>
        </tr>
         <tr>
            <td><asp:Label ID="Label9" runat="server" Text="Price"></asp:Label>
                :</td>
            <td><asp:TextBox ID="txtPrice" runat="server" ></asp:TextBox></td>
            <td><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Error: Price required" ValidationGroup="grpProducts" ControlToValidate="txtPrice"></asp:RequiredFieldValidator></td>
        </tr>
         <tr>
            <td style="height: 25px"><asp:Label ID="Label10" runat="server" Text="Quantity"></asp:Label>
                :</td>
            <td style="height: 25px"><asp:TextBox ID="txtQuantity" runat="server" ></asp:TextBox></td>
            <td style="height: 25px"><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Error: Quantity required" ValidationGroup="grpProducts" ControlToValidate="txtQuantity"></asp:RequiredFieldValidator></td>
        </tr>
         <tr>
            <td><asp:Label ID="Label11" runat="server" Text="Company"></asp:Label>
                :</td>
            <td><asp:DropDownList ID="ddlCompany" runat="server"></asp:DropDownList></td>
             <td><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCompany" InitialValue="Please select" ErrorMessage="Error: Company required" ValidationGroup="grpProducts" /></td>

        </tr>
        <tr>
            <td><asp:Label ID="Label1" runat="server" Text="Category"></asp:Label>
                :</td>
            <td><asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList></td>
            <td><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlCategory" InitialValue="Please select" ErrorMessage="Error: Category required" ValidationGroup="grpProducts" /></td>
        </tr>
    </table>
    

    

    <asp:Button ID="btnAdd" runat="server" Text="Add" ValidationGroup="grpProducts" OnClick="btnAdd_Click" />
    <asp:Label ID="lblErrorMessage" runat="server"></asp:Label>
    <br />
    

    

</asp:Content>

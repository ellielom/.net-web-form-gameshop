<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="FinalProject.Cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%@ MasterType VirtualPath="~/Site1.Master" %>




    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="743px" OnRowCommand="GridView1_RowCommand" DataKeyNames="productID" OnRowDataBound="GridView1_RowDataBound">
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

            
            <asp:TemplateField HeaderText="Update Cart">
                <ItemTemplate>
                    <asp:Button ID="btnUpdate" runat="server" Text="Update Item" CommandName="UpdateItemInCart" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                </ItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Remove from Cart">
                <ItemTemplate>
                    <asp:Button ID="btnRemove" runat="server" Text="Remove Item" CommandName="RemoveItemFromCart" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                </ItemTemplate>
            </asp:TemplateField>

            
        </Columns>
    </asp:GridView>
    
    <br />
    <table>
        <tr>
            <th>Subtotal</th>
            <td id="tdSubtotal"><asp:Label runat="server" ID="lblSubtotal"></asp:Label></td>
        </tr>
        <tr>
            <th>Tax (@13%)</th>
            <td id="tdTax"><asp:Label runat="server" ID="lblTax"></asp:Label></td>
        </tr>
        <tr>
            <th>Total</th>
            <td id="tdTotal"><asp:Label runat="server" ID="lblTotal"></asp:Label></td>
        </tr>
    </table>
    <br />
    
    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
    <br />
    &nbsp;
    

    

    <br />
    

    
<asp:Button ID="btnOrder" runat="server" OnClick="btnOrder_Click" Text="Order" />
    <asp:Label ID="lblLogin" runat="server" Enabled="False" Visible="False">Please login to complete your order.</asp:Label>
    <br />
    

    

</asp:Content>

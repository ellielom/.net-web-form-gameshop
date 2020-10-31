<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="OrderHistory.aspx.cs" Inherits="FinalProject.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="Stock/OrderHistory.css" rel="stylesheet" />
   
   

    <center>
    <asp:GridView ID="GridView1" runat="server"  CssClass="myCss" Width="400px" DataSourceID="SqlDataSource1" OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="False" PagerStyle-CssClass="pgr" >

        <Columns>

            <asp:BoundField DataField="orderID" HeaderText="Order ID"  SortExpression="orderID" />
            
            <asp:BoundField DataField="date" HeaderText="Date"  SortExpression="date" />
            
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button id="Details" runat="server" Text="Details" CommandName="getDetails" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                </ItemTemplate>
            </asp:TemplateField>
            
        </Columns>
    </asp:GridView>
            <h1><asp:Label runat="server" ID="lblIvy" ></asp:Label></h1>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT DISTINCT orderID, date FROM [OrderHistory] where userID =@userID">
     <SelectParameters>
        <asp:SessionParameter Name="userID" SessionField="userID" Type="String" />
    </SelectParameters>
    </asp:SqlDataSource>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="productName" HeaderText="Product Name" />
                <asp:BoundField DataField="quantity" HeaderText="Quantity" />
                <asp:BoundField DataField="price" HeaderText="Price" />
            </Columns>
        </asp:GridView>
    <br /><br />
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
        

    </center>
     
    


</asp:Content>

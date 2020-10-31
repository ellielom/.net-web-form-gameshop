<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EditProduct.aspx.cs" Inherits="FinalProject.Admin.EditProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <table>
        <tr>
            <td>
                <asp:Label Text="Name" runat="server" ID="lblName"></asp:Label>
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Error: Name Required" 
                   ControlToValidate="txtName" ValidationGroup="grpProducts"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label Text="Desctiption" runat="server" ID="lblDesctiption"></asp:Label>
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtDescription"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Error: Description Required" 
                   ControlToValidate="txtDescription" ValidationGroup="grpProducts"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label Text="Price" runat="server" ID="lblPrice"></asp:Label>
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtPrice"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Error: Price Required" 
                   ControlToValidate="txtPrice" ValidationGroup="grpProducts"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label Text="Quantity" runat="server" ID="lblQuantity"></asp:Label>
            </td>
            <td style="margin-left: 40px">
                <asp:TextBox runat="server" ID="txtQuantity"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Error: Quantity Required" 
                   ControlToValidate="txtQuantity" ValidationGroup="grpProducts"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label Text="Category" runat="server" ID="lblCategory"></asp:Label>
            </td>
            <td>
                <asp:DropDownList runat="server" ID="ddlCategory"></asp:DropDownList>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlCategory" 
                    InitialValue="Please select" ErrorMessage="Error: Category Required" ValidationGroup="grpProducts" />
            </td>
        </tr>
        <tr>
            <td style="height: 26px">
                <asp:Label Text="Company" runat="server" ID="lblCompany"></asp:Label>
            </td>
            <td style="height: 26px">
                <asp:DropDownList runat="server" ID="ddlCompany"></asp:DropDownList>
            </td>
            <td style="height: 26px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCompany" 
                    InitialValue="Please select" ErrorMessage="Error: Company Required" ValidationGroup="grpProducts" />
            </td>
        </tr>
    </table>
     <asp:Button Text="Update" runat="server" ID="btnUpdate" OnClick="BtnUpdate_Click" ValidationGroup="grpProducts"/>
    <asp:Button Text="Cancel" runat="server" ID="btnCancel" OnClick="btnCancel_Click"/>
    <asp:Label ID="lblError" runat="server"></asp:Label>
    <asp:HiddenField ID="hdnProdID" runat="server" Visible="true" Value="0" />

</asp:Content>

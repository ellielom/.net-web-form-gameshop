<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="FinalProject.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
    <link href="Stock/css.css" rel="stylesheet" />
    
</head>

<body><center>
    <div>
    <form id="form1" runat="server">
        <br /><br /><br /><br />
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </td>

            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFullName" runat="server" Text="Label">Full Name:</asp:Label>
                    
                </td>
                <td>
                   
                    <asp:TextBox ID="txtFullName" runat="server"></asp:TextBox><br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUserName" runat="server" Text="Label">Username:</asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox><br />
                </td>
            </tr>
            
            <tr>
                <td>
                    <asp:Label ID="lblPassword" runat="server" Text="Label">Password:</asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox><br />
    
                </td>
            </tr>
                   
            <tr>
                <td>
                    <asp:Label ID="lblShippingAddress" runat="server" Text="Label">Shipping Address:</asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtShippingAddress" runat="server"></asp:TextBox><br />
    
                </td>
            </tr>
             
            <tr>
                <td>
                    <br /><br />
                     <asp:Button ID="btnCancel" runat="server" Text="  Cancel  " OnClick="btnCancel_Click" />
                </td>
                <td>
                    <br /><br />
                    <asp:Button ID="btnRegister" runat="server" Text="  Register  " OnClick="btnRegister_Click" />	
                </td>
                
            </tr>
        </table>
    </form>
    </div>
</center></body>
</html>

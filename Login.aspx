<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FinalProject.Login" %>

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
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl1" runat="server" Text="Label">User Name:</asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txt1" runat="server"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td>
                    <asp:Label ID="lbl2" runat="server" Text="Label">Password:</asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox TextMode="Password" ID="txt2" runat="server"></asp:TextBox><br />
                </td>
            </tr>
            
            <tr>
                <td>
                    <br /><br /><br /><br />
                    <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" />
                   
                     
                </td>
                <td style="text-align:right;">
                    <br /><br /><br /><br />
                     <asp:Button ID="btnSubmit" runat="server" Text="  Login  " OnClick="btnSubmit_Click" />
                </td>
                
               
            </tr>
        </table>
        <br /><br /><br /><br />
    </form>
    </div>
</center></body>
</html>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="updateBanner.aspx.cs" Inherits="Dist23MVC.updateBanner" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 263px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">Main banner</td>
                <td>
                    <asp:TextBox ID="tbBannerTitle" runat="server" Width="667px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Subtitle</td>
                <td>
                    <asp:TextBox ID="tbBannerSubTitle" runat="server" Width="659px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Main phone</td>
                <td>
                    <asp:TextBox ID="tbHotlinePh" runat="server" Width="279px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Alt phone</td>
                <td>
                    <asp:TextBox ID="tbAltHotline" runat="server" Width="275px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Alt Phone Message</td>
                <td>
                    <asp:TextBox ID="tbAltHotlineMsg" runat="server" Width="265px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
    <div>
    
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Update Banner" />
    
    </div>
    </form>
</body>
</html>

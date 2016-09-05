<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllServants.aspx.cs" Inherits="Dist23MVC.AllServants" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="DistKey" HeaderText="DistKey" SortExpression="DistKey" />
                <asp:BoundField DataField="GroupName" HeaderText="GroupName" SortExpression="GroupName" />
                <asp:BoundField DataField="PositionName" HeaderText="PositionName" SortExpression="PositionName" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
                <asp:BoundField DataField="phone" HeaderText="phone" SortExpression="phone" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Dist23Data %>" SelectCommand="SELECT * FROM [vw_AllServants]"></asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>

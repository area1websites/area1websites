<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentSpec.aspx.cs" Inherits="Dist23MVC.PaymentSpec" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 177px;
        }
        .auto-style3 {
            width: 152px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label1" runat="server" Text="Text"></asp:Label>
                </td>
                <td class="auto-style3">
                    <asp:Label ID="Label2" runat="server" Text="Amount"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:TextBox ID="tbValue" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style3">
                    <asp:TextBox ID="tbAmount" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged1">
            <Columns>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="pKey" HeaderText="pKey" InsertVisible="False" ReadOnly="True" SortExpression="pKey" />
                <asp:BoundField DataField="PaymentSetupKey" HeaderText="PaymentSetupKey" SortExpression="PaymentSetupKey" />
                <asp:BoundField DataField="SpecialValue" HeaderText="SpecialValue" SortExpression="SpecialValue" />
                <asp:BoundField DataField="SpecialAmount" HeaderText="SpecialAmount" SortExpression="SpecialAmount" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Dist23Data %>" InsertCommand="INSERT INTO [PaymentSpecValues] ([PaymentSetupKey], [SpecialValue], [SpecialAmount]) VALUES (@PaymentSetupKey, @SpecialValue, @SpecialAmount)" SelectCommand="SELECT * FROM [PaymentSpecValues] WHERE ([PaymentSetupKey] = @PaymentSetupKey)" UpdateCommand="UPDATE [PaymentSpecValues] SET [PaymentSetupKey] = @PaymentSetupKey, [SpecialValue] = @SpecialValue, [SpecialAmount] = @SpecialAmount WHERE [pKey] = @pKey">
            <InsertParameters>
                <asp:Parameter Name="PaymentSetupKey" Type="Int32" />
                <asp:Parameter Name="SpecialValue" Type="String" />
                <asp:Parameter Name="SpecialAmount" Type="Decimal" />
            </InsertParameters>
            <SelectParameters>
                <asp:SessionParameter Name="PaymentSetupKey" SessionField="currSetupKey" Type="Int32" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="PaymentSetupKey" Type="Int32" />
                <asp:Parameter Name="SpecialValue" Type="String" />
                <asp:Parameter Name="SpecialAmount" Type="Decimal" />
                <asp:Parameter Name="pKey" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>

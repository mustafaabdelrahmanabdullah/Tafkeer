<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="teacherhistory.aspx.cs" Inherits="endlessthoughts.personsdatareport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Viewer</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div>
            <asp:Label ID="Label1" runat="server" Text="Select a name" Font-Bold="True"></asp:Label>
            &nbsp;
            <asp:DropDownList ID="DropDownList1" runat="server" Height="20px" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
            &nbsp;
            <asp:TextBox ID="TextBox1" runat="server" Width="209px"></asp:TextBox>
        </div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="529px" Width="1313px" />
    </form>
</body>
</html>

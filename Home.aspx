<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="endlessthoughts.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>

</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 100px">
            <placeholder>
            <asp:Panel ID="Panel1" runat="server" Height="150px" BorderStyle="Groove">
                <asp:TextBox ID="TextBox1" runat="server" Width="125px" ReadOnly="True" Font-Bold="True"></asp:TextBox>
                <asp:TextBox ID="TextBox2" runat="server" Width="200px" ReadOnly="True" Font-Bold="True"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TextBox16" runat="server" Width="125px" ReadOnly="True" Font-Bold="True"></asp:TextBox>
                <asp:TextBox ID="TextBox17" runat="server" Width="200px" ReadOnly="True" Font-Bold="True"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TextBox3" runat="server" ReadOnly="True" Width="125px" Font-Bold="True"></asp:TextBox>
                <asp:TextBox ID="TextBox4" runat="server" ReadOnly="True" Width="200px" Font-Bold="True"></asp:TextBox>
                <br />
                <asp:TextBox ID="TextBox18" runat="server" Width="125px" ReadOnly="True" Font-Bold="True"></asp:TextBox>
                <asp:TextBox ID="TextBox19" runat="server" Width="200px" ReadOnly="True" Font-Bold="True"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TextBox5" runat="server" Width="125px" ReadOnly="True" Font-Bold="True"></asp:TextBox>
                <asp:TextBox ID="TextBox6" runat="server" ReadOnly="True" Width="200px" Font-Bold="True"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TextBox20" runat="server" Width="125px" ReadOnly="True" Font-Bold="True"></asp:TextBox>
                <asp:TextBox ID="TextBox21" runat="server" Width="200px" ReadOnly="True" Font-Bold="True"></asp:TextBox>
                <br />
                <asp:TextBox ID="TextBox7" runat="server" ReadOnly="True" Width="125px" Font-Bold="True"></asp:TextBox>
                <asp:TextBox ID="TextBox8" runat="server" ReadOnly="True" Width="200px" Font-Bold="True"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TextBox22" runat="server" Width="125px" ReadOnly="True" Font-Bold="True"></asp:TextBox>
                <asp:TextBox ID="TextBox23" runat="server" Width="200px" ReadOnly="True" Font-Bold="True"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TextBox9" runat="server" ReadOnly="True" Width="125px" Font-Bold="True"></asp:TextBox>
                <asp:TextBox ID="TextBox10" runat="server" ReadOnly="True" Width="200px" Font-Bold="True"></asp:TextBox>
                <br />
                <asp:TextBox ID="TextBox13" runat="server" Font-Bold="True" ReadOnly="True" Width="125px"></asp:TextBox>
                <asp:TextBox ID="TextBox14" runat="server" Width="200px" ReadOnly="True" Font-Bold="True"></asp:TextBox>
                </asp:Panel>
                </placeholder>
        </div>
        
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" Style="height: 50px;" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Pick a task "></asp:Label>
                &nbsp;&nbsp;
                <asp:DropDownList ID="DropDownList1" runat="server" Font-Bold="True" Height="30px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="225px" AutoPostBack="True">
                </asp:DropDownList>
                &nbsp;&nbsp;
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Task Code"></asp:Label>
&nbsp;<asp:TextBox ID="TextBox11" runat="server" ReadOnly="True" Width="125px" Font-Bold="True"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<br />
                <br />
            <iframe id="iframeWebpage" runat="server" style="width:100%; height:500px;"></iframe>
                </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        </asp:UpdateProgress>

    </form>
</body>
</html>

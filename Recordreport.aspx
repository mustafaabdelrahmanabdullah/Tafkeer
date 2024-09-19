<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Recordreport.aspx.cs" Inherits="endlessthoughts.Recordreport" Async="true" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">
        .inlineBlock
        {
            display: inline-block;
            vertical-align: top;
        }
        .container
        {
            width: 100%;
            white-space: nowrap;
        }
    </style>

    <script type="text/javascript">
        function highlightDate(dateString) {
            var calendar = document.getElementById('<%= Calendar1.ClientID %>');
            var date = new Date(dateString);
            // Use JavaScript to highlight the date on the calendar
        }
    </script>

    <script type="text/javascript">
        function previewImage(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    var imageControl = document.getElementById('<%= Image1.ClientID %>');
                    imageControl.src = e.target.result;
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>

<script type="text/javascript">
    function resetControls() {
        // Reset FileUpload control
        document.getElementById('<%= FileUpload1.ClientID %>').value = "";
        
        // Reset Image control
        var imageControl = document.getElementById('<%= Image1.ClientID %>');
        imageControl.src = "";  // Or set to a placeholder image if you prefer
    }
</script>

<script type="text/javascript">
    function validateinputs() {
        if (document.getElementById('<%= TextBox1.ClientID %>').value === "") {
            alert("Select the person name you are filling this report for");
            return false;
        }

        if (document.getElementById('<%= TextBox2.ClientID %>').value === "") {
            alert("You must type the matter");
            return false;
        }

        return true;
    }
</script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="Label1" runat="server" Text="Name" Font-Bold="True"></asp:Label>
        &nbsp;
        <asp:DropDownList ID="DropDownList1" runat="server" Height="30px" Width="400px" Font-Bold="True" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Code"></asp:Label>
        &nbsp;&nbsp;
        <asp:TextBox ID="TextBox1" runat="server" Font-Bold="True" ReadOnly="True" Width="250px"></asp:TextBox>
        &nbsp;&nbsp;
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="Date"></asp:Label>
&nbsp;<asp:TextBox ID="TextBox3" runat="server" Width="148px" Font-Bold="True" ReadOnly="True" style="text-align: center"></asp:TextBox>
        <br />
        <br />

        <!-- Container to ensure proper alignment -->
        <div class="container">
            <asp:Image ID="Image1" runat="server" Height="149px" Width="341px" CssClass="inlineBlock" />
            &nbsp;
            <asp:TextBox ID="TextBox2" runat="server" BorderStyle="Groove" Font-Bold="True" Height="140px" TextMode="MultiLine" Width="346px" CssClass="inlineBlock"></asp:TextBox>
        </div>

        <br />

        <!-- Panels placed next to each other -->
        <div class="container">
            <asp:Panel ID="Panel1" runat="server" CssClass="inlineBlock" Width="340px">
                <asp:FileUpload ID="FileUpload1" runat="server" accept=".jpeg, .jpg" onchange="previewImage(this);" Width="332px" />
                <br />
                <br />
                <asp:Button ID="Button1" runat="server" Text="Reset image Selection" Width="216px" Font-Bold="True" OnClientClick="resetControls();" />
                <br />
                <br />
                <br />
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Record umber"></asp:Label>
&nbsp;
                <asp:TextBox ID="TextBox4" runat="server" Font-Bold="True" ReadOnly="True" style="text-align: center"></asp:TextBox>
                <br />
                <br />
                <asp:ImageButton ID="ImageButton1" runat="server" Height="34px" ImageUrl="~/Images/submet.jpg" OnClientClick="return validateinputs();" Width="173px" style="margin-top: 11px" OnClick="ImageButton1_Click" />
                &nbsp;&nbsp;
                <br />
                <br />
                <asp:Label ID="Label5" runat="server" Text="Statue"></asp:Label>
                <br />
                <br />
                <asp:ImageButton ID="ImageButton2" runat="server" Enabled="False" Height="27px" OnClick="ImageButton2_Click" Width="153px" ImageUrl="~/Images/clear.png" />
            </asp:Panel>

            &nbsp;

            <asp:Panel ID="Panel2" runat="server" CssClass="inlineBlock" Width="353px">
                <asp:Calendar ID="Calendar1" runat="server" CssClass="inlineBlock" Width="347px" Height="212px">
                    <SelectedDayStyle BackColor="#66FFFF" BorderColor="Black" />
                </asp:Calendar>
            </asp:Panel>
        </div>

    </form>
</body>
</html>

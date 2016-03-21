<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TestWebApp.Default" %>

<%@ Register Assembly="CustomControls" Namespace="Company.CustomControls" TagPrefix="ccc" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" method="post">
        <div>
            <asp:ScriptManager ID="whatever" runat="server"></asp:ScriptManager>

                <ccc:LoginControl ID="LoginControl1" runat="server" PasswordTitle="Password" Layout="Portrait" OnSubmit="LoginControl1_Submit"></ccc:LoginControl>
            <br />
            <hr />
            <br />
                <ccc:LoginControl ID="LoginControl2" runat="server" PasswordTitle="Password" Layout="Portrait" OnSubmit="LoginControl2_Submit"></ccc:LoginControl>
            <br />
            <hr />
            <br />
                <ccc:LoginControl ID="LoginControl3" runat="server" PasswordTitle="Password" Layout="Portrait" OnSubmit="LoginControl3_Submit"></ccc:LoginControl>
            <br />
            <hr />
            <br />
                <ccc:LoginControl ID="LoginControl4" runat="server" PasswordTitle="Password" Layout="Portrait" OnSubmit="LoginControl4_Submit"></ccc:LoginControl>
            <br />
            <hr />
            <br />
                <ccc:LoginControl ID="LoginControl5" runat="server" PasswordTitle="Password" Layout="Portrait" OnSubmit="LoginControl5_Submit"></ccc:LoginControl>
            <br />
            <hr />
            <br />
            Result:
            <asp:Label ID="lblOldEmail" runat="server"></asp:Label>
            <asp:Label ID="lblNewEmail" runat="server"></asp:Label>

        </div>
    </form>
</body>
</html>

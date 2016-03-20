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
    <ccc:LoginControl ID="LoginControl1" runat="server" EmailTitle="Email" PasswordTitle="Password" Layout="Portrait" OnSubmit="LoginControl1_Submit" OnEmailChanged="LoginControl1_EmailChanged"></ccc:LoginControl>
        <asp:Label ID="lblEmail" runat="server"></asp:Label>
        <asp:Label ID="lblEmailChanged" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>

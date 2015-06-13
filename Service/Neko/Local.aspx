<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Local.aspx.cs" Inherits="Service.Neko.Local" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Neko Local Login</title>
    <style>
        div {
            margin: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Image ID="imgYzm" runat="server" />
        </div>
        <div>
            <asp:TextBox ID="boxYzm" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="Submit" runat="server" Text="提交" OnClick="Submit_Click" />
        </div>
    </form>

</body>
</html>

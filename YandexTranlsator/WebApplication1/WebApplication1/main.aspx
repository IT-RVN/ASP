<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="WebApplication1.main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="float:left; background-color:bisque;">
        <asp:Label ID="lblFromLang" runat="server" Text="From Language :"></asp:Label> <br />
        <asp:DropDownList ID="ddlFromLang" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="ddlFromLang_SelectedIndexChanged">
            <asp:ListItem Text="...select language" Value="-1"/>
        </asp:DropDownList>
        <asp:TextBox ID="tbFrom" runat="server" Width="300" Height="100" Text="...enter text here"></asp:TextBox>
    </div>
    <div style="float:none; background-color:aliceblue;">
        <asp:Label ID="lblToLang" runat="server" Text="To Language :"></asp:Label> <br />
        <asp:DropDownList ID="ddlToLang" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="ddlToLang_SelectedIndexChanged">
            <asp:ListItem Text="...select language" Value="-1" />
        </asp:DropDownList>
        <asp:TextBox ID="tbTo" runat="server" Width="300" Height="100" Text="...result translate" ></asp:TextBox>
    </div>
    <div style="margin-left:100px;" >
        <asp:Button ID="Button1" runat="server" Text="Translate" Width="70" Height="100" OnClick="Translate"/>
    </div>
    </form>
</body>
</html>

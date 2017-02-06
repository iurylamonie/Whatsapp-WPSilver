<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnviarMsg.aspx.cs" Inherits="SimuladorWeb.EnviarMsg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBoxRemetente" runat="server" Text="Remetente"></asp:TextBox>
        <asp:TextBox ID="TextBoxUri" runat="server" Text="Uri"></asp:TextBox>
        <br />
        <asp:TextBox ID="TextBoxMsg" runat="server" TextMode="MultiLine"></asp:TextBox><br />
        <asp:Button ID="ButtonEnviar" runat="server" Text="Enviar" OnClick="ButtonEnviar_Click" />
    </div>
    </form>
</body>
</html>

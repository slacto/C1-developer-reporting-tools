<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FixMediaExtensions.aspx.cs" Inherits="Composite.Developers.DeveloperTools" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="style.css" rel="stylesheet" />
</head>
<body>
    <h1>Fix media extensions</h1>
    <form id="form1" runat="server">
    <div>
		<asp:Button ID="btnFixMediaExtensions" runat="server" OnClick="BtnFixMediaExtensionsClick" Text="Fix Media Extensions"/>
		<div id="Messages" runat="server"></div> 
    </div>
    </form>
</body>
</html>

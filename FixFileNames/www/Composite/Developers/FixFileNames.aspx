<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FixFileNames.aspx.cs" Inherits="Composite.Developers.DeveloperTools" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fix file names (Remove "_Published" from DataStore file name)</title>
    <link href="style.css" rel="stylesheet" />
</head>
<body>
    <h1>Fix file names</h1>
    <form id="form1" runat="server">
    <div>
		<asp:Button ID="btnFixFileNames" runat="server" OnClick="BtnFixFileNamesClick" Text="Fix file names"/>
		<div id="Messages" runat="server"></div> 
    </div>
    </form>
</body>
</html>

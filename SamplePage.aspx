<%@ Page Language="C#" CodeFile="SamplePage.aspx.cs" Inherits="SamplePage" AutoEventWireup="true" %>
<html>
<head runat="server" >
   <title>Code-Behind Page Model</title>
</head>
<body>
  <form id="form1" runat="server">
    <div>
       <asp:Label id="Label1" 
         runat="server" Text="Label" >
      </asp:Label>
     <asp:Literal runat="server" ID="LtlText"></asp:Literal>
      <br />
      <asp:Button id="Button1" 
         runat="server" 
         onclick="Button1_Click" 
         Text="Button" >
       </asp:Button>
    </div>
  </form>
</body>
</html>
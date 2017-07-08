<%@ Page Language="C#" Inherits="PageList" CodeFile="PageList.aspx.cs" %>
<!DOCTYPE html>
<html>
<head>
<meta charset="utf-8">

</head>

<body>

<form class="form-inline" runat="server">
  <asp:Literal id="LtlBreadCrumb" runat="server" />

  <asp:dataGrid id="MyDataGrid" runat="server" showHeader="true"
      ShowFooter="false"
      AutoGenerateColumns="false"
      HeaderStyle-CssClass="info thead"
      CssClass="table table-bordered table-hover"
      gridlines="none">
    <Columns>
      <asp:TemplateColumn HeaderText="IP">
        <ItemTemplate>
          <asp:Literal ID="ListItem" runat="server"></asp:Literal>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="left" />
      </asp:TemplateColumn>
      <asp:TemplateColumn>
        <ItemTemplate>
          <asp:Literal ID="EditUrl" runat="server"></asp:Literal>
        </ItemTemplate>
        <ItemStyle Width="50" cssClass="center" />
      </asp:TemplateColumn>
      <asp:TemplateColumn>
        <ItemTemplate>
          <asp:Literal ID="DeleteUrl" runat="server"></asp:Literal>
        </ItemTemplate>
        <ItemStyle Width="50" cssClass="center" />
      </asp:TemplateColumn>
    </Columns>
  </asp:dataGrid>

  <ul class="breadcrumb breadcrumb-button">
    <asp:Button class="btn btn-success" id="AddButton" Text="添 加" runat="server" />
  </ul>

</form>
</body>
</html>

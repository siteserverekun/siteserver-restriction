using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using SiteServer.Restriction;

public partial class PageList : Page
{
    private string _type;
    private Config _config;

    public void Page_Load(object sender, EventArgs e)
    {
        _type = Request.QueryString["type"];
        _config = Main.Api.GetConfig<Config>("Config") ?? new Config
        {
            RestrictionType = ERestrictionTypeUtils.GetValue(ERestrictionType.None),
            BlackList = string.Empty,
            WhiteList = string.Empty
        };

        if (!string.IsNullOrEmpty(Request.QueryString["Delete"]))
        {
            var restriction = Request.QueryString["Restriction"];

            if (_type == "Black")
            {
                var list = new List<string>(_config.BlackList.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries));
                list.Remove(restriction);
                _config.BlackList = string.Join(",", list.ToArray());
            }
            else
            {
                var list = new List<string>(_config.WhiteList.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries));
                list.Remove(restriction);
                _config.WhiteList = string.Join(",", list.ToArray());
            }
            Main.Api.SetConfig("Config", _config);
        }
        if (!IsPostBack)
        {
            BindGrid();

            //AddButton.Attributes.Add("onclick", ModalRestrictionAdd.GetOpenWindowStringToAdd(0, _type));
        }
    }

    public void BindGrid()
    {
        var list = _type == "Black" ? _config.BlackList : _config.WhiteList;
        MyDataGrid.DataSource = new List<string>(list.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries));

        MyDataGrid.ItemDataBound += MyDataGrid_ItemDataBound;
        MyDataGrid.DataBind();
    }

    private static void MyDataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;

        var restriction = e.Item.DataItem as string;
        var listItem = (Literal)e.Item.FindControl("ListItem");
        //var editUrl = e.Item.FindControl("EditUrl") as Literal;
        //var deleteUrl = e.Item.FindControl("DeleteUrl") as Literal;
        listItem.Text = restriction;

        //var showPopWinString = ModalRestrictionAdd.GetOpenWindowStringToEdit(0, _type, restriction);
        //editUrl.Text = $"<a href=\"javascript:;\" onClick=\"{showPopWinString}\">修改</a>";

        //var urlDelete = PageUtils.GetPluginsUrl(nameof(PageRestrictionList), new NameValueCollection
        //{
        //    {"Delete", "True"},
        //    {"Type", _type},
        //    {"Restriction", restriction}
        //});

        //deleteUrl.Text =
        //    $"<a href=\"{urlDelete}\" onClick=\"javascript:return confirm('此操作将删除IP访问规则“{restriction}”，确认吗？');\">删除</a>";
    }

}
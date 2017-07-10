using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SiteServer.Restriction
{
    public class PageOptions : Page
    {
        public Literal LtlMessage;
        public RadioButtonList RblRestrictionType;

        private Config _config;

        public void Page_Load(object sender, EventArgs e)
        {
            if (!Main.Api.IsAuthorized())
            {
                HttpContext.Current.Response.Write("<h1>未授权访问</h1>");
                HttpContext.Current.Response.End();
                return;
            }

            _config = Main.GetConfig();

            if (!IsPostBack)
            {
                RblRestrictionType.Items.Add(new ListItem
                {
                    Text = ERestrictionTypeUtils.GetText(ERestrictionType.None),
                    Value = ERestrictionTypeUtils.GetValue(ERestrictionType.None),
                    Selected = ERestrictionTypeUtils.Equals(ERestrictionType.None, _config.RestrictionType)
                });
                RblRestrictionType.Items.Add(new ListItem
                {
                    Text = ERestrictionTypeUtils.GetText(ERestrictionType.BlackList),
                    Value = ERestrictionTypeUtils.GetValue(ERestrictionType.BlackList),
                    Selected = ERestrictionTypeUtils.Equals(ERestrictionType.BlackList, _config.RestrictionType)
                });
                RblRestrictionType.Items.Add(new ListItem
                {
                    Text = ERestrictionTypeUtils.GetText(ERestrictionType.WhiteList),
                    Value = ERestrictionTypeUtils.GetValue(ERestrictionType.WhiteList),
                    Selected = ERestrictionTypeUtils.Equals(ERestrictionType.WhiteList, _config.RestrictionType)
                });
            }
        }

        public void Submit_OnClick(object sender, EventArgs e)
        {
            if (!Page.IsPostBack || !Page.IsValid) return;

            _config.RestrictionType = RblRestrictionType.SelectedValue;

            try
            {
                Main.SetConfig(_config);

                LtlMessage.Text = @"<div class=""alert alert-primary"" role=""alert"">访问限制选项修改成功！</div>";
            }
            catch (Exception ex)
            {
                LtlMessage.Text = $@"<div class=""alert alert-danger"" role=""alert"">访问限制选项修改失败：{ex.Message}！</div>";
            }
        }
    }
}
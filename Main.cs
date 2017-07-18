using System;
using System.Collections.Generic;
using System.Web;
using SiteServer.Plugin;
using SiteServer.Plugin.Hooks;

namespace SiteServer.Restriction
{
    public class Main : PluginBase, IMenu
    {
        private static Config _config;

        public const string Permission = "";

        public static IPublicApi Api { get; set; }

        public static void SetConfig(Config config)
        {
            _config = config;
            Api.SetGlobalConfig(nameof(Config), config);
        }

        public static Config GetConfig()
        {
            return _config;
        }

        public override void Active(PluginContext context)
        {
            Api = context.Api;
            _config = Api.GetGlobalConfig<Config>(nameof(Config)) ?? new Config
            {
                RestrictionType = ERestrictionTypeUtils.GetValue(ERestrictionType.None),
                BlackList = string.Empty,
                WhiteList = string.Empty
            };
            if (_config.BlackList == null)
            {
                _config.BlackList = string.Empty;
            }
            if (_config.WhiteList == null)
            {
                _config.WhiteList = string.Empty;
            }
        }

        public override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);

            if (RestrictionManager.IsVisitAllowed(_config)) return;

            HttpContext.Current.Response.Write("<h1>禁止访问</h1>");
            HttpContext.Current.Response.Write($"<p>IP地址：{RestrictionManager.GetIpAddress()}<br />需要访问后台请与网站管理员联系开通相关权限.</p>");
            HttpContext.Current.Response.End();
        }

        public override PluginMenu GetTopMenu()
        {
            return new PluginMenu
            {
                Text = "后台访问限制",
                Menus = new List<PluginMenu>
                {
                    new PluginMenu
                    {
                        Text = "黑名单",
                        Href = "PageList.aspx?type=Black"
                    },
                    new PluginMenu
                    {
                        Text = "白名单",
                        Href = "PageList.aspx?type=White"
                    },
                    new PluginMenu
                    {
                        Text = "访问限制选项",
                        Href = "PageOptions.aspx"
                    }
                }
            };
        }
    }
}
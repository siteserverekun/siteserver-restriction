using System;
using System.Collections.Generic;
using SiteServer.Plugin;
using SiteServer.Plugin.Hooks;

namespace SiteServer.Restriction
{
    public class Main : IPlugin, IRestful, IMenu
    {
        private Config _config;

        public static IPublicApi Api { get; private set; }

        public void Active(PluginContext context)
        {
            Api = context.Api;
            _config = Api.GetConfig<Config>(nameof(Config));
        }

        public void Deactive(PluginContext context)
        {

        }

        public object Get(IRequestContext context, string name, int id)
        {
            return _config;
        }

        public object Put(IRequestContext context, string name, int id)
        {
            throw new NotImplementedException();
        }

        public object Delete(IRequestContext context, string name, int id)
        {
            throw new NotImplementedException();
        }

        public object Patch(IRequestContext context, string name, int id)
        {
            throw new NotImplementedException();
        }

        public object Post(IRequestContext context, string name, int id)
        {
            var config = new Config
            {
                RestrictionType = ERestrictionTypeUtils.GetValue(ERestrictionTypeUtils.GetEnumType(context.GetPostString("restrictionType"))),
                BlackList = context.GetPostString("blackList"),
                WhiteList = context.GetPostString("whiteList")
            };

            Api.SetConfig(nameof(Config), config);
            _config = config;

            return config;
        }

        public PluginMenu GetTopMenu()
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

        public PluginMenu GetSiteMenu(int siteId)
        {
            return null;
        }
    }
}
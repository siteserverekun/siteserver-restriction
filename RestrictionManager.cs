using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

namespace SiteServer.Restriction
{
	public class RestrictionManager
	{
        private RestrictionManager()
		{
		}

        public static bool Contains(string text, string inner)
        {
            return text?.IndexOf(inner, StringComparison.Ordinal) >= 0;
        }

        public static int GetCount(string innerText, string content)
        {
            if (innerText == null || content == null)
            {
                return 0;
            }
            var count = 0;
            for (var index = content.IndexOf(innerText, StringComparison.Ordinal); index != -1; index = content.IndexOf(innerText, index + innerText.Length, StringComparison.Ordinal))
            {
                count++;
            }
            return count;
        }

        public static bool IsIpAddress(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        public static string GetIpAddress()
        {
            var result = string.Empty;

            try
            {
                //取CDN用户真实IP的方法
                //当用户使用代理时，取到的是代理IP
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(result))
                {
                    //可能有代理
                    if (result.IndexOf(".", StringComparison.Ordinal) == -1)
                        result = null;
                    else
                    {
                        if (result.IndexOf(",", StringComparison.Ordinal) != -1)
                        {
                            result = result.Replace("  ", "").Replace("'", "");
                            var temparyip = result.Split(",;".ToCharArray());
                            foreach (var t in temparyip)
                            {
                                if (IsIpAddress(t) && t.Substring(0, 3) != "10." && t.Substring(0, 7) != "192.168" && t.Substring(0, 7) != "172.16.")
                                {
                                    result = t;
                                }
                            }
                            var str = result.Split(',');
                            if (str.Length > 0)
                                result = str[0].Trim();
                        }
                        else if (IsIpAddress(result))
                            return result;
                    }
                }

                if (string.IsNullOrEmpty(result))
                    result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                if (string.IsNullOrEmpty(result))
                    result = HttpContext.Current.Request.UserHostAddress;
                if (string.IsNullOrEmpty(result))
                    result = "localhost";

                if (result == "::1")
                {
                    result = "localhost";
                }
            }
            catch
            {
                // ignored
            }

            return result;
        }

        public static bool IsVisitAllowed(Config config)
        {
            var restrictionType = ERestrictionTypeUtils.GetEnumType(config.RestrictionType);
            if (restrictionType == ERestrictionType.None) return true;

            var restrictionList = new List<string>();
            if (restrictionType == ERestrictionType.BlackList)
            {
                restrictionList = new List<string>(config.BlackList.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries));
            }
            else if (restrictionType == ERestrictionType.WhiteList)
            {
                restrictionList = new List<string>(config.WhiteList.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries));
            }
            return IsVisitAllowed(restrictionType, restrictionList);
        }

        private static bool IsVisitAllowed(ERestrictionType restrictionType, List<string> restrictionList)
        {
            var isAllowed = true;
            if (restrictionType != ERestrictionType.None)
            {
                var userIp = GetIpAddress();
                if (restrictionType == ERestrictionType.BlackList)
                {
                    var list = new IpList();
                    foreach (var restriction in restrictionList)
                    {
                        AddRestrictionToIpList(list, restriction);
                    }
                    if (list.CheckNumber(userIp))
                    {
                        isAllowed = false;
                    }
                }
                else if (restrictionType == ERestrictionType.WhiteList)
                {
                    if (restrictionList.Count > 0)
                    {
                        isAllowed = false;
                        var list = new IpList();
                        foreach (var restriction in restrictionList)
                        {
                            AddRestrictionToIpList(list, restriction);
                        }
                        if (list.CheckNumber(userIp))
                        {
                            isAllowed = true;
                        }
                    }
                }
            }
            return isAllowed;
        }

        private static void AddRestrictionToIpList(IpList list, string restriction)
        {
            if (string.IsNullOrEmpty(restriction)) return;

            if (Contains(restriction, "-"))
            {
                restriction = restriction.Trim(' ', '-');
                var arr = restriction.Split('-');
                list.AddRange(arr[0].Trim(), arr[1].Trim());
            }
            else if (Contains(restriction, "*"))
            {
                var ipPrefix = restriction.Substring(0, restriction.IndexOf('*'));
                ipPrefix = ipPrefix.Trim(' ', '.');
                var dotNum = GetCount(".", ipPrefix);

                string ipNumber;
                string mask;
                if (dotNum == 0)
                {
                    ipNumber = ipPrefix + ".0.0.0";
                    mask = "255.0.0.0";
                }
                else if (dotNum == 1)
                {
                    ipNumber = ipPrefix + ".0.0";
                    mask = "255.255.0.0";
                }
                else
                {
                    ipNumber = ipPrefix + ".0";
                    mask = "255.255.255.0";
                }
                list.Add(ipNumber, mask);
            }
            else
            {
                list.Add(restriction);
            }
        }
	}
}

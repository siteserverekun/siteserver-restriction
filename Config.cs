using Newtonsoft.Json;

namespace SiteServer.Restriction
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Config
    {
        public string RestrictionType { get; set; }
        public string BlackList { get; set; }
        public string WhiteList { get; set; }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commander.Services
{
    public class ConfigData
    {
        public string FileName { get; set; }
        public List<ArgumentInfo> Arguments { get; set; } = new List<ArgumentInfo>();

        public string IEProxy { get; set; } = "all";
        public string IEProxyPort { get; set; } = "2080";
        public string IEProxyMode { get; set; } = "N";
        public int PACPort { get; set; } = 34567;

        [JsonIgnore]
        public string IEProxyString
        {
            get
            {
                return IEProxy == string.Empty || IEProxy.ToLower() == "all"
                    ? "127.0.0.1:" + IEProxyPort : IEProxy + "=127.0.0.1:" + IEProxyPort;
            }
        }
    }

    public class ArgumentInfo
    {
        public bool Checked { get; set; }
        public string AliasName { get; set; }
        public string Arguments { get; set; }
    }
}


using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

namespace Commander.Services
{
    public class GFWListUpdater
    {
        public const string PAC_FILE = "pac.txt";
        public const string USER_RULE_FILE = "user-rule.txt";
        private const string GFWLIST_URL = "https://raw.githubusercontent.com/gfwlist/gfwlist/master/gfwlist.txt";

        private static readonly IEnumerable<char> IgnoredLineBegins = new char[]
        {
            '!',
            '['
        };

        private void http_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                var list = new List<string>();
                if (File.Exists("user-rule.txt"))
                {
                    list.AddRange(File.ReadAllLines("user-rule.txt").Where(
                        x => !string.IsNullOrEmpty(x) && !IgnoredLineBegins.Contains(x.First())));
                }

                var text = Encoding.ASCII.GetString(Convert.FromBase64String(e.Result));
                var result = text.Split(new[] { '\r', '\n' }).Where(x => !string.IsNullOrEmpty(x) && !IgnoredLineBegins.Contains(x.First())).ToList();
                list.AddRange(result);

                var templete = string.Empty;
                var assembly = Assembly.GetExecutingAssembly();
                var abpjsName = assembly.GetManifestResourceNames().FirstOrDefault(x => x.EndsWith("abp.js"));

                using (var ms = assembly.GetManifestResourceStream(abpjsName))
                {
                    templete = new StreamReader(ms).ReadToEnd();
                }

                var json = JsonConvert.SerializeObject(list, Formatting.Indented);
                templete = templete.Replace("__RULES__", json);

                File.WriteAllText(PAC_FILE, templete, Encoding.UTF8);
                _success?.Invoke();
            }
            catch (Exception ex)
            {
                _error?.Invoke(ex);
            }
        }

        Action _success;
        Action<Exception> _error;

        public void UpdatePACFromGFWList(Action<Exception> error = null, Action success = null)
        {
            _error = error;
            _success = success;

            var webClient = new WebClient();
            webClient.Proxy = new WebProxy(IPAddress.Loopback.ToString(), int.Parse(Program.Conifg.Data.IEProxyPort));
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(this.http_DownloadStringCompleted);
            webClient.DownloadStringAsync(new Uri(GFWLIST_URL));
        }
    }
}

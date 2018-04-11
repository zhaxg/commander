using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading; 

namespace Commander.Services
{
    public class SimpleHTTPServer
    {
        static SimpleHTTPServer _instance;
        public static SimpleHTTPServer GetOrStartDefaultServer()
        {
            return _instance ??
                (_instance = new SimpleHTTPServer(AppDomain.CurrentDomain.BaseDirectory, Program.Conifg.Data.PACPort));
        }

        private readonly string[] _indexFiles = {
        "index.html",
        "index.htm",
        "default.html",
        "default.htm"    };

        private static IDictionary<string, string> _mimeTypeMappings =
            new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) {
        #region extension to MIME type list
        {".asf", "video/x-ms-asf"},
        {".asx", "video/x-ms-asf"},
        {".avi", "video/x-msvideo"},
        {".bin", "application/octet-stream"},
        {".cco", "application/x-cocoa"},
        {".crt", "application/x-x509-ca-cert"},
        {".css", "text/css"},
        {".deb", "application/octet-stream"},
        {".der", "application/x-x509-ca-cert"},
        {".dll", "application/octet-stream"},
        {".dmg", "application/octet-stream"},
        {".ear", "application/java-archive"},
        {".eot", "application/octet-stream"},
        {".exe", "application/octet-stream"},
        {".flv", "video/x-flv"},
        {".gif", "image/gif"},
        {".hqx", "application/mac-binhex40"},
        {".htc", "text/x-component"},
        {".htm", "text/html"},
        {".html", "text/html"},
        {".ico", "image/x-icon"},
        {".img", "application/octet-stream"},
        {".iso", "application/octet-stream"},
        {".jar", "application/java-archive"},
        {".jardiff", "application/x-java-archive-diff"},
        {".jng", "image/x-jng"},
        {".jnlp", "application/x-java-jnlp-file"},
        {".jpeg", "image/jpeg"},
        {".jpg", "image/jpeg"},
        {".js", "application/x-javascript"},
        {".mml", "text/mathml"},
        {".mng", "video/x-mng"},
        {".mov", "video/quicktime"},
        {".mp3", "audio/mpeg"},
        {".mpeg", "video/mpeg"},
        {".mpg", "video/mpeg"},
        {".msi", "application/octet-stream"},
        {".msm", "application/octet-stream"},
        {".msp", "application/octet-stream"},
        {".pdb", "application/x-pilot"},
        {".pdf", "application/pdf"},
        {".pem", "application/x-x509-ca-cert"},
        {".pl", "application/x-perl"},
        {".pm", "application/x-perl"},
        {".png", "image/png"},
        {".prc", "application/x-pilot"},
        {".ra", "audio/x-realaudio"},
        {".rar", "application/x-rar-compressed"},
        {".rpm", "application/x-redhat-package-manager"},
        {".rss", "text/xml"},
        {".run", "application/x-makeself"},
        {".sea", "application/x-sea"},
        {".shtml", "text/html"},
        {".sit", "application/x-stuffit"},
        {".swf", "application/x-shockwave-flash"},
        {".tcl", "application/x-tcl"},
        {".tk", "application/x-tcl"},
        {".txt", "text/plain"},
        {".war", "application/java-archive"},
        {".wbmp", "image/vnd.wap.wbmp"},
        {".wmv", "video/x-ms-wmv"},
        {".xml", "text/xml"},
        {".xpi", "application/x-xpinstall"},
        {".zip", "application/zip"},
        #endregion
    };

        private Thread _serverThread;
        private string _rootDirectory;
        private HttpListener _listener;
        private int _port;

        public int Port { get => _port; }
        public string PACUrl { get => $"http://127.0.0.1:{Port}/{GFWListUpdater.PAC_FILE}"; }

        public SimpleHTTPServer(string path, int port)
        {
            this.Initialize(path, port);
        }

        public SimpleHTTPServer(string path)
        {
            //get an empty port
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            this.Initialize(path, port);
        }

        public void Stop()
        {
            _serverThread.Abort();
            _listener.Stop();
        }

        private void Listen()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add("http://*:" + _port.ToString() + "/");
            _listener.Start();
            while (true)
            {
                try
                {
                    HttpListenerContext context = _listener.GetContext();
                    Process(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private void Process(HttpListenerContext context)
        {
            var filename = context.Request.Url.AbsolutePath;
            if (filename.EndsWith(GFWListUpdater.PAC_FILE))
            {
                SendPacResponse(context);
            }
        }

        private void Initialize(string path, int port)
        {
            this._rootDirectory = path;
            this._port = port;
            _serverThread = new Thread(this.Listen);
            _serverThread.Start();
        }

        public void SendPacResponse(HttpListenerContext context)
        {
            var port = Program.Conifg.Data.IEProxyPort;
            var proxy = "socks".Equals(Program.Conifg.Data.IEProxy, StringComparison.OrdinalIgnoreCase)
                ? "SOCKS5" : "PROXY";

            var text = File.ReadAllText(GFWListUpdater.PAC_FILE).Replace("__PROXY__", $"{proxy} 127.0.0.1:{port};");
            var bytes = Encoding.UTF8.GetBytes(text);

            context.Response.ContentType = "application/x-ns-proxy-autoconfig";
            context.Response.ContentLength64 = bytes.Length;

            context.Response.OutputStream.Write(bytes, 0, bytes.Length);
            context.Response.OutputStream.Flush();
            context.Response.OutputStream.Close();
        }
    }
}

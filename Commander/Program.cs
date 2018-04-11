using Commander.Services;
using Commander.Views;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Commander
{
    static class Program
    {
        static JsonConfig<ConfigData> _cfg;
        public static JsonConfig<ConfigData> Conifg
        {
            get
            {
                var executable = new FileInfo(Application.ExecutablePath);
                return _cfg ?? (_cfg = new JsonConfig<ConfigData>(executable.Name));
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.ThreadException += Application_ThreadException;

            Task.Factory.StartNew(() =>
            {
                SimpleHTTPServer.GetOrStartDefaultServer();
            });

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            SimpleHTTPServer.GetOrStartDefaultServer().Stop();
            MessageBox.Show(e.Exception?.ToString());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            SimpleHTTPServer.GetOrStartDefaultServer().Stop();
            MessageBox.Show(e.ExceptionObject?.ToString());
        }
    }
}

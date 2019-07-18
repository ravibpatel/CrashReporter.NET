using System;
using System.Net;
using System.Windows.Forms;
using CrashReporterDotNET;

namespace CrashReporterTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += (sender, args) => SendReport(args.Exception);
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                {
                    SendReport((Exception) args.ExceptionObject);
                };
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

        public static void SendReport(Exception exception, string developerMessage = "", bool silent = false)
        {
            var reportCrash = new ReportCrash("Email where you want to receive crash reports.")
            {
                DeveloperMessage = developerMessage,
                Silent = silent,
                DoctorDumpSettings = new DoctorDumpSettings
                {
                    WebProxy = new WebProxy("Web proxy address, if needed"),
                    ApplicationID = new Guid("Application ID you received from DrDump.com"),
                    OpenReportInBrowser = true
                }
            };
            reportCrash.Send(exception);
        }
    }
}

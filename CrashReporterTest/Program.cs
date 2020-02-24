using System;
using System.Net;
using System.Windows.Forms;
using CrashReporterDotNET;

namespace CrashReporterTest
{
    static class Program
    {
        private static ReportCrash _reportCrash;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += (sender, args) => SendReport(args.Exception);
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                {
                    SendReport((Exception)args.ExceptionObject);
                };
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            _reportCrash = new ReportCrash("Email where you want to receive crash reports")
            {
                DeveloperMessage = "Retry attempt",
                Silent = true,
                WebProxy = new WebProxy("Web proxy address, if needed"),
                DoctorDumpSettings = new DoctorDumpSettings
                {
                    ApplicationID = new Guid("Application ID you received from DrDump.com"),
                    OpenReportInBrowser = true
                }
            };
            _reportCrash.RetryFailedReports();
            Application.Run(new FormMain());
        }

        public static void SendReport(Exception exception, string developerMessage = "", bool silent = false)
        {
            _reportCrash.DeveloperMessage = developerMessage;
            _reportCrash.Silent = silent;
            _reportCrash.Send(exception);
        }
    }
}

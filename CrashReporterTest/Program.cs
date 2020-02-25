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
                Silent = true,
                ShowScreenshotTab = true,
                IncludeScreenshot = false,
                #region Optional Configuration
                WebProxy = new WebProxy("Web proxy address, if needed"),
                AnalyzeWithDoctorDump = true,
                DoctorDumpSettings = new DoctorDumpSettings
                {
                    ApplicationID = new Guid("Application ID you received from DrDump.com"),
                    OpenReportInBrowser = true
                }
                #endregion
            };
            _reportCrash.RetryFailedReports();
            Application.Run(new FormMain());
        }

        public static void SendReport(Exception exception, string developerMessage = "")
        {
            _reportCrash.DeveloperMessage = developerMessage;
            _reportCrash.Silent = false;
            _reportCrash.Send(exception);
        }

        public static void SendReportSilently(Exception exception, string developerMessage = "")
        {
            _reportCrash.DeveloperMessage = developerMessage;
            _reportCrash.Silent = true;
            _reportCrash.Send(exception);
        }
    }
}

using System;
using System.Globalization;
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
            Application.ThreadException += (sender, args) => SendCrashReport(args.Exception);
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                {
                    SendCrashReport((Exception) args.ExceptionObject);
                    Environment.Exit(0);
                };
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

        public static void SendCrashReport(Exception exception, string developerMessage = "")
        {
            var reportCrash = new ReportCrash
            {
                CurrentCulture = new CultureInfo("en-US"),
                AnalyzeWithDoctorDump = true,
                DeveloperMessage = developerMessage,
                ToEmail = "Email where you want to receive crash reports.",
                DoctorDumpSettings = new DoctorDumpSettings
                {
                    ApplicationID = new Guid("Application ID you received from DrDump.com"),
                    OpenReportInBrowser = true
                },
            };

            reportCrash.Send(exception);
        }
    }
}

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
                FromEmail = "Your Gmail Address",
                ToEmail = "Email where you want to recieve crash reports.",
                SmtpHost = "smtp.gmail.com",
                Port = 587,
                UserName = "Your Gmail Address",
                Password = "Your Gmail Password",
                EnableSSL = true,
                EmailRequired = true,
                CaptureScreen = false,
                CurrentCulture = CultureInfo.CreateSpecificCulture("nl"),
                DeveloperMessage = developerMessage
            };

            reportCrash.Send(exception);
        }
    }
}

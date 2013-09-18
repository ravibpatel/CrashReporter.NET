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
                FromEmail = "your gmail",
                ToEmail = "email where you want to recieve crash reports.",
                SmtpHost = "smtp.gmail.com",
                Port = 587,
                UserName = "your gmail",
                Password = "your gmail password",
                EnableSSL = true,
                EmailRequired = true,
                CurrentCulture = CultureInfo.CreateSpecificCulture("es"),
                DeveloperMessage = developerMessage
            };

            reportCrash.Send(exception);
        }
    }
}

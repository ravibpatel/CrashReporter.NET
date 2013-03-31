using System;
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
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
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

        public static void SendCrashReport(Exception exception)
        {
            var reportCrash = new ReportCrash
            {
                FromEmail = "mysmtpmail@gmail.com",
                ToEmail = "support@rbsoft.org",
                SmtpHost = "smtp.gmail.com",
                Port = 587,
                UserName = "mysmtpmail@gmail.com",
                Password = "mypass",
                EnableSSL = true,
            };
            reportCrash.Send(exception);
        }
    }
}

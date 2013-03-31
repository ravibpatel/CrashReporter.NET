using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading;

namespace CrashReporterDotNET
{
    public class ReportCrash
    {
        public Boolean EnableSSL;
        public String FromEmail;
        public String Password = "";

        public int Port = 25;
        public String SmtpHost;
        public String ToEmail;

        public String UserName = "";

        public void Send(Exception exception)
        {
            try
            {
                var captureScreenshot = new CaptureScreenshot();
                captureScreenshot.CaptureScreenToFile(
                    string.Format(@"{0}\{1}CrashScreenshot.png", Path.GetTempPath(),
                                  Assembly.GetEntryAssembly().GetName().Name), ImageFormat.Png);
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
            }
            if (String.IsNullOrEmpty(FromEmail) || String.IsNullOrEmpty(ToEmail) || String.IsNullOrEmpty(SmtpHost))
                return;
            var fromAddress = new MailAddress(FromEmail);
            var toAddress = new MailAddress(ToEmail);

            var smtp = new SmtpClient
                {
                    Host = SmtpHost,
                    Port = Port,
                    EnableSsl = EnableSSL,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(UserName, Password),
                };

            var crashReport = new CrashReport(exception, fromAddress, toAddress, smtp);

            var parameterizedThreadStart = new ParameterizedThreadStart(ShowUI);
            var thread = new Thread(parameterizedThreadStart);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(crashReport);
            thread.Join();
        }

        public static void ShowUI(object crashReportDialog)
        {
            ((CrashReport) crashReportDialog).ShowDialog();
        }
    }
}
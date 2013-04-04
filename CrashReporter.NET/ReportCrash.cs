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
    /// <summary>
    /// Set SMTP server details and receiver email fieds of this class instance to send crash reports directly in your inbox.
    /// </summary>
    public class ReportCrash
    {
        /// <summary>
        /// Gets or Sets name or IP address of the Host used for SMTP transactions.
        /// </summary>
        public String SmtpHost;

        /// <summary>
        /// Specify whether the SMTP client uses the Secure Socket Layer (SSL) to encrypt the connection.
        /// </summary>
        public Boolean EnableSSL;

        /// <summary>
        /// Gets or Sets the port used for SMTP transactions.
        /// </summary>
        public int Port = 25;

        /// <summary>
        /// Gets or Sets the username used for SMTP transactions.
        /// </summary>
        public String UserName = "";

        /// <summary>
        /// Gets or Sets the password used for SMTP transactions. 
        /// </summary>
        public String Password = "";

        /// <summary>
        /// Gets or Sets email address where you want to receive crash reports.
        /// </summary>
        public String ToEmail;

        /// <summary>
        /// Gets or Sets email address used by crash reporter if user don't provide her email address.
        /// </summary>
        public String FromEmail;

        /// <summary>
        /// Sends exception report directly to receiver email address provided in ToEmail.
        /// </summary>
        /// <param name="exception">Exception object that contains details of the exception.</param>
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

        private static void ShowUI(object crashReportDialog)
        {
            ((CrashReport) crashReportDialog).ShowDialog();
        }
    }
}
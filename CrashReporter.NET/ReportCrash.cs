using System;
using System.Deployment.Application;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using CrashReporterDotNET.DrDump;
using Application = System.Windows.Forms.Application;

namespace CrashReporterDotNET
{
    /// <summary>
    /// Set SMTP server details and receiver email fields of this class instance to send crash reports directly in your inbox.
    /// </summary>
    public class ReportCrash
    {
        /// <summary>
        /// Set it to true if you want to send whole crash report silently.
        /// </summary>
        public bool Silent = false;

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
        /// Gets or Sets exception that occur during application execution.
        /// </summary>
        public Exception Exception;

        /// <summary>
        /// Specify whether CrashReporter.NET should take screen shot of whole screen or not.
        /// </summary>
        public bool CaptureScreen = false;

        /// <summary>
        /// Gets or Sets custom message developer wants to send. It can be something like value of variables or other details you want to send.
        /// </summary>
        public String DeveloperMessage = "";

        /// <summary>
        ///  Gets or Sets if email is required to send the crash report.
        /// </summary>
        public bool EmailRequired = false;

        /// <summary>
        /// Gets or Sets "Include screenshot" start value.
        /// </summary>
        public bool IncludeScreenshot = true;

        /// <summary>
        /// Specify whether CrashReporter.NET should send crash reports only for new problems (duplicates detected by Doctor Dump free cloud service).
        /// </summary>
        public bool AnalyzeWithDoctorDump = true;

        /// <summary>
        /// Specify Doctor Dump processing settings. Used only when AnalyzeWithDoctorDump is true.
        /// </summary>
        public DoctorDumpSettings DoctorDumpSettings = new DoctorDumpSettings();

        internal string ApplicationTitle;

        internal string ApplicationVersion;

        internal string ScreenShot;

        private DrDumpService _doctorDumpService;

        /// <summary>
        /// Object use to send exception report to your Inbox.
        /// </summary>
        /// <param name="toEmail">Email where you want to receive crash reports.</param>
        public ReportCrash(string toEmail)
        {
            ToEmail = toEmail;
        }

        /// <summary>
        /// Sends exception report silently to receiver email address provided in ToEmail.
        /// </summary>
        /// <param name="exception">Exception object that contains details of the exception.</param>
        public void SendSilently(Exception exception)
        {
            Send(exception, true);
        }

        /// <summary>
        /// Sends exception report directly to receiver email address provided in ToEmail.
        /// </summary>
        /// <param name="exception">Exception object that contains details of the exception.</param>
        public void Send(Exception exception)
        {
            Send(exception, Silent);
        }

        private void Send(Exception exception, bool silent)
        {
            Exception = exception;

            var mainAssembly = Assembly.GetEntryAssembly();
            string appTitle = null;
            var attributes = mainAssembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), true);
            if (attributes.Length > 0)
            {
                appTitle = ((AssemblyTitleAttribute) attributes[0]).Title;
            }

            ApplicationTitle = !string.IsNullOrEmpty(appTitle) ? appTitle : mainAssembly.GetName().Name;
            ApplicationVersion = ((Type.GetType("Mono.Runtime") == null) && ApplicationDeployment.IsNetworkDeployed)
                ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
                : mainAssembly.GetName().Version.ToString();
            try
            {
                ScreenShot = $@"{Path.GetTempPath()}\{ApplicationTitle} Crash Screenshot.png";
                if (CaptureScreen)
                    CaptureScreenshot.CaptureScreen(ScreenShot, ImageFormat.Png);
                else
                    CaptureScreenshot.CaptureActiveWindow(ScreenShot, ImageFormat.Png);
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
            }

            if (!AnalyzeWithDoctorDump)
            {
                if (string.IsNullOrEmpty(FromEmail))
                {
                    throw new ArgumentNullException(@"FromEmail");
                }

                if (string.IsNullOrEmpty(SmtpHost))
                {
                    throw new ArgumentNullException("SmtpHost");
                }
            }

            if (!Application.MessageLoop)
            {
                Application.EnableVisualStyles();
            }

            if (silent)
            {
                SendReport(IncludeScreenshot);
            }
            else
            {
                if (Thread.CurrentThread.GetApartmentState().Equals(ApartmentState.MTA))
                {
                    var thread = new Thread(() => new CrashReport(this).ShowDialog()) {IsBackground = false};
                    thread.CurrentCulture = thread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                    thread.Join();
                }
                else
                {
                    new CrashReport(this).ShowDialog();
                }
            }
        }

        internal void SendReport(bool includeScreenshot,
            DrDumpService.SendRequestCompletedEventHandler sendRequestCompleted = null,
            SendCompletedEventHandler smtpClientSendCompleted = null, Control form = null, string from = "",
            string userMessage = "")
        {
            string subject = String.Empty;
            if (string.IsNullOrEmpty(from))
            {
                from = !string.IsNullOrEmpty(FromEmail)
                    ? FromEmail
                    : null;
            }
            else
            {
                subject = $"{ApplicationTitle} {ApplicationVersion} Crash Report by {from}";
            }

            if (AnalyzeWithDoctorDump)
            {
                SendFullReport(includeScreenshot, sendRequestCompleted, form, from, userMessage);
            }
            else
            {
                SendEmail(includeScreenshot, smtpClientSendCompleted, from, subject, userMessage);
            }
        }

        #region Send Email Using SMTP

        private void SendEmail(bool includeScreenshot, SendCompletedEventHandler smtpClientSendCompleted, string from,
            string subject, string userMessage)
        {
            if (string.IsNullOrEmpty(subject))
            {
                subject = $"{ApplicationTitle} {ApplicationVersion} Crash Report";
            }

            var smtpClient = new SmtpClient
            {
                Host = SmtpHost,
                Port = Port,
                EnableSsl = EnableSSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(UserName, Password),
            };

            var message = new MailMessage(new MailAddress(from), new MailAddress(ToEmail))
            {
                IsBodyHtml = true,
                Subject = subject,
                Body = CreateHtmlReport(userMessage)
            };

            if (File.Exists(ScreenShot) && includeScreenshot)
            {
                message.Attachments.Add(new Attachment(ScreenShot));
            }

            if (smtpClientSendCompleted != null)
            {
                smtpClient.SendCompleted += smtpClientSendCompleted;
                smtpClient.SendAsync(message, "Crash Report");
            }
            else
            {
                smtpClient.Send(message);
            }
        }

        #endregion

        #region HTML Report Generator

        internal string CreateHtmlReport(string userMessage)
        {
            string report =
                string.Format(
                    @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
                    <html xmlns=""http://www.w3.org/1999/xhtml"">
                    <head>
                    <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />
                    <title>{0} {1} Crash Report</title>
                    <style type=""text/css"">
                    .message {{
                    padding-top:5px;
                    padding-bottom:5px;
                    padding-right:20px;
                    padding-left:20px;
                    font-family:Sans-serif;
                    }}
                    .content
                    {{
                    border-style:dashed;
                    border-width:1px;
                    }}
                    .title
                    {{
                    padding-top:1px;
                    padding-bottom:1px;
                    padding-right:10px;
                    padding-left:10px;
                    font-family:Arial;
                    }}
                    </style>
                    </head>
                    <body>
                    <div class=""title"" style=""background-color: #FFCC99"">
                    <h2>{0} {1} Crash Report</h2>
                    </div>
                    <br/>
                    <div class=""content"">
                    <div class=""title"" style=""background-color: #66CCFF;"">
                    <h3>Windows Version</h3>
                    </div>
                    <div class=""message"">
                    <p>{2}</p>
                    </div>
                    </div>
                    <br/>
                    <div class=""content"">
                    <div class=""title"" style=""background-color: #66CCFF;"">
                    <h3>CLR Version</h3>
                    </div>
                    <div class=""message"">
                    <p>{3}</p>
                    </div>
                    </div>
                    <br/>    
                    <div class=""content"">
                    <div class=""title"" style=""background-color: #66CCFF;"">
                    <h3>Exception</h3>
                    </div>
                    <div class=""message"">
                    {4}
                    </div>
                    </div>", HttpUtility.HtmlEncode(ApplicationTitle),
                    HttpUtility.HtmlEncode(ApplicationVersion),
                    HttpUtility.HtmlEncode(HelperMethods.GetWindowsVersion()),
                    HttpUtility.HtmlEncode(Environment.Version.ToString()),
                    CreateReport(Exception));
            if (!String.IsNullOrEmpty(userMessage))
            {
                report += $@"<br/>
                            <div class=""content"">
                            <div class=""title"" style=""background-color: #66FF99;"">
                            <h3>User Comment</h3>
                            </div>
                            <div class=""message"">
                            <p>{HttpUtility.HtmlEncode(userMessage)}</p>
                            </div>
                            </div>";
            }

            if (!String.IsNullOrEmpty(DeveloperMessage.Trim()))
            {
                report += $@"<br/>
                            <div class=""content"">
                            <div class=""title"" style=""background-color: #66FF99;"">
                            <h3>Developer Message</h3>
                            </div>
                            <div class=""message"">
                            <p>{HttpUtility.HtmlEncode(DeveloperMessage.Trim())}</p>
                            </div>
                            </div>";
            }

            report += "</body></html>";
            return report;
        }

        private string CreateReport(Exception exception)
        {
            string report = $@"<br/>
                        <div class=""content"">
                        <div class=""title"" style=""background-color: #66CCFF;"">
                        <h3>Exception Type</h3>
                        </div>
                        <div class=""message"">
                        <p>{HttpUtility.HtmlEncode(exception.GetType().ToString())}</p>
                        </div>
                        </div><br/>
                        <div class=""content"">
                        <div class=""title"" style=""background-color: #66CCFF;"">
                        <h3>Error Message</h3>
                        </div>
                        <div class=""message"">
                        <p>{HttpUtility.HtmlEncode(exception.Message)}</p>
                        </div>
                        </div><br/>
                        <div class=""content"">
                        <div class=""title"" style=""background-color: #66CCFF;"">
                        <h3>Source</h3>
                        </div>
                        <div class=""message"">
                        <p>{HttpUtility.HtmlEncode(exception.Source ?? "No source")}</p>
                        </div>
                        </div><br/>
                        <div class=""content"">
                        <div class=""title"" style=""background-color: #66CCFF;"">
                        <h3>Stack Trace</h3>
                        </div>
                        <div class=""message"">
                        <p>{
                    HttpUtility.HtmlEncode(exception.StackTrace ?? "No stack trace").Replace("\r\n", "<br/>")
                }</p>
                        </div>
                        </div>";
            if (exception.InnerException != null)
            {
                report += $@"<br/>
                        <div class=""content"">
                        <div class=""title"" style=""background-color: #66CCFF;"">
                        <h3>Inner Exception</h3>
                        </div>
                        <div class=""message"">
                        {CreateReport(exception.InnerException)}
                        </div>
                        </div>";
            }

            report += "<br/>";
            return report;
        }

        #endregion

        #region DrDump Functions

        internal void SendAnonymousReport(DrDumpService.SendRequestCompletedEventHandler sendRequestCompleted)
        {
            _doctorDumpService = new DrDumpService(DoctorDumpSettings?.WebProxy);

            _doctorDumpService.SendRequestCompleted += sendRequestCompleted;

            _doctorDumpService.SendAnonymousReportAsync(
                Exception,
                ToEmail,
                DoctorDumpSettings?.ApplicationID);
        }

        private void SendFullReport(bool includeScreenshot,
            DrDumpService.SendRequestCompletedEventHandler sendRequestCompleted, Control form, string from,
            string userMessage)
        {
            var sendScreenshot = File.Exists(ScreenShot) && includeScreenshot;
            var screenshot = sendScreenshot ? File.ReadAllBytes(ScreenShot) : null;

            if (sendRequestCompleted != null)
            {
                if (_doctorDumpService == null)
                {
                    SendAnonymousReport(sendRequestCompleted);
                }

                _doctorDumpService.SendAdditionalDataAsync(form, DeveloperMessage, from,
                    userMessage, screenshot);
            }
            else
            {
                _doctorDumpService = new DrDumpService(DoctorDumpSettings?.WebProxy);
                var reportUrl =_doctorDumpService.SendReportSilently(Exception, ToEmail, DoctorDumpSettings?.ApplicationID, DeveloperMessage, from, userMessage, screenshot);
                if (DoctorDumpSettings != null && DoctorDumpSettings.OpenReportInBrowser)
                {
                    if (!string.IsNullOrEmpty(reportUrl))
                        Process.Start(reportUrl);
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// Set Doctor Dump processing settings.
    /// </summary>
    public class DoctorDumpSettings
    {
        /// <summary>
        /// Specify a proxy for a web request.
        /// </summary>
        public IWebProxy WebProxy;

        /// <summary>
        /// Gets or Sets application ID.
        /// </summary>
        public Guid? ApplicationID;

        /// <summary>
        /// Specify whether CrashReporter.NET should send anonymous crash report to Doctor Dump that doesn't contain private information.
        /// Only about 1/10 of users press "Send" button on crash reporting dialogs. And even less if there are required fields to fill.
        /// Without sending anonymous reports most of the problems are hidden from the developer.
        /// </summary>
        public bool SendAnonymousReportSilently = true;

        /// <summary>
        /// Specify whether CrashReporter.NET should open the web page in browser about crash report that contains report ID and may contain steps to fix the problem.
        /// </summary>
        public bool OpenReportInBrowser = true;
    }
}
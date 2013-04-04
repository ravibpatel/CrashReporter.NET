using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
using CrashReporterDotNET.Properties;

namespace CrashReporterDotNET
{
    internal partial class CrashReport : Form
    {
        private readonly String _appTitle;

        private readonly String _appVersion;
        private readonly Exception _exception;

        private readonly SmtpClient _smtpClient;

        private readonly MailAddress _toAddress;
        private readonly String _windowsVersion;
        private MailAddress _fromAddress;

        private ProgressDialog _progressDialog;

        public CrashReport(Exception exception, MailAddress fromAddress, MailAddress toAddress, SmtpClient smtpClient)
        {
            InitializeComponent();

            _exception = exception;
            _smtpClient = smtpClient;
            _fromAddress = fromAddress;
            _toAddress = toAddress;

            _appTitle = Assembly.GetEntryAssembly().GetName().Name;
            _appVersion = Assembly.GetEntryAssembly().GetName().Version.ToString();

            Text = string.Format("{0} {1} crashed.", _appTitle, _appVersion);
            textBoxException.Text = exception.GetType().ToString();
            saveFileDialog.FileName = string.Format("{0} {1} Crash Report", _appTitle, _appVersion);

            string osArchitecture;
            try
            {
                osArchitecture = IsOS64Bit() ? "64" : "32";
            }
            catch (Exception)
            {
                osArchitecture = "32/64 bit (Undetermine)";
            }
            if (File.Exists(string.Format("{0}\\{1}CrashScreenshot.png", Path.GetTempPath(), _appTitle)))
            {
                pictureBoxScreenshot.ImageLocation = string.Format("{0}\\{1}CrashScreenshot.png", Path.GetTempPath(),
                                                                   _appTitle);
                pictureBoxScreenshot.Show();
            }
            switch (Environment.OSVersion.Version.Major)
            {
                case 5:
                    switch (Environment.OSVersion.Version.Minor)
                    {
                        case 0:
                            _windowsVersion = string.Format("Windows 2000 {0} {1} Version {2}",
                                                            Environment.OSVersion.ServicePack, osArchitecture,
                                                            Environment.OSVersion.Version);
                            break;
                        case 1:
                            _windowsVersion = string.Format("Windows XP {0} {1} Version {2}",
                                                            Environment.OSVersion.ServicePack, osArchitecture,
                                                            Environment.OSVersion.Version);
                            break;
                        case 2:
                            _windowsVersion =
                                string.Format(
                                    "Windows XP x64 Professional Edition / Windows Server 2003 {0} {1} Version {2}",
                                    Environment.OSVersion.ServicePack, osArchitecture, Environment.OSVersion.Version);
                            break;
                    }
                    break;
                case 6:
                    switch (Environment.OSVersion.Version.Minor)
                    {
                        case 0:
                            _windowsVersion = string.Format("Windows Vista {0} {1} bit Version {2}",
                                                            Environment.OSVersion.ServicePack, osArchitecture,
                                                            Environment.OSVersion.Version);
                            break;
                        case 1:
                            _windowsVersion = string.Format("Windows 7 {0} {1} bit Version {2}",
                                                            Environment.OSVersion.ServicePack, osArchitecture,
                                                            Environment.OSVersion.Version);
                            break;
                        case 2:
                            _windowsVersion = string.Format("Windows 8 {0} {1} bit Version {2}",
                                                            Environment.OSVersion.ServicePack, osArchitecture,
                                                            Environment.OSVersion.Version);
                            break;
                    }
                    break;
            }
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private void ButtonSendReportClick(object sender, EventArgs e)
        {
            var regex = new Regex(@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                                  + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                            [0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                                  + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                            [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                                  + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$");
            string subject;
            if (!String.IsNullOrEmpty(textBoxEmail.Text) && regex.IsMatch(textBoxEmail.Text))
            {
                _fromAddress = new MailAddress(textBoxEmail.Text);
                subject = string.Format("{0} {1} Crash Report by {2}", _appTitle, _appVersion, textBoxEmail.Text);
            }
            else
                subject = string.Format("{0} {1} Crash Report", _appTitle, _appVersion);
            var message = new MailMessage(_fromAddress, _toAddress)
                {
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = HtmlReport()
                };
            if (File.Exists(string.Format("{0}\\{1}CrashScreenshot.png", Path.GetTempPath(), _appTitle)) &&
                checkBoxIncludeScreenshot.Checked)
                message.Attachments.Add(
                    new Attachment(string.Format("{0}\\{1}CrashScreenshot.png", Path.GetTempPath(), _appTitle)));
            _smtpClient.SendCompleted += SmtpClientSendCompleted;
            _smtpClient.SendAsync(message, "Crash Report");
            _progressDialog = new ProgressDialog();
            _progressDialog.ShowDialog();
        }

        public string HtmlReport()
        {
            string report =
                string.Format(@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
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
                    </div>", HttpUtility.HtmlEncode(_appTitle), HttpUtility.HtmlEncode(_appVersion),
                              HttpUtility.HtmlEncode(_windowsVersion),
                              HttpUtility.HtmlEncode(Environment.Version.ToString()), CreateReport(_exception));
            if (!String.IsNullOrEmpty(textBoxUserMessage.Text.Trim()))
            {
                report += string.Format(@"<br/>
                            <div class=""content"">
                            <div class=""title"" style=""background-color: #66FF99;"">
                            <h3>User Comment</h3>
                            </div>
                            <div class=""message"">
                            <p>{0}</p>
                            </div>
                            </div>", HttpUtility.HtmlEncode(textBoxUserMessage.Text.Trim()));
            }
            report += "</body></html>";
            return report;
        }

        private string CreateReport(Exception exception)
        {
            string report = string.Format(@"<br/>
                        <div class=""content"">
                        <div class=""title"" style=""background-color: #66CCFF;"">
                        <h3>Exception Type</h3>
                        </div>
                        <div class=""message"">
                        <p>{0}</p>
                        </div>
                        </div><br/>
                        <div class=""content"">
                        <div class=""title"" style=""background-color: #66CCFF;"">
                        <h3>Error Message</h3>
                        </div>
                        <div class=""message"">
                        <p>{1}</p>
                        </div>
                        </div><br/>
                        <div class=""content"">
                        <div class=""title"" style=""background-color: #66CCFF;"">
                        <h3>Source</h3>
                        </div>
                        <div class=""message"">
                        <p>{2}</p>
                        </div>
                        </div><br/>
                        <div class=""content"">
                        <div class=""title"" style=""background-color: #66CCFF;"">
                        <h3>Stack Trace</h3>
                        </div>
                        <div class=""message"">
                        <p>{3}</p>
                        </div>
                        </div>", HttpUtility.HtmlEncode(exception.GetType().ToString()),
                                          HttpUtility.HtmlEncode(exception.Message),
                                          HttpUtility.HtmlEncode(exception.Source),
                                          HttpUtility.HtmlEncode(exception.StackTrace).Replace("\r\n", "<br/>"));
            if (exception.InnerException != null)
            {
                report += string.Format(@"<br/>
                        <div class=""content"">
                        <div class=""title"" style=""background-color: #66CCFF;"">
                        <h3>Inner Exception</h3>
                        </div>
                        <div class=""message"">
                        {0}
                        </div>
                        </div>", CreateReport(exception.InnerException));
            }
            report += "<br/>";
            return report;
        }

        private void SmtpClientSendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            _progressDialog.Close();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, e.Error.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(
                    string.Format("Crassh report of {0} {1} is sent to the developer. Thanks for your support.",
                                  _appTitle, _appVersion), Resources.CrashReport_Crash_report_sent, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
        }

        private void CrashReportLoad(object sender, EventArgs e)
        {
            textBoxApplicationName.Text = _appTitle;
            textBoxApplicationVersion.Text = _appVersion;
            textBoxExceptionMessage.Text = _exception.Message;
            textBoxMessage.Text = _exception.Message;
            textBoxTime.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            textBoxSource.Text = _exception.Source;
            textBoxStackTrace.Text = string.Format("{0}\n{1}", _exception.InnerException, _exception.StackTrace);
        }

        private void ButtonSaveClick(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
        }

        private void SaveFileDialogFileOk(object sender, CancelEventArgs e)
        {
            File.WriteAllText(saveFileDialog.FileName, HtmlReport());
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            if (File.Exists(string.Format(@"{0}\{1}CrashScreenshot.png", Path.GetTempPath(), _appTitle)))
            {
                try
                {
                    File.Delete(string.Format(@"{0}\{1}CrashScreenshot.png", Path.GetTempPath(), _appTitle));
                }
                catch (Exception exception)
                {
                    Debug.Write(exception.Message);
                }
            }
        }

        private void LinkLabelViewLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(string.Format(@"{0}\{1}CrashScreenshot.png", Path.GetTempPath(), _appTitle));
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(
                    Resources.CrashReport_error_while_capturing_image,
                    Resources.CrashReport_No_image_captured, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show(
                    Resources.CrashReport_no_image_will_be_shown,
                    Resources.CrashReport_Error_opening_image, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Detect Architecture of Operating System

        [DllImport("kernel32", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        public static extern IntPtr LoadLibrary(string libraryName);

        [DllImport("kernel32", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        public static extern IntPtr GetProcAddress(IntPtr hwnd, string procedureName);

        public static bool IsOS64Bit()
        {
            return IntPtr.Size == 8 || (IntPtr.Size == 4 && Is32BitProcessOn64BitProcessor());
        }

        private static IsWow64ProcessDelegate GetIsWow64ProcessDelegate()
        {
            IntPtr handle = LoadLibrary("kernel32");

            if (handle != IntPtr.Zero)
            {
                IntPtr fnPtr = GetProcAddress(handle, "IsWow64Process");

                if (fnPtr != IntPtr.Zero)
                {
                    return
                        (IsWow64ProcessDelegate)
                        Marshal.GetDelegateForFunctionPointer(fnPtr, typeof (IsWow64ProcessDelegate));
                }
            }

            return null;
        }

        private static bool Is32BitProcessOn64BitProcessor()
        {
            IsWow64ProcessDelegate fnDelegate = GetIsWow64ProcessDelegate();

            if (fnDelegate == null)
            {
                return false;
            }

            bool isWow64;
            bool retVal = fnDelegate.Invoke(Process.GetCurrentProcess().Handle, out isWow64);

            if (retVal == false)
            {
                return false;
            }

            return isWow64;
        }

        private delegate bool IsWow64ProcessDelegate([In] IntPtr handle, [Out] out bool isWow64Process);
    }
}
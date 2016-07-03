using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
using CrashReporterDotNET.DrDump;

namespace CrashReporterDotNET
{
    internal partial class CrashReport : Form
    {
        private readonly ReportCrash _reportCrash;

        private readonly ComponentResourceManager _resources = new ComponentResourceManager(typeof (CrashReport));

        private ProgressDialog _progressDialog;

        private DrDumpService _doctorDumpService;

        #region Form Events

        public CrashReport(ReportCrash reportCrashObject)
        {
            InitializeComponent();
            _reportCrash = reportCrashObject;
            Text = string.Format(_resources.GetString("TitleText"), _reportCrash.ApplicationTitle,
                _reportCrash.ApplicationVersion);
            saveFileDialog.FileName = string.Format(_resources.GetString("ReportFileName"),
                _reportCrash.ApplicationTitle, _reportCrash.ApplicationVersion);
            saveFileDialog.Filter = @"HTML files(*.html)|*.html";
            if (File.Exists(_reportCrash.ScreenShot))
            {
                checkBoxIncludeScreenshot.Checked = _reportCrash.IncludeScreenshot;
                pictureBoxScreenshot.ImageLocation = _reportCrash.ScreenShot;
                pictureBoxScreenshot.Show();
            }

            if (_reportCrash.DoctorDumpSettings != null && _reportCrash.DoctorDumpSettings.SendAnonymousReportSilently)
                SendAnonymousReport();
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private void CrashReportLoad(object sender, EventArgs e)
        {
            textBoxException.Text = _reportCrash.Exception.GetType().ToString();
            textBoxApplicationName.Text = _reportCrash.ApplicationTitle;
            textBoxApplicationVersion.Text = _reportCrash.ApplicationVersion;
            textBoxExceptionMessage.Text = _reportCrash.Exception.Message;
            textBoxMessage.Text = _reportCrash.Exception.Message;
            textBoxTime.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            textBoxSource.Text = _reportCrash.Exception.Source;
            textBoxStackTrace.Text = string.Format("{0}\n{1}", _reportCrash.Exception.InnerException,
                _reportCrash.Exception.StackTrace);
        }

        private void CrashReport_Shown(object sender, EventArgs e)
        {
            Activate();
            textBoxEmail.Select();
        }

        private void CrashReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (File.Exists(_reportCrash.ScreenShot))
            {
                try
                {
                    File.Delete(_reportCrash.ScreenShot);
                }
                catch (Exception exception)
                {
                    Debug.Write(exception.Message);
                }
            }
        }

        #endregion

        #region Control Events

        private void ButtonSendReportClick(object sender, EventArgs e)
        {
            var fromAddress = !string.IsNullOrEmpty(_reportCrash.FromEmail) ? new MailAddress(_reportCrash.FromEmail) : null;
            var toAddress = new MailAddress(_reportCrash.ToEmail);

            const string r0_255 = @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])";
            var regexEmail = new Regex(@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                                       + @"((" + r0_255 + @"\." + r0_255 + @"\." + r0_255 + @"\." + r0_255 + @"){1}|"
                                       + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$");
            var subject = "";

            if (string.IsNullOrEmpty(textBoxEmail.Text.Trim()))
            {
                if (_reportCrash.EmailRequired)
                {
                    errorProviderEmail.SetError(textBoxEmail, _resources.GetString("EmailRequiredError"));
                    return;
                }
            }
            else
            {
                errorProviderEmail.SetError(textBoxEmail, "");
                if (!regexEmail.IsMatch(textBoxEmail.Text.Trim()))
                {
                    if (_reportCrash.EmailRequired)
                    {
                        errorProviderEmail.SetError(textBoxEmail, _resources.GetString("InvalidEmailAddressError"));
                        return;
                    }
                }
                else
                {
                    errorProviderEmail.SetError(textBoxEmail, "");
                    fromAddress = new MailAddress(textBoxEmail.Text.Trim());
                    subject = string.Format("{0} {1} Crash Report by {2}", _reportCrash.ApplicationTitle,
                        _reportCrash.ApplicationVersion, textBoxEmail.Text.Trim());
                }
            }
            if (string.IsNullOrEmpty(subject.Trim()))
            {
                subject = string.Format("{0} {1} Crash Report", _reportCrash.ApplicationTitle,
                    _reportCrash.ApplicationVersion);
            }

            if (_reportCrash.AnalyzeWithDoctorDump)
            {
                SendFullReport();
            }
            else
            {
                var smtpClient = new SmtpClient
                {
                    Host = _reportCrash.SmtpHost,
                    Port = _reportCrash.Port,
                    EnableSsl = _reportCrash.EnableSSL,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_reportCrash.UserName, _reportCrash.Password),
                };

                var message = new MailMessage(fromAddress, toAddress)
                {
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = HtmlReport(),
                };

                if (File.Exists(_reportCrash.ScreenShot) && checkBoxIncludeScreenshot.Checked)
                {
                    message.Attachments.Add(new Attachment(_reportCrash.ScreenShot));
                }

                smtpClient.SendCompleted += SmtpClientSendCompleted;
                smtpClient.SendAsync(message, "Crash Report");
            }

            _progressDialog = new ProgressDialog();
            _progressDialog.ShowDialog();
        }

        private void SmtpClientSendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                ReportFailure(e.Error);
            }
            else
            {
                ReportSuccess();
            }
        }

        private void ButtonSaveClick(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
        }

        private void SaveFileDialogFileOk(object sender, CancelEventArgs e)
        {
            File.WriteAllText(saveFileDialog.FileName, HtmlReport());
        }

        private void LinkLabelViewLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(_reportCrash.ScreenShot);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(
                    _resources.GetString("ErrorCapturingImageMessage"),
                    _resources.GetString("ErrorCapturingImageCaption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show(
                    _resources.GetString("NoImageShownMessage"),
                    _resources.GetString("NoImageShownCaption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region HTML Report Generator

        private string HtmlReport()
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
                    </div>", HttpUtility.HtmlEncode(_reportCrash.ApplicationTitle),
                    HttpUtility.HtmlEncode(_reportCrash.ApplicationVersion),
                    HttpUtility.HtmlEncode(HelperMethods.GetWindowsVersion()),
                    HttpUtility.HtmlEncode(Environment.Version.ToString()),
                    CreateReport(_reportCrash.Exception));
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
            if (!String.IsNullOrEmpty(_reportCrash.DeveloperMessage.Trim()))
            {
                report += string.Format(@"<br/>
                            <div class=""content"">
                            <div class=""title"" style=""background-color: #66FF99;"">
                            <h3>Developer Message</h3>
                            </div>
                            <div class=""message"">
                            <p>{0}</p>
                            </div>
                            </div>", HttpUtility.HtmlEncode(_reportCrash.DeveloperMessage.Trim()));
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
                HttpUtility.HtmlEncode(exception.Source ?? "No source"),
                HttpUtility.HtmlEncode(exception.StackTrace ?? "No stack trace").Replace("\r\n", "<br/>"));
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

        #endregion

        #region DrDump Functions

        private void SendAnonymousReport()
        {
            _doctorDumpService = new DrDumpService();
            _doctorDumpService.SendRequestCompleted += SendRequestCompleted;

            _doctorDumpService.SendAnonymousReportAsync(
                _reportCrash.Exception,
                _reportCrash.ToEmail,
                _reportCrash.DoctorDumpSettings != null ? _reportCrash.DoctorDumpSettings.ApplicationID : null);
        }

        private void SendFullReport()
        {
            var sendScreenshot = File.Exists(_reportCrash.ScreenShot) && checkBoxIncludeScreenshot.Checked;
            var screenshot = sendScreenshot ? File.ReadAllBytes(_reportCrash.ScreenShot) : null;

            if (_doctorDumpService == null)
            {
                SendAnonymousReport();
            }
            _doctorDumpService.SendAdditionalDataAsync(this, _reportCrash.DeveloperMessage, textBoxEmail.Text.Trim(),
                    textBoxUserMessage.Text.Trim(), screenshot);
        }
        private void SendRequestCompleted(object sender, DrDumpService.SendRequestCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                ReportFailure(e.Error);
            }
            else
            {
                ReportSuccess();

                if (_reportCrash.DoctorDumpSettings != null && _reportCrash.DoctorDumpSettings.OpenReportInBrowser)
                {
                    if (!string.IsNullOrEmpty(e.Result.UrlToProblem))
                        Process.Start(e.Result.UrlToProblem);
                }
            }
        }

        private void ReportSuccess()
        {
            _progressDialog.Close();
            MessageBox.Show(
                string.Format(_resources.GetString("MessageSentMessage"),
                    _reportCrash.ApplicationTitle, _reportCrash.ApplicationVersion),
                _resources.GetString("MessageSentCaption"), MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
        }

        private void ReportFailure(Exception exception)
        {
            _progressDialog.Close();
            MessageBox.Show(exception.Message, exception.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion
    }
}
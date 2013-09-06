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
using CrashReporterDotNET.Properties;

namespace CrashReporterDotNET
{
    internal partial class CrashReport : Form
    {
        private readonly ReportCrash _reportCrash;
        private ProgressDialog _progressDialog;

        public CrashReport(ReportCrash reportCrashObject)
        {
            InitializeComponent();
            _reportCrash = reportCrashObject;
            Text = string.Format("{0} {1} crashed.", _reportCrash.ApplicationTitle, _reportCrash.ApplicationVersion);
            saveFileDialog.FileName = string.Format("{0} {1} Crash Report", _reportCrash.ApplicationTitle,
                                                    _reportCrash.ApplicationVersion);

            if (File.Exists(_reportCrash.ScreenShot))
            {
                pictureBoxScreenshot.ImageLocation = _reportCrash.ScreenShot;
                pictureBoxScreenshot.Show();
            }
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private void ButtonSendReportClick(object sender, EventArgs e)
        {
            var fromAddress = new MailAddress(_reportCrash.FromEmail);
            var toAddress = new MailAddress(_reportCrash.ToEmail);

            var regexEmail = new Regex(@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                                       + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                            [0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                                       + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                            [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                                       + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$");
            string subject;


            if (!String.IsNullOrEmpty(textBoxEmail.Text) && regexEmail.IsMatch(textBoxEmail.Text))
            {
                fromAddress = new MailAddress(textBoxEmail.Text);
                subject = string.Format("{0} {1} Crash Report by {2}", _reportCrash.ApplicationTitle,
                                        _reportCrash.ApplicationVersion, textBoxEmail.Text);
            }
            else
            {
                subject = string.Format("{0} {1} Crash Report", _reportCrash.ApplicationTitle,
                                        _reportCrash.ApplicationVersion);
            }

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
                    </div>", HttpUtility.HtmlEncode(_reportCrash.ApplicationTitle),
                              HttpUtility.HtmlEncode(_reportCrash.ApplicationVersion),
                              HttpUtility.HtmlEncode(HelperMethods.GetWindowsVersion()),
                              HttpUtility.HtmlEncode(Environment.Version.ToString()),
                              CreateReport(_reportCrash.Exception));
            string message = textBoxUserMessage.Text.Trim() + _reportCrash.DeveloperMessage;
            if (!String.IsNullOrEmpty(message))
            {
                report += string.Format(@"<br/>
                            <div class=""content"">
                            <div class=""title"" style=""background-color: #66FF99;"">
                            <h3>User Comment</h3>
                            </div>
                            <div class=""message"">
                            <p>{0}</p>
                            </div>
                            </div>", HttpUtility.HtmlEncode(message));
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
                    string.Format("Crash report of {0} {1} is sent to the developer. Thanks for your support.",
                                  _reportCrash.ApplicationTitle, _reportCrash.ApplicationVersion),
                    Resources.CrashReport_Crash_report_sent, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
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

        private void CrashReport_Shown(object sender, EventArgs e)
        {
            Activate();
        }
    }
}
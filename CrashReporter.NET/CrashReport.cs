using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CrashReporterDotNET.DrDump;
using CrashReporterDotNET.Properties;

namespace CrashReporterDotNET
{
    internal partial class CrashReport : Form
    {
        private readonly ReportCrash _reportCrash;

        private ProgressDialog _progressDialog;

        #region Form Events

        public CrashReport(ReportCrash reportCrashObject)
        {
            InitializeComponent();
            toolTip.ToolTipTitle = Resources.ToolTipTitle;
            _reportCrash = reportCrashObject;
            Text = string.Format(Resources.TitleText, _reportCrash.ApplicationTitle,
                _reportCrash.ApplicationVersion);
            saveFileDialog.FileName = string.Format(Resources.ReportFileName,
                _reportCrash.ApplicationTitle, _reportCrash.ApplicationVersion);
            saveFileDialog.Filter = @"HTML files(*.html)|*.html";
            if (File.Exists(_reportCrash.ScreenShot))
            {
                checkBoxIncludeScreenshot.Checked = _reportCrash.IncludeScreenshot;
                pictureBoxScreenshot.ImageLocation = _reportCrash.ScreenShot;
                pictureBoxScreenshot.Show();
            }

            if (_reportCrash.DoctorDumpSettings != null &&
                _reportCrash.DoctorDumpSettings.SendAnonymousReportSilently)
                _reportCrash.SendAnonymousReport(SendRequestCompleted);
        }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        private void CrashReportLoad(object sender, EventArgs e)
        {
            if (!_reportCrash.showScreentshotTab)
                tabControl.TabPages.RemoveAt(2);
            textBoxException.Text = _reportCrash.Exception.GetType().ToString();
            textBoxApplicationName.Text = _reportCrash.ApplicationTitle;
            textBoxApplicationVersion.Text = _reportCrash.ApplicationVersion;
            textBoxExceptionMessage.Text = _reportCrash.Exception.Message;
            textBoxMessage.Text = _reportCrash.Exception.Message;
            textBoxTime.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            textBoxSource.Text = _reportCrash.Exception.Source;
            textBoxStackTrace.Text = $@"{_reportCrash.Exception.InnerException}\n{_reportCrash.Exception.StackTrace}";
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
            var regexEmail = new Regex(
                @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$");
            string from = String.Empty;

            if (string.IsNullOrEmpty(textBoxEmail.Text.Trim()))
            {
                if (_reportCrash.EmailRequired)
                {
                    errorProviderEmail.SetError(textBoxEmail, Resources.EmailRequiredError);
                    return;
                }
            }
            else
            {
                if (regexEmail.IsMatch(textBoxEmail.Text.Trim()))
                {
                    errorProviderEmail.SetError(textBoxEmail, "");
                    from = textBoxEmail.Text.Trim();
                }
                else
                {
                    if (_reportCrash.EmailRequired)
                    {
                        errorProviderEmail.SetError(textBoxEmail, Resources.InvalidEmailAddressError);
                        return;
                    }
                }
            }

            try
            {
                _reportCrash.SendReport(checkBoxIncludeScreenshot.Checked, SendRequestCompleted,
                    SmtpClientSendCompleted,
                    this, from, textBoxUserMessage.Text.Trim());

                _progressDialog = new ProgressDialog();
                _progressDialog.ShowDialog();
            }
            catch (SocketException)
            {
                MessageBox.Show(Resources.NoConnectionMessage, Resources.NoConnectionCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonSaveClick(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
        }

        private void SaveFileDialogFileOk(object sender, CancelEventArgs e)
        {
            File.WriteAllText(saveFileDialog.FileName, _reportCrash.CreateHtmlReport(textBoxUserMessage.Text.Trim()));
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
                    Resources.ErrorCapturingImageMessage,
                    Resources.ErrorCapturingImageCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show(
                    Resources.NoImageShownMessage,
                    Resources.NoImageShownCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Events

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

        private void ReportSuccess()
        {
            _progressDialog.Close();
            MessageBox.Show(
                string.Format(Resources.MessageSentMessage,
                    _reportCrash.ApplicationTitle, _reportCrash.ApplicationVersion),
                Resources.MessageSentCaption, MessageBoxButtons.OK,
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

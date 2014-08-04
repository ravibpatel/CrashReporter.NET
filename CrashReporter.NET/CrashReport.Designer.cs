namespace CrashReporterDotNET
{
    partial class CrashReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CrashReport));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.labelEmail = new System.Windows.Forms.Label();
            this.textBoxUserMessage = new System.Windows.Forms.TextBox();
            this.labelMessage = new System.Windows.Forms.Label();
            this.textBoxTime = new System.Windows.Forms.TextBox();
            this.labelTime = new System.Windows.Forms.Label();
            this.textBoxApplicationVersion = new System.Windows.Forms.TextBox();
            this.labelVersion = new System.Windows.Forms.Label();
            this.textBoxApplicationName = new System.Windows.Forms.TextBox();
            this.labelApplication = new System.Windows.Forms.Label();
            this.textBoxExceptionMessage = new System.Windows.Forms.TextBox();
            this.pictureBoxError = new System.Windows.Forms.PictureBox();
            this.tabPageExceptions = new System.Windows.Forms.TabPage();
            this.textBoxException = new System.Windows.Forms.TextBox();
            this.labelExceptionType = new System.Windows.Forms.Label();
            this.textBoxSource = new System.Windows.Forms.TextBox();
            this.labelSource = new System.Windows.Forms.Label();
            this.textBoxStackTrace = new System.Windows.Forms.TextBox();
            this.labelStackTrace = new System.Windows.Forms.Label();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.labelExceptionMessage = new System.Windows.Forms.Label();
            this.tabPageScreenshot = new System.Windows.Forms.TabPage();
            this.linkLabelView = new System.Windows.Forms.LinkLabel();
            this.groupBoxScreenshot = new System.Windows.Forms.GroupBox();
            this.pictureBoxScreenshot = new System.Windows.Forms.PictureBox();
            this.checkBoxIncludeScreenshot = new System.Windows.Forms.CheckBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSendReport = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.errorProviderEmail = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabControl.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxError)).BeginInit();
            this.tabPageExceptions.SuspendLayout();
            this.tabPageScreenshot.SuspendLayout();
            this.groupBoxScreenshot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreenshot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderEmail)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageGeneral);
            this.tabControl.Controls.Add(this.tabPageExceptions);
            this.tabControl.Controls.Add(this.tabPageScreenshot);
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.textBoxEmail);
            this.tabPageGeneral.Controls.Add(this.labelEmail);
            this.tabPageGeneral.Controls.Add(this.textBoxUserMessage);
            this.tabPageGeneral.Controls.Add(this.labelMessage);
            this.tabPageGeneral.Controls.Add(this.textBoxTime);
            this.tabPageGeneral.Controls.Add(this.labelTime);
            this.tabPageGeneral.Controls.Add(this.textBoxApplicationVersion);
            this.tabPageGeneral.Controls.Add(this.labelVersion);
            this.tabPageGeneral.Controls.Add(this.textBoxApplicationName);
            this.tabPageGeneral.Controls.Add(this.labelApplication);
            this.tabPageGeneral.Controls.Add(this.textBoxExceptionMessage);
            this.tabPageGeneral.Controls.Add(this.pictureBoxError);
            resources.ApplyResources(this.tabPageGeneral, "tabPageGeneral");
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.BackColor = System.Drawing.SystemColors.Info;
            resources.ApplyResources(this.textBoxEmail, "textBoxEmail");
            this.textBoxEmail.Name = "textBoxEmail";
            // 
            // labelEmail
            // 
            resources.ApplyResources(this.labelEmail, "labelEmail");
            this.labelEmail.Name = "labelEmail";
            // 
            // textBoxUserMessage
            // 
            this.textBoxUserMessage.BackColor = System.Drawing.SystemColors.Info;
            resources.ApplyResources(this.textBoxUserMessage, "textBoxUserMessage");
            this.textBoxUserMessage.Name = "textBoxUserMessage";
            // 
            // labelMessage
            // 
            resources.ApplyResources(this.labelMessage, "labelMessage");
            this.labelMessage.Name = "labelMessage";
            // 
            // textBoxTime
            // 
            this.textBoxTime.Cursor = System.Windows.Forms.Cursors.IBeam;
            resources.ApplyResources(this.textBoxTime, "textBoxTime");
            this.textBoxTime.Name = "textBoxTime";
            this.textBoxTime.ReadOnly = true;
            // 
            // labelTime
            // 
            resources.ApplyResources(this.labelTime, "labelTime");
            this.labelTime.Name = "labelTime";
            // 
            // textBoxApplicationVersion
            // 
            this.textBoxApplicationVersion.Cursor = System.Windows.Forms.Cursors.IBeam;
            resources.ApplyResources(this.textBoxApplicationVersion, "textBoxApplicationVersion");
            this.textBoxApplicationVersion.Name = "textBoxApplicationVersion";
            this.textBoxApplicationVersion.ReadOnly = true;
            // 
            // labelVersion
            // 
            resources.ApplyResources(this.labelVersion, "labelVersion");
            this.labelVersion.Name = "labelVersion";
            // 
            // textBoxApplicationName
            // 
            this.textBoxApplicationName.Cursor = System.Windows.Forms.Cursors.IBeam;
            resources.ApplyResources(this.textBoxApplicationName, "textBoxApplicationName");
            this.textBoxApplicationName.Name = "textBoxApplicationName";
            this.textBoxApplicationName.ReadOnly = true;
            // 
            // labelApplication
            // 
            resources.ApplyResources(this.labelApplication, "labelApplication");
            this.labelApplication.Name = "labelApplication";
            // 
            // textBoxExceptionMessage
            // 
            this.textBoxExceptionMessage.Cursor = System.Windows.Forms.Cursors.IBeam;
            resources.ApplyResources(this.textBoxExceptionMessage, "textBoxExceptionMessage");
            this.textBoxExceptionMessage.Name = "textBoxExceptionMessage";
            this.textBoxExceptionMessage.ReadOnly = true;
            // 
            // pictureBoxError
            // 
            this.pictureBoxError.Image = global::CrashReporterDotNET.Properties.Resources.warning_64;
            resources.ApplyResources(this.pictureBoxError, "pictureBoxError");
            this.pictureBoxError.Name = "pictureBoxError";
            this.pictureBoxError.TabStop = false;
            // 
            // tabPageExceptions
            // 
            this.tabPageExceptions.Controls.Add(this.textBoxException);
            this.tabPageExceptions.Controls.Add(this.labelExceptionType);
            this.tabPageExceptions.Controls.Add(this.textBoxSource);
            this.tabPageExceptions.Controls.Add(this.labelSource);
            this.tabPageExceptions.Controls.Add(this.textBoxStackTrace);
            this.tabPageExceptions.Controls.Add(this.labelStackTrace);
            this.tabPageExceptions.Controls.Add(this.textBoxMessage);
            this.tabPageExceptions.Controls.Add(this.labelExceptionMessage);
            resources.ApplyResources(this.tabPageExceptions, "tabPageExceptions");
            this.tabPageExceptions.Name = "tabPageExceptions";
            this.tabPageExceptions.UseVisualStyleBackColor = true;
            // 
            // textBoxException
            // 
            resources.ApplyResources(this.textBoxException, "textBoxException");
            this.textBoxException.Name = "textBoxException";
            this.textBoxException.ReadOnly = true;
            // 
            // labelExceptionType
            // 
            resources.ApplyResources(this.labelExceptionType, "labelExceptionType");
            this.labelExceptionType.Name = "labelExceptionType";
            // 
            // textBoxSource
            // 
            resources.ApplyResources(this.textBoxSource, "textBoxSource");
            this.textBoxSource.Name = "textBoxSource";
            this.textBoxSource.ReadOnly = true;
            // 
            // labelSource
            // 
            resources.ApplyResources(this.labelSource, "labelSource");
            this.labelSource.Name = "labelSource";
            // 
            // textBoxStackTrace
            // 
            this.textBoxStackTrace.Cursor = System.Windows.Forms.Cursors.IBeam;
            resources.ApplyResources(this.textBoxStackTrace, "textBoxStackTrace");
            this.textBoxStackTrace.Name = "textBoxStackTrace";
            this.textBoxStackTrace.ReadOnly = true;
            // 
            // labelStackTrace
            // 
            resources.ApplyResources(this.labelStackTrace, "labelStackTrace");
            this.labelStackTrace.Name = "labelStackTrace";
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Cursor = System.Windows.Forms.Cursors.IBeam;
            resources.ApplyResources(this.textBoxMessage, "textBoxMessage");
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.ReadOnly = true;
            // 
            // labelExceptionMessage
            // 
            resources.ApplyResources(this.labelExceptionMessage, "labelExceptionMessage");
            this.labelExceptionMessage.Name = "labelExceptionMessage";
            // 
            // tabPageScreenshot
            // 
            this.tabPageScreenshot.Controls.Add(this.linkLabelView);
            this.tabPageScreenshot.Controls.Add(this.groupBoxScreenshot);
            this.tabPageScreenshot.Controls.Add(this.checkBoxIncludeScreenshot);
            resources.ApplyResources(this.tabPageScreenshot, "tabPageScreenshot");
            this.tabPageScreenshot.Name = "tabPageScreenshot";
            this.tabPageScreenshot.UseVisualStyleBackColor = true;
            // 
            // linkLabelView
            // 
            resources.ApplyResources(this.linkLabelView, "linkLabelView");
            this.linkLabelView.Name = "linkLabelView";
            this.linkLabelView.TabStop = true;
            this.linkLabelView.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelViewLinkClicked);
            // 
            // groupBoxScreenshot
            // 
            this.groupBoxScreenshot.Controls.Add(this.pictureBoxScreenshot);
            resources.ApplyResources(this.groupBoxScreenshot, "groupBoxScreenshot");
            this.groupBoxScreenshot.Name = "groupBoxScreenshot";
            this.groupBoxScreenshot.TabStop = false;
            // 
            // pictureBoxScreenshot
            // 
            resources.ApplyResources(this.pictureBoxScreenshot, "pictureBoxScreenshot");
            this.pictureBoxScreenshot.Name = "pictureBoxScreenshot";
            this.pictureBoxScreenshot.TabStop = false;
            // 
            // checkBoxIncludeScreenshot
            // 
            resources.ApplyResources(this.checkBoxIncludeScreenshot, "checkBoxIncludeScreenshot");
            this.checkBoxIncludeScreenshot.Checked = true;
            this.checkBoxIncludeScreenshot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIncludeScreenshot.Name = "checkBoxIncludeScreenshot";
            this.toolTip.SetToolTip(this.checkBoxIncludeScreenshot, resources.GetString("checkBoxIncludeScreenshot.ToolTip"));
            this.checkBoxIncludeScreenshot.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.AllowDrop = true;
            this.buttonSave.Image = global::CrashReporterDotNET.Properties.Resources.save_as;
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSaveClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Image = global::CrashReporterDotNET.Properties.Resources.stop;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSendReport
            // 
            this.buttonSendReport.Image = global::CrashReporterDotNET.Properties.Resources.email_go;
            resources.ApplyResources(this.buttonSendReport, "buttonSendReport");
            this.buttonSendReport.Name = "buttonSendReport";
            this.buttonSendReport.UseVisualStyleBackColor = true;
            this.buttonSendReport.Click += new System.EventHandler(this.ButtonSendReportClick);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "html";
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveFileDialogFileOk);
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 1;
            this.toolTip.AutoPopDelay = 50000000;
            this.toolTip.InitialDelay = 1;
            this.toolTip.IsBalloon = true;
            this.toolTip.ReshowDelay = 0;
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.ToolTipTitle = "Include application screenshot";
            // 
            // errorProviderEmail
            // 
            this.errorProviderEmail.ContainerControl = this;
            resources.ApplyResources(this.errorProviderEmail, "errorProviderEmail");
            // 
            // CrashReport
            // 
            this.AcceptButton = this.buttonSendReport;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSendReport);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CrashReport";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CrashReport_FormClosing);
            this.Load += new System.EventHandler(this.CrashReportLoad);
            this.Shown += new System.EventHandler(this.CrashReport_Shown);
            this.tabControl.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxError)).EndInit();
            this.tabPageExceptions.ResumeLayout(false);
            this.tabPageExceptions.PerformLayout();
            this.tabPageScreenshot.ResumeLayout(false);
            this.tabPageScreenshot.PerformLayout();
            this.groupBoxScreenshot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreenshot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderEmail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.TabPage tabPageExceptions;
        private System.Windows.Forms.Button buttonSendReport;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.PictureBox pictureBoxError;
        private System.Windows.Forms.TextBox textBoxExceptionMessage;
        private System.Windows.Forms.TextBox textBoxApplicationVersion;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.TextBox textBoxApplicationName;
        private System.Windows.Forms.Label labelApplication;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.TextBox textBoxTime;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.TextBox textBoxUserMessage;
        private System.Windows.Forms.TextBox textBoxStackTrace;
        private System.Windows.Forms.Label labelStackTrace;
        private System.Windows.Forms.TextBox textBoxMessage;
        private System.Windows.Forms.Label labelExceptionMessage;
        private System.Windows.Forms.TextBox textBoxSource;
        private System.Windows.Forms.Label labelSource;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.TextBox textBoxException;
        private System.Windows.Forms.Label labelExceptionType;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TabPage tabPageScreenshot;
        private System.Windows.Forms.PictureBox pictureBoxScreenshot;
        private System.Windows.Forms.GroupBox groupBoxScreenshot;
        private System.Windows.Forms.CheckBox checkBoxIncludeScreenshot;
        private System.Windows.Forms.LinkLabel linkLabelView;
        private System.Windows.Forms.ErrorProvider errorProviderEmail;
    }
}
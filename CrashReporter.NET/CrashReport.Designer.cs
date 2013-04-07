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
            this.buttonCacel = new System.Windows.Forms.Button();
            this.buttonSendReport = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxError)).BeginInit();
            this.tabPageExceptions.SuspendLayout();
            this.tabPageScreenshot.SuspendLayout();
            this.groupBoxScreenshot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreenshot)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageGeneral);
            this.tabControl.Controls.Add(this.tabPageExceptions);
            this.tabControl.Controls.Add(this.tabPageScreenshot);
            this.tabControl.Location = new System.Drawing.Point(13, 14);
            this.tabControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(599, 450);
            this.tabControl.TabIndex = 0;
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
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 26);
            this.tabPageGeneral.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageGeneral.Size = new System.Drawing.Size(591, 420);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "General";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxEmail.Location = new System.Drawing.Point(105, 192);
            this.textBoxEmail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(480, 25);
            this.textBoxEmail.TabIndex = 11;
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(3, 195);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(39, 17);
            this.labelEmail.TabIndex = 10;
            this.labelEmail.Text = "Email";
            // 
            // textBoxUserMessage
            // 
            this.textBoxUserMessage.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxUserMessage.Location = new System.Drawing.Point(6, 251);
            this.textBoxUserMessage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxUserMessage.Multiline = true;
            this.textBoxUserMessage.Name = "textBoxUserMessage";
            this.textBoxUserMessage.Size = new System.Drawing.Size(579, 161);
            this.textBoxUserMessage.TabIndex = 9;
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.labelMessage.Location = new System.Drawing.Point(3, 230);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(322, 17);
            this.labelMessage.TabIndex = 8;
            this.labelMessage.Text = "Please tell us how application crashed so we can fix it.";
            // 
            // textBoxTime
            // 
            this.textBoxTime.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxTime.Location = new System.Drawing.Point(341, 153);
            this.textBoxTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxTime.Name = "textBoxTime";
            this.textBoxTime.ReadOnly = true;
            this.textBoxTime.Size = new System.Drawing.Size(244, 25);
            this.textBoxTime.TabIndex = 7;
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.labelTime.Location = new System.Drawing.Point(299, 156);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(36, 17);
            this.labelTime.TabIndex = 6;
            this.labelTime.Text = "Time";
            // 
            // textBoxApplicationVersion
            // 
            this.textBoxApplicationVersion.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxApplicationVersion.Location = new System.Drawing.Point(105, 153);
            this.textBoxApplicationVersion.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxApplicationVersion.Name = "textBoxApplicationVersion";
            this.textBoxApplicationVersion.ReadOnly = true;
            this.textBoxApplicationVersion.Size = new System.Drawing.Size(188, 25);
            this.textBoxApplicationVersion.TabIndex = 5;
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.labelVersion.Location = new System.Drawing.Point(3, 157);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(52, 17);
            this.labelVersion.TabIndex = 4;
            this.labelVersion.Text = "Version";
            // 
            // textBoxApplicationName
            // 
            this.textBoxApplicationName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxApplicationName.Location = new System.Drawing.Point(105, 115);
            this.textBoxApplicationName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxApplicationName.Name = "textBoxApplicationName";
            this.textBoxApplicationName.ReadOnly = true;
            this.textBoxApplicationName.Size = new System.Drawing.Size(480, 25);
            this.textBoxApplicationName.TabIndex = 3;
            // 
            // labelApplication
            // 
            this.labelApplication.AutoSize = true;
            this.labelApplication.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.labelApplication.Location = new System.Drawing.Point(3, 118);
            this.labelApplication.Name = "labelApplication";
            this.labelApplication.Size = new System.Drawing.Size(73, 17);
            this.labelApplication.TabIndex = 2;
            this.labelApplication.Text = "Application";
            // 
            // textBoxExceptionMessage
            // 
            this.textBoxExceptionMessage.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxExceptionMessage.Location = new System.Drawing.Point(105, 7);
            this.textBoxExceptionMessage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxExceptionMessage.Multiline = true;
            this.textBoxExceptionMessage.Name = "textBoxExceptionMessage";
            this.textBoxExceptionMessage.ReadOnly = true;
            this.textBoxExceptionMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxExceptionMessage.Size = new System.Drawing.Size(480, 101);
            this.textBoxExceptionMessage.TabIndex = 1;
            // 
            // pictureBoxError
            // 
            this.pictureBoxError.Image = global::CrashReporterDotNET.Properties.Resources.warning_64;
            this.pictureBoxError.Location = new System.Drawing.Point(6, 7);
            this.pictureBoxError.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBoxError.Name = "pictureBoxError";
            this.pictureBoxError.Size = new System.Drawing.Size(93, 84);
            this.pictureBoxError.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxError.TabIndex = 0;
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
            this.tabPageExceptions.Location = new System.Drawing.Point(4, 26);
            this.tabPageExceptions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageExceptions.Name = "tabPageExceptions";
            this.tabPageExceptions.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageExceptions.Size = new System.Drawing.Size(591, 420);
            this.tabPageExceptions.TabIndex = 1;
            this.tabPageExceptions.Text = "Exceptions";
            this.tabPageExceptions.UseVisualStyleBackColor = true;
            // 
            // textBoxException
            // 
            this.textBoxException.Location = new System.Drawing.Point(6, 30);
            this.textBoxException.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxException.Name = "textBoxException";
            this.textBoxException.ReadOnly = true;
            this.textBoxException.Size = new System.Drawing.Size(579, 25);
            this.textBoxException.TabIndex = 9;
            // 
            // labelExceptionType
            // 
            this.labelExceptionType.AutoSize = true;
            this.labelExceptionType.Location = new System.Drawing.Point(3, 7);
            this.labelExceptionType.Name = "labelExceptionType";
            this.labelExceptionType.Size = new System.Drawing.Size(96, 17);
            this.labelExceptionType.TabIndex = 7;
            this.labelExceptionType.Text = "Exception Type";
            // 
            // textBoxSource
            // 
            this.textBoxSource.Location = new System.Drawing.Point(6, 170);
            this.textBoxSource.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxSource.Name = "textBoxSource";
            this.textBoxSource.ReadOnly = true;
            this.textBoxSource.Size = new System.Drawing.Size(579, 25);
            this.textBoxSource.TabIndex = 6;
            // 
            // labelSource
            // 
            this.labelSource.AutoSize = true;
            this.labelSource.Location = new System.Drawing.Point(3, 147);
            this.labelSource.Name = "labelSource";
            this.labelSource.Size = new System.Drawing.Size(48, 17);
            this.labelSource.TabIndex = 5;
            this.labelSource.Text = "Source";
            // 
            // textBoxStackTrace
            // 
            this.textBoxStackTrace.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxStackTrace.Location = new System.Drawing.Point(6, 222);
            this.textBoxStackTrace.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxStackTrace.Multiline = true;
            this.textBoxStackTrace.Name = "textBoxStackTrace";
            this.textBoxStackTrace.ReadOnly = true;
            this.textBoxStackTrace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxStackTrace.Size = new System.Drawing.Size(579, 190);
            this.textBoxStackTrace.TabIndex = 4;
            // 
            // labelStackTrace
            // 
            this.labelStackTrace.AutoSize = true;
            this.labelStackTrace.Location = new System.Drawing.Point(3, 199);
            this.labelStackTrace.Name = "labelStackTrace";
            this.labelStackTrace.Size = new System.Drawing.Size(74, 17);
            this.labelStackTrace.TabIndex = 3;
            this.labelStackTrace.Text = "Stack Trace";
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxMessage.Location = new System.Drawing.Point(6, 82);
            this.textBoxMessage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxMessage.Multiline = true;
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.ReadOnly = true;
            this.textBoxMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxMessage.Size = new System.Drawing.Size(579, 61);
            this.textBoxMessage.TabIndex = 2;
            // 
            // labelExceptionMessage
            // 
            this.labelExceptionMessage.AutoSize = true;
            this.labelExceptionMessage.Location = new System.Drawing.Point(2, 59);
            this.labelExceptionMessage.Name = "labelExceptionMessage";
            this.labelExceptionMessage.Size = new System.Drawing.Size(61, 17);
            this.labelExceptionMessage.TabIndex = 0;
            this.labelExceptionMessage.Text = "Message";
            // 
            // tabPageScreenshot
            // 
            this.tabPageScreenshot.Controls.Add(this.linkLabelView);
            this.tabPageScreenshot.Controls.Add(this.groupBoxScreenshot);
            this.tabPageScreenshot.Controls.Add(this.checkBoxIncludeScreenshot);
            this.tabPageScreenshot.Location = new System.Drawing.Point(4, 26);
            this.tabPageScreenshot.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageScreenshot.Name = "tabPageScreenshot";
            this.tabPageScreenshot.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageScreenshot.Size = new System.Drawing.Size(591, 420);
            this.tabPageScreenshot.TabIndex = 2;
            this.tabPageScreenshot.Text = "Screenshot";
            this.tabPageScreenshot.UseVisualStyleBackColor = true;
            // 
            // linkLabelView
            // 
            this.linkLabelView.AutoSize = true;
            this.linkLabelView.Location = new System.Drawing.Point(451, 14);
            this.linkLabelView.Name = "linkLabelView";
            this.linkLabelView.Size = new System.Drawing.Size(126, 17);
            this.linkLabelView.TabIndex = 3;
            this.linkLabelView.TabStop = true;
            this.linkLabelView.Text = "View Full Screenshot";
            this.linkLabelView.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelViewLinkClicked);
            // 
            // groupBoxScreenshot
            // 
            this.groupBoxScreenshot.Controls.Add(this.pictureBoxScreenshot);
            this.groupBoxScreenshot.Location = new System.Drawing.Point(6, 41);
            this.groupBoxScreenshot.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxScreenshot.Name = "groupBoxScreenshot";
            this.groupBoxScreenshot.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxScreenshot.Size = new System.Drawing.Size(579, 371);
            this.groupBoxScreenshot.TabIndex = 2;
            this.groupBoxScreenshot.TabStop = false;
            this.groupBoxScreenshot.Text = "Screenshot";
            // 
            // pictureBoxScreenshot
            // 
            this.pictureBoxScreenshot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxScreenshot.Location = new System.Drawing.Point(3, 22);
            this.pictureBoxScreenshot.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBoxScreenshot.Name = "pictureBoxScreenshot";
            this.pictureBoxScreenshot.Size = new System.Drawing.Size(573, 345);
            this.pictureBoxScreenshot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxScreenshot.TabIndex = 0;
            this.pictureBoxScreenshot.TabStop = false;
            // 
            // checkBoxIncludeScreenshot
            // 
            this.checkBoxIncludeScreenshot.AutoSize = true;
            this.checkBoxIncludeScreenshot.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxIncludeScreenshot.Checked = true;
            this.checkBoxIncludeScreenshot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIncludeScreenshot.Location = new System.Drawing.Point(3, 12);
            this.checkBoxIncludeScreenshot.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBoxIncludeScreenshot.Name = "checkBoxIncludeScreenshot";
            this.checkBoxIncludeScreenshot.Size = new System.Drawing.Size(136, 21);
            this.checkBoxIncludeScreenshot.TabIndex = 1;
            this.checkBoxIncludeScreenshot.Text = "Include Screenshot";
            this.toolTip.SetToolTip(this.checkBoxIncludeScreenshot, "Add screenshot of error screen in your crash report.");
            this.checkBoxIncludeScreenshot.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.AllowDrop = true;
            this.buttonSave.Image = global::CrashReporterDotNET.Properties.Resources.save_as;
            this.buttonSave.Location = new System.Drawing.Point(328, 472);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(139, 56);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Save";
            this.buttonSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSaveClick);
            // 
            // buttonCacel
            // 
            this.buttonCacel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCacel.Image = global::CrashReporterDotNET.Properties.Resources.stop;
            this.buttonCacel.Location = new System.Drawing.Point(473, 472);
            this.buttonCacel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonCacel.Name = "buttonCacel";
            this.buttonCacel.Size = new System.Drawing.Size(139, 56);
            this.buttonCacel.TabIndex = 2;
            this.buttonCacel.Text = "Cancel";
            this.buttonCacel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCacel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonCacel.UseVisualStyleBackColor = true;
            // 
            // buttonSendReport
            // 
            this.buttonSendReport.Image = global::CrashReporterDotNET.Properties.Resources.email_go;
            this.buttonSendReport.Location = new System.Drawing.Point(183, 472);
            this.buttonSendReport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonSendReport.Name = "buttonSendReport";
            this.buttonSendReport.Size = new System.Drawing.Size(139, 56);
            this.buttonSendReport.TabIndex = 1;
            this.buttonSendReport.Text = "Send Report";
            this.buttonSendReport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSendReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonSendReport.UseVisualStyleBackColor = true;
            this.buttonSendReport.Click += new System.EventHandler(this.ButtonSendReportClick);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "HTML files(*.html)|*.html";
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
            // CrashReport
            // 
            this.AcceptButton = this.buttonSendReport;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCacel;
            this.ClientSize = new System.Drawing.Size(624, 539);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCacel);
            this.Controls.Add(this.buttonSendReport);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CrashReport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CrashReport";
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.TabPage tabPageExceptions;
        private System.Windows.Forms.Button buttonSendReport;
        private System.Windows.Forms.Button buttonCacel;
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
    }
}
using System;
using System.Windows.Forms;

namespace CrashReporterDotNET
{
    /// <inheritdoc />
    public partial class Screenshot : Form
    {
        /// <inheritdoc />
        public Screenshot(byte[] image)
        {
            InitializeComponent();
            webBrowser.DocumentText = $@"
                <!doctype html>
                <html>
                   <head>
                      <style>html,body{{padding:0;margin:0;height:100%;width:100%;}}img{{width:100%}}</style>
                   </head>
                   <body><img src=""data:image/png;base64,{Convert.ToBase64String(image)}"" /></body>
                </html>";
        }
    }
}

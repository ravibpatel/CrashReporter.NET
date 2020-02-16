using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CrashReporterDotNET
{
    public partial class Screenshot : Form
    {
        public Screenshot(byte[] image)
        {
            InitializeComponent();
            webBrowser1.DocumentText = $@"<!doctype html>
<html><head><style>html,body{{padding:0;margin:0;height:100%;width:100%;}}img{{width:100%}}</style></head><body><img alt="""" src=""data:image/png;base64,{Convert.ToBase64String(image)}"" /></body></html>";
        }
    }
}

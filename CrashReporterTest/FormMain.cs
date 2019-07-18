using System;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace CrashReporterTest
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void ButtonTestClick(object sender, EventArgs e)
        {
            ThrowException();
        }

        private void buttonThreadException_Click(object sender, EventArgs e)
        {
            var thread = new Thread(ThrowException);
            thread.Start();
        }

        private void ThrowException()
        {
            try
            {
                throw new ArgumentException();
            }
            catch (ArgumentException argumentException)
            {
                const string path = "test.txt";

                if (!File.Exists(path))
                {
                    throw new FileNotFoundException(
                        "File Not found when trying to write argument exception to the file", argumentException);
                }
            }
        }
    }
}

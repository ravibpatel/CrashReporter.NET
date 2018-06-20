using System;
using System.IO;
using System.Threading;
using System.Windows;

namespace CrashReporterWPFTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonCrash_Click(object sender, RoutedEventArgs e)
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
                try
                {
                    if (!File.Exists(path))
                    {
                        throw new FileNotFoundException(
                            "File Not found when trying to write argument exception to the file", argumentException);
                    }
                }
                catch (Exception exception)
                {
                    App.SendReport(exception, "Value of path variable is " + path);
                }
            }
        }
    }
}

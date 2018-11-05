using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using CrashReporterDotNET;

namespace CrashReporterWPFTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            Application.Current.DispatcherUnhandledException += DispatcherOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
        }

        private void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
        {
            SendReport(unobservedTaskExceptionEventArgs.Exception);
            Environment.Exit(0);
        }

        private void DispatcherOnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs dispatcherUnhandledExceptionEventArgs)
        {
            SendReport(dispatcherUnhandledExceptionEventArgs.Exception);
            Environment.Exit(0);
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            SendReport((Exception)unhandledExceptionEventArgs.ExceptionObject);
            Environment.Exit(0);
        }

        public static void SendReport(Exception exception, string developerMessage = "", bool silent = false)
        {
            var reportCrash = new ReportCrash("Email where you want to receive crash reports.")
            {
                DeveloperMessage = developerMessage,
                Silent = silent
            };
            reportCrash.Send(exception);
        }
    }
}

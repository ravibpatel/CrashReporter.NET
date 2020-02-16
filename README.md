# CrashReporter.NET

[![AppVeyor branch](https://img.shields.io/appveyor/ci/gruntjs/grunt/master.svg)](https://ci.appveyor.com/project/ravibpatel/crashreporter-net) [![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](http://paypal.me/rbsoft)

Send crash reports of your classic desktop application developed using .NET Framework directly to your mail's inbox with full exception report, stack trace and screenshot.

## The nuget package  [![NuGet](https://img.shields.io/nuget/v/CrashReporter.NET.Official.svg)](https://www.nuget.org/packages/CrashReporter.NET.Official/) [![NuGet](https://img.shields.io/nuget/dt/CrashReporter.NET.Official.svg)](https://www.nuget.org/packages/CrashReporter.NET.Official/)

    PM> Install-Package CrashReporter.NET.Official

## How it works

CrashReporter.NET uses the exception information like stack trace, exception type, message, source, .NET CLR version, OS version and application version to generate the crash report and send it to developer using email. It uses [DoctorDump](https://drdump.com/crash-reporting-system) service to send email to developer. Developers can use their SMTP server to send email too.

## Using the code

### Windows Forms Application

First thing you need to do is subscribe to Application.ThreadException and AppDomain.CurrentDomain.UnhandledException in your Program.cs file as shown below.

````csharp
static class Program
{
    private static ReportCrash _reportCrash;

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        Application.ThreadException += (sender, args) => SendReport(args.Exception);
        AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                SendReport((Exception)args.ExceptionObject);
            };
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        _reportCrash = new ReportCrash("Email where you want to receive crash reports")
        {
            DeveloperMessage = "Retry attempt",
            Silent = true,
            WebProxy = new WebProxy("Web proxy address, if needed"),
            DoctorDumpSettings = new DoctorDumpSettings
            {
                ApplicationID = new Guid("Application ID you received from DrDump.com"),
                OpenReportInBrowser = true
            }
        };
        _reportCrash.RetryFailedReports();
        Application.Run(new FormMain());
    }

    public static void SendReport(Exception exception, string developerMessage = "", bool silent = false)
    {
        _reportCrash.DeveloperMessage = developerMessage;
        _reportCrash.Silent = silent;
        _reportCrash.Send(exception);
    }
}
````

Just set the ToEmail in above example with your email to start receiving crash reports.

If you want to handle exception report for individual exception with special message you can do it like shown below.

````csharp
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
    Program.SendReport(exception, "Value of path variable is " + path);
}
````

### WPF Application

First thing you need to do is subscribe to AppDomain.CurrentDomain.UnhandledException in your App.xaml.cs file as shown below.

````csharp
public partial class App : Application
{
    private static ReportCrash _reportCrash;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
        Application.Current.DispatcherUnhandledException += DispatcherOnUnhandledException;
        TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
        _reportCrash = new ReportCrash("Email where you want to receive crash reports")
        {
            DeveloperMessage = "Retry attempt",
            Silent = true
        };
        _reportCrash.RetryFailedReports();
    }

    private void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
    {
        SendReport(unobservedTaskExceptionEventArgs.Exception);
    }

    private void DispatcherOnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs dispatcherUnhandledExceptionEventArgs)
    {
        SendReport(dispatcherUnhandledExceptionEventArgs.Exception);
    }

    private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
    {
        SendReport((Exception)unhandledExceptionEventArgs.ExceptionObject);
    }

    public static void SendReport(Exception exception, string developerMessage = "", bool silent = true)
    {
        _reportCrash.Send(exception);
    }
}
````

Just set the ToEmail in above example with your email to start receiving crash reports.

If you want to handle exception report for individual exception with special message you can do it like shown below.

````csharp
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
````

## Configuration Options

### Show screenshot tab

You can show screenshot tab by setting ShowScreenshotTab to true. It will be false by default.

````csharp
reportCrash.ShowScreenshotTab = true

### Include screenshot with crash report

You can set the IncludeScreenshot value to false if you don't want to include screenshot in the crash report. If you are showing the screenshot tab then user can still choose to include the screenshot even if you set it to false.

````csharp
reportCrash.IncludeScreenshot = false;
````

### Send reports silently

You can send crash reports silently by setting Silent property to true.

````csharp
reportCrash.Silent = true;
````

### Send reports using a web proxy

You can send crash report using a web proxy by adding following line in SendReport method.

````csharp
reportCrash.WebProxy = new WebProxy("Web proxy address"),
````

### Send reports to your DrDump account

You can send crash report to you doctor dump account by adding following line in SendReport method.

````csharp
reportCrash.DoctorDumpSettings = new DoctorDumpSettings
{
    ApplicationID = new Guid("Application ID you received from DrDump.com"),
};
````

Just set the ApplicationID to ID you received from DrDump.com.

### Capture whole screen instead of application screen

You can take screenshot of whole screen instead of only application when application crashes by adding following line in SendReport method.

````csharp
reportCrash.CaptureScreen = true;
````
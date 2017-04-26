# CrashReporter.NET
Send crash reports of your classic desktop application developed using .NET Framework directly to your mail's inbox with full exception report, stack trace and screenshot.

## The nuget package  [![NuGet](https://img.shields.io/nuget/v/CrashReporter.NET.Official.svg)](https://www.nuget.org/packages/CrashReporter.NET.Official/)
https://www.nuget.org/packages/CrashReporter.NET.Official/

    PM> Install-Package CrashReporter.NET.Official

## How it works

CrashReporter.NET uses the exception information like stack trace, exception type, message, source, .NET CLR version, OS version and application version to generate the crash report and send it to developer using email. It uses DoctorDump service (http://drdump.com) to send email to developer. Developers can use their SMTP server to send email too.

## Using the code

First thing you need to do is subscribe to Application.ThreadException and AppDomain.CurrentDomain.UnhandledException in your Program.cs file as shown below.

````csharp
internal static class Program
{
    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        Application.ThreadException += ApplicationThreadException;

        AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FormMain());
    }

    private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
    {
        ReportCrash((Exception)unhandledExceptionEventArgs.ExceptionObject);
        Environment.Exit(0);
    }

    private static void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
    {
        ReportCrash(e.Exception);
    }

    public static void ReportCrash(Exception exception,  string developerMessage = "")
    {
        var reportCrash = new ReportCrash
        {
            DeveloperMessage = developerMessage,
            ToEmail = "Email address where you want to receive crash reports"
        };

        reportCrash.Send(exception);
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
    Program.ReportCrash(exception, "Value of path variable is " + path);
}
````

## Configuration Options
### Change Language

You can change the language of the crash report dialog by adding following line in ReportCrash method of Program.cs file.

````csharp
reportCrash.CurrentCulture = CultureInfo.CreateSpecificCulture("ru");
````

In above example CrashReporter.NET will show crash report dialog in russian language.

### Send reports to your DrDump account

You can send crash report to you doctor dump account by adding following line in ReportCrash method of Program.cs file.

````csharp
reportCrash.DoctorDumpSettings = new DoctorDumpSettings
{
    ApplicationID = new Guid("Application ID you received from DrDump.com"),
};
````

Just set the ApplicationID to ID you received from DrDump.com.

### Capture whole screen instead of Application screen

You can take screenshot of whole screen instead of only application when application crashes by adding following line in ReportCrash method of Program.cs file.

````csharp
reportCrash.CaptureScreen = true;
````
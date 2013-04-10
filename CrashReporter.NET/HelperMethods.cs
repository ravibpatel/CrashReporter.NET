using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CrashReporterDotNET
{
    internal static class HelperMethods
    {
        [DllImport("kernel32", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr LoadLibrary(string libraryName);

        [DllImport("kernel32", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr GetProcAddress(IntPtr hwnd, string procedureName);

        private static bool IsOS64Bit()
        {
            return IntPtr.Size == 8 || (IntPtr.Size == 4 && Is32BitProcessOn64BitProcessor());
        }

        private static IsWow64ProcessDelegate GetIsWow64ProcessDelegate()
        {
            IntPtr handle = LoadLibrary("kernel32");

            if (handle != IntPtr.Zero)
            {
                IntPtr fnPtr = GetProcAddress(handle, "IsWow64Process");

                if (fnPtr != IntPtr.Zero)
                {
                    return
                        (IsWow64ProcessDelegate)
                        Marshal.GetDelegateForFunctionPointer(fnPtr, typeof(IsWow64ProcessDelegate));
                }
            }

            return null;
        }

        private static bool Is32BitProcessOn64BitProcessor()
        {
            IsWow64ProcessDelegate fnDelegate = GetIsWow64ProcessDelegate();

            if (fnDelegate == null)
            {
                return false;
            }

            bool isWow64;
            bool retVal = fnDelegate.Invoke(Process.GetCurrentProcess().Handle, out isWow64);

            if (retVal == false)
            {
                return false;
            }

            return isWow64;
        }

        private delegate bool IsWow64ProcessDelegate([In] IntPtr handle, [Out] out bool isWow64Process);

        public static string GetWindowsVersion()
        {
            string osArchitecture;
            Version windowsVersion = Environment.OSVersion.Version;
            try
            {
                osArchitecture = IsOS64Bit() ? "64" : "32";
            }
            catch (Exception)
            {
                osArchitecture = "32/64 bit (Undetermine)";
            }
            switch (windowsVersion.Major)
            {
                case 5:
                    switch (windowsVersion.Minor)
                    {
                        case 0:
                            return string.Format("Windows 2000 {0} {1} Version {2}",
                                                 Environment.OSVersion.ServicePack, osArchitecture,
                                                 windowsVersion);
                        case 1:
                            return string.Format("Windows XP {0} {1} Version {2}",
                                                 Environment.OSVersion.ServicePack, osArchitecture,
                                                 windowsVersion);
                        case 2:
                            return string.Format(
                                "Windows XP x64 Professional Edition / Windows Server 2003 {0} {1} Version {2}",
                                Environment.OSVersion.ServicePack, osArchitecture, windowsVersion);
                    }
                    break;
                case 6:
                    switch (Environment.OSVersion.Version.Minor)
                    {
                        case 0:
                            return string.Format("Windows Vista {0} {1} bit Version {2}",
                                                 Environment.OSVersion.ServicePack, osArchitecture,
                                                 windowsVersion);
                        case 1:
                            return string.Format("Windows 7 {0} {1} bit Version {2}",
                                                 Environment.OSVersion.ServicePack, osArchitecture,
                                                 windowsVersion);
                        case 2:
                            return string.Format("Windows 8 {0} {1} bit Version {2}",
                                                 Environment.OSVersion.ServicePack, osArchitecture,
                                                 windowsVersion);
                    }
                    break;
            }

            return string.Format("Unknown {0} bit Version {1}", osArchitecture,
                                         windowsVersion);
        }
    }
}

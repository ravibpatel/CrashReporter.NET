using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32;

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

        private static string HKLM_GetString(string key, string value)
        {
            try
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(key);
                if (registryKey == null) return "";
                return (string)registryKey.GetValue(value);
            }
            catch { return ""; }
        }

        private delegate bool IsWow64ProcessDelegate([In] IntPtr handle, [Out] out bool isWow64Process);

        public static string GetWindowsVersion()
        {
            string osArchitecture;
            try
            {
                osArchitecture = IsOS64Bit() ? "64-bit" : "32-bit";
            }
            catch (Exception)
            {
                osArchitecture = "32/64-bit (Undetermined)";
            }
            string productName = HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName");
            string csdVersion = HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CSDVersion");
            string currentBuild = HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentBuild");
            if (productName != "")
            {
                return string.Format("{0}{1}{2}{3} (OS Build {4})", (productName.StartsWith("Microsoft") ? "" : "Microsoft "), productName, (csdVersion != "" ? " " + csdVersion : ""), osArchitecture, currentBuild);
            }
            return "";
        }
    }
}

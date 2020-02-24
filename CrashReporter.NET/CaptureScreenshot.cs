using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CrashReporterDotNET
{
    internal static class CaptureScreenshot
    {
        public static byte[] CaptureScreen(ImageFormat imageFormat)
        {
            var screenLeft = SystemInformation.VirtualScreen.Left;
            var screenTop = SystemInformation.VirtualScreen.Top;
            var screenWidth = SystemInformation.VirtualScreen.Width;
            var screenHeight = SystemInformation.VirtualScreen.Height;

            using (var bitmap = new Bitmap(screenWidth, screenHeight))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(screenLeft, screenTop, 0, 0, bitmap.Size);
                }

                using (var ms = new System.IO.MemoryStream())
                {
                    bitmap.Save(ms, imageFormat);
                    return ms.ToArray();
                }
            }
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public static byte[] CaptureActiveWindow( ImageFormat imageFormat)
        {
            var foregroundWindowHandle = GetForegroundWindow();
            var rect = new Rect();
            GetWindowRect(foregroundWindowHandle, ref rect);
            var bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            using (var bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                }

                using (var ms = new System.IO.MemoryStream())
                {
                    bitmap.Save(ms, imageFormat);
                    return ms.ToArray();
                }
            }
        }
    }
}
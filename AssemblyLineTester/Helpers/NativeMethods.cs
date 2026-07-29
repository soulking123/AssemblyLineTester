using System;
using System.Runtime.InteropServices;

namespace AssemblyLineTester.Helpers
{
    internal static class NativeMethods
    {
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int WM_SETREDRAW = 0x000B;
        private const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ReleaseCapture();

        // Hands the drag back to Windows, so a borderless form moves with the title bar.
        public static void DragWindow(IntPtr handle)
        {
            ReleaseCapture();
            SendMessage(handle, WM_NCLBUTTONDOWN, HT_CAPTION, IntPtr.Zero);
        }

        // Suspends painting while a control tree is rebuilt, so the rebuild is not drawn in stages.
        public static void SetRedraw(IntPtr handle, bool enabled)
        {
            SendMessage(handle, WM_SETREDRAW, enabled ? 1 : 0, IntPtr.Zero);
        }
    }
}

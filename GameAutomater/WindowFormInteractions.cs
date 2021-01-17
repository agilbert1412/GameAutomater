using BTD6Automater;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Interactions;

namespace GameAutomater
{
    public class WindowFormInteractions : WindowInteractions
    {
        #region Methods

        public void Focus(string name)
        {
            var p = Process.GetProcessesByName(name).FirstOrDefault();
            if (p != null)
            {
                IntPtr h = p.MainWindowHandle;
                SetForegroundWindow(h);
            }
        }

        public void SendClick(int x, int y)
        {
            Cursor.Position = new Point(x, y);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public void PlaceCursor(int x, int y)
        {
            Cursor.Position = new Point(x, y);
        }

        public void SendKey(string key)
        {
            SendKeys.SendWait(key);
        }

        public Point GetCursorLocation()
        {
            return Cursor.Position;
        }

        public void MinimizeCurrentWindow()
        {
            IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;

            ShowWindow(handle, WINDOW_MINIMIZE);
        }

        public void MaximizeCurrentWindow()
        {
            IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;

            ShowWindow(handle, WINDOW_MAXIMIZE);
        }

        #endregion Methods

        #region External Methods

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow([In] IntPtr hWnd, [In] int nCmdShow);

        #endregion External Methods

        #region Flags

        // Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        private const int MOUSEEVENTF_MOVE = 0x0001;

        // Window Actions
        private const int WINDOW_MINIMIZE = 6;
        private const int WINDOW_MAXIMIZE = 9;

        #endregion Flags
    }
}

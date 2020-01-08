using BTD6Automater;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameAutomater
{
    public class WindowFormInteractions : WindowInteractions
    {
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
            var curMousePos = Cursor.Position;
            Console.WriteLine("Cursor is currently at " + curMousePos.ToString() + ", trying to click at [" + x + ", " + y + "]");

            Cursor.Position = new Point(x, y);

            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public void SendKey(string key)
        {
            SendKeys.SendWait(key);
        }

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        public Point GetCursorLocation()
        {
            return Cursor.Position;
        }

        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        private const int MOUSEEVENTF_MOVE = 0x0001;
    }
}

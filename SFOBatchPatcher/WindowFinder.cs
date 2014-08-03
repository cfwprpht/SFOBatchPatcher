using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ConsoleControl
{
    /// <summary>
    /// The WindowFinder Class is to find the correct Window we use for ConsoleControl.
    /// </summary>
    public class WindowFinder
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
        [DllImport("user32.dll")]
        static extern bool EnumDesktopWindows(IntPtr hDesktop,
           EnumWindowsProc lpfn, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        /// <summary>
        /// The WindowFinder Cast
        /// </summary>
        public WindowFinder()
        {
            ewp = new EnumWindowsProc(EnumWindowFunction);
        }

        /// <summary>
        /// Find our Window by the Process ID.
        /// </summary>
        /// <param name="processId">The Process ID to search for.</param>
        /// <returns></returns>
        public IntPtr FindWindowHandleByProcessId(int processId)
        {
            pid = processId;
            EnumDesktopWindows(IntPtr.Zero, ewp, IntPtr.Zero);
            return windowHandle;
        }

        private EnumWindowsProc ewp;

        private bool EnumWindowFunction(IntPtr hWnd, IntPtr lParam)
        {
            //  Is it the right window?
            uint pID = 0;
            GetWindowThreadProcessId(hWnd, out pID);
            if (pID == pid)
            {
                windowHandle = hWnd;
                return false;
            }
            return true;
        }

        IntPtr windowHandle = IntPtr.Zero;
        int pid = 0;
    }
}

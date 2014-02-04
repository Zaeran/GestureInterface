//Authored by Nathan Beattie

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Media;


namespace ImgTest
{
    /// <summary>
    /// Gestures obtain their methods to invoke from this class.
    /// Any method written here can be assigned to a gesture.
    /// All methods MUST be public.
    /// </summary>
    public class Methods
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        
        /// <summary>
        /// Exits the program
        /// </summary>
        public void ExitProgram()
        {
            Environment.Exit(0);
        }

        public void Message()
        {
            MessageBox.Show("HELLO WORLD!");
        }

        public void Sound()
        {
            SystemSounds.Beep.Play();
        }

        public void OpenSong()
        {
            Process.Start(@"C:\\PicTest/06 - The Devil In The Wishing Well.mp3");
        }

        public void CloseWindow()
        {
            uint processID;
            GetWindowThreadProcessId(GetForegroundWindow(), out processID);
            Process p = Process.GetProcessById((int)processID);
            p.CloseMainWindow();
        }

        public void One()
        {
            SendKeys.SendWait("1");
        }

        public void Two()
        {
            SendKeys.SendWait("2");
        }

        public void Three()
        {
            SendKeys.SendWait("3");
        }

        public void Four()
        {
            SendKeys.SendWait("4");
        }

        public void Five()
        {
            SendKeys.SendWait("5");
        }

        public void Six()
        {
            SendKeys.SendWait("6");
        }

        public void Seven()
        {
            SendKeys.SendWait("7");
        }

        public void Eight()
        {
            SendKeys.SendWait("8");
        }

        public void Nine()
        {
            SendKeys.SendWait("9");
        }

        public void Zero()
        {
            SendKeys.SendWait("0");
        }

        public void OpenNotepad()
        {
            Process.Start(@"C:\Windows\system32\notepad.exe");
        }

        public void Tab()
        {
            SendKeys.SendWait("{TAB}");
        }

        public void Enter()
        {
            SendKeys.SendWait("~");
        }
    }
}

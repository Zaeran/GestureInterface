//Authored by Nathan Beattie

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

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

        public void NextSlidePPT()
        {
            if (Process.GetProcessesByName("POWERPNT").Length != 0)
            {
                PowerPoint.Application pptApp;
                pptApp = (PowerPoint.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("PowerPoint.Application");
                if (pptApp.SlideShowWindows.Count != 0)
                {
                    pptApp.ActivePresentation.SlideShowWindow.View.Next();
                }
            } 
        }

        public void PrevSlidePPT()
        {
            if (Process.GetProcessesByName("POWERPNT").Length != 0)
            {
                PowerPoint.Application pptApp;
                pptApp = (PowerPoint.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("PowerPoint.Application");
                if (pptApp.SlideShowWindows.Count != 0)
                {
                    pptApp.ActivePresentation.SlideShowWindow.View.Previous();
                }
            }
        }

    }
}

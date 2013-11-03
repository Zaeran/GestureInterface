//Authored by Nathan Beattie

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImgTest
{
    /// <summary>
    /// Gestures obtain their methods to invoke from this class.
    /// Any method written here can be assigned to a gesture.
    /// </summary>
    public class Methods
    {
        /// <summary>
        /// Exits the program
        /// </summary>
        public void ExitProgram()
        {
            Environment.Exit(0);
        }
    }
}

/* 
 Swoopae's MediaTek65x2 Porting Tool
        (C) Swoopae 2017
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MediaTek65xxTool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows;
namespace Restaurante
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
      
         
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        frmMain start_instance = new frmMain();
    //        datenTransfer start_instance = new datenTransfer();
       
            Application.Run(start_instance);
        }
        
    }
}

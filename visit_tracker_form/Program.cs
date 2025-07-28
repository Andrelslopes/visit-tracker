using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using visit_tracker;

namespace visit_tracker_form
{
    internal static class Program
    {
        public static string connect = System.Configuration.ConfigurationManager.ConnectionStrings["connectionDB"].ConnectionString;
        
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frm_Visit());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace smacop2
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Administracion.frmusuarios());
            //Application.Run(new Reportes.Reporte03());
            //Application.Run(new Consultas.frmproduccionmescm());
            Application.Run(new frmmenu());
            //Application.Run(new frmprueba());
        }
    }
}

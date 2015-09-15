using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba
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
            if (MessageBox.Show("¿ES UN TERMINAL KIOSCO?", "INICIO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MessageBox.Show("TERMINAL KIOSCO");
            }
            else new Login.Login().Show();
            Application.Run();
        }
    }
}

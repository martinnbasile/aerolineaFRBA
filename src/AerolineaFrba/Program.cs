using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        /// 
        public static String rol;
        public static String username;
        public static String password;
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ConexionALaBase.Conexion.verificarConexion();
            //ConexionALaBase.Conexion.ejecutarNonQuery("exec actualizarFecha(" + System.DateTime.Today.ToShortDateString() + ")");
            /* Ale lo va a crear y hace el convert adentro */
            if (MessageBox.Show("¿ES UN TERMINAL KIOSCO?", "INICIO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                rol = "Cliente";
                username = "";
                password = "";
                new Funcionalidades.Funcionalidades().Show();
            }
            else new Login.Login().Show();
            Application.Run();
        }
    }
}

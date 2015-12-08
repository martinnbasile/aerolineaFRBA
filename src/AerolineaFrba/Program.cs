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
        public static String fechaSistema;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ConexionALaBase.Conexion.verificarConexion();
            DateTime fecha = Convert.ToDateTime(AerolineaFrba.Properties.Settings.Default.fechaDelSistema);
            fechaSistema = fecha.ToString("dd/MM/yyyy");
            ConexionALaBase.Conexion.ejecutarNonQuery("exec MM.actualizarFecha '" + fechaSistema + "'");
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

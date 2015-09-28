using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AerolineaFrba.Abm_Rol
{
    public partial class crearRol : Form
    {
        public crearRol()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new buscarRol().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand unComando=ConexionALaBase.Conexion.getComando();
            unComando.CommandText = "insert into roles values (3,'masterr')";
            unComando.ExecuteNonQuery();
        }
    }
}

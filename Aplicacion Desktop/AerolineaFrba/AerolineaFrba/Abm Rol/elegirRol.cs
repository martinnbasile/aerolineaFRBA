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
    public partial class elegirRol : Form
    {
        public elegirRol()
        {
            InitializeComponent();
        }

        private void elegirRol_Load(object sender, EventArgs e)
        {
            SqlDataReader reader = ConexionALaBase.Conexion.consultarBase("Select descri from roles");
            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetSqlString(0));
            }
            this.Show();
            reader.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Abm_Ruta
{
    public partial class modificarRuta : Form
    {
        public modificarRuta()
        {
            InitializeComponent();
            ConexionALaBase.CargadorDeEstructuras.cargarDataGrid(dataGridView1, "select * from Vista_rutas_aereas");
        }

        private void modificarRuta_Load(object sender, EventArgs e)
        {

        }
    }
}

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
    public partial class ABM_RUTA : Form
    {
        public ABM_RUTA()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new NuevaRuta().Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) //modificar
        {
            new SeleccionarRuta(1).Show();
            this.Close();
            return;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Funcionalidades.Funcionalidades().Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new SeleccionarRuta(2).Show();
            this.Close();
            return;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Listado_Estadistico
{
    public partial class verListado : Form
    {
        public verListado(String listadoElegido)
        {
            InitializeComponent();
            label1.Text = listadoElegido;
        }

        private void button1_Click(object sender, EventArgs e) //VOLVER
        {
            new ElegirListado().Show();
            this.Close();
        }
    }
}

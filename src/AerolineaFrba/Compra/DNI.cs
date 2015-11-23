using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Compra
{
    public partial class DNI : Form
    {
        public DNI(int idViaje, int cantidadPasajes, int cantidadKilos)
        {
            InitializeComponent();
            label1.Text = "Ingrese DNI";
            maskedTextBox1.Mask = "9999999";
            
        }

        private void button2_Click(object sender, EventArgs e)
        {//VOLVER
            new compra().Show();
            this.Close();
        }
    }
}

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
        LaCompra compraRecibida;
        public DNI(LaCompra unaCompra)
        {
            InitializeComponent();
            label1.Text = "Ingrese DNI";
            maskedTextBox1.Mask = "9999999";
            compraRecibida = unaCompra;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {//VOLVER
            new compra().Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {//NEXT
            if (Validaciones.Validaciones.validarMaskedTextBox(maskedTextBox1,"Ingrese su DNI"))
            {
                compraRecibida.dniCliente =int.Parse(maskedTextBox1.Text);
                new datosDelCliente(compraRecibida).Show(); ;
                this.Close();
            }
            
        }
    }
}

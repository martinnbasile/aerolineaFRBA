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
    public partial class seleccionarMedioPago : Form
    {
        LaCompra unaCompra;
        public seleccionarMedioPago(LaCompra compraRecibida)
        {
            InitializeComponent();
            unaCompra = compraRecibida;
            button1.Text = "Siguiente";
            button2.Text = "Cancelar";
        }

        private void seleccionarMedioPago_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Tarjeta de credito");
            if (Program.rol != "Cliente")
            {
                comboBox1.Items.Add("Efectivo");
            }
                

        }

        private void button2_Click(object sender, EventArgs e)
        {//VOLVER
            ConexionALaBase.Conexion.ejecutarNonQuery("Rollback transaction compra");
            new compra().Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        { //NEXT
            if (Validaciones.Validaciones.validarComboBox(comboBox1, "Elija el medio de pago"))
            {
                
                MessageBox.Show("A continuación, ingrese los datos de la persona que abona");
                unaCompra.instanciaDeCompra = comboBox1.Text;
                new DNI(unaCompra).Show();
                this.Close();
            }

        }
    }
}

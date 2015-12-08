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
    public partial class ingresarDatosTC : Form
    {
        LaCompra unaCompra;
        Pasajero elPasajero;
        public ingresarDatosTC(LaCompra compra,Pasajero pasajero)
        {
            InitializeComponent();
            unaCompra = compra;
            elPasajero = pasajero;
        }

        private void ingresarDatosTC_Load(object sender, EventArgs e)
        {
            numericUpDown1.Maximum = 12;
            numericUpDown1.Minimum = 1;
            numericUpDown2.Maximum = DateTime.Parse(Properties.Settings.Default.fechaDelSistema).Year + 10;
            numericUpDown2.Minimum = DateTime.Parse(Properties.Settings.Default.fechaDelSistema).Year;
            numericUpDown1.Value = DateTime.Parse(Properties.Settings.Default.fechaDelSistema).Month;
            numericUpDown2.Value = DateTime.Parse(Properties.Settings.Default.fechaDelSistema).Year;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {//VOLVER
            unaCompra.comandoT.Transaction.Rollback();
            new compra().Show();
            this.Close();
        }


        private bool validarFecha()
        {
            if (numericUpDown2.Value==DateTime.Parse(Properties.Settings.Default.fechaDelSistema).Year)
            {
                if (numericUpDown1.Value < DateTime.Parse(Properties.Settings.Default.fechaDelSistema).Month)
                {
                    MessageBox.Show("La fecha de vencimiento debe ser posterior a la actual");
                    return false;
                }
            }

            return true;

        }

        private bool validarTodo()
        {   
            String numT = maskedTextBox1.Text;
            String codSeg = maskedTextBox2.Text;
            if (numT.Length == 16)
            {
                if (codSeg.Length == 3)
                {
                    return true;
                }
                else MessageBox.Show("El codigo de seguridad son 3 numeros");
            }
            else MessageBox.Show("El numero de tarjeta debe tener 16 numeros");
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {//CONFIRMAR
              

            if ((this.validarFecha()) & (this.validarTodo()))
            {
            ConexionALaBase.Conexion.ejecutarNonQuery(unaCompra.comandoT,"exec mm.ingresarTC "+ maskedTextBox1.Text+", " + maskedTextBox2.Text+", "+numericUpDown2.Value+", "+numericUpDown1.Value+","+unaCompra.codigoCompra);
            if (unaCompra.cantidadKgs > 0)
            {
                ConexionALaBase.Conexion.ejecutarNonQuery(unaCompra.comandoT, "exec mm.ingresarCompraPaquete " + unaCompra.idViaje + ", " + elPasajero.dni + ", " + unaCompra.cantidadKgs + " , " + unaCompra.codigoCompra + "," + unaCompra.precioPaquete);
            }
            ConexionALaBase.Conexion.ejecutarNonQuery(unaCompra.comandoT, "exec mm.asentarCompra " + unaCompra.codigoCompra + ", " + elPasajero.dni + ", " + unaCompra.totalCompra() + ", 'Tarjeta: "+maskedTextBox1.Text+"'");
            MessageBox.Show("Total: " + unaCompra.totalCompra());
            MessageBox.Show("Operacion exitosa. Codigo de compra: " + unaCompra.codigoCompra);
            unaCompra.comandoT.Transaction.Commit();
            new Funcionalidades.Funcionalidades().Show();
            this.Close();
            }

        }
    }
}

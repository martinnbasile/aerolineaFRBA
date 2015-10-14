using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Abm_Ciudad
{
    public partial class crearCiudad : Form
    {
        public crearCiudad()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new buscarCiudad().Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Validaciones.Validaciones.validarTextBox(textBox2, "Ingrese un nombre para la ciudad a crear"))
            {   
                String nombreNuevaCiudad = textBox2.Text;
                ConexionALaBase.Conexion.ejecutarNonQuery("insert into ciudades values ('" + nombreNuevaCiudad + "')");
                MessageBox.Show("Creada la ciudad " + nombreNuevaCiudad + "");
                new buscarCiudad().Show();
                this.Close();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

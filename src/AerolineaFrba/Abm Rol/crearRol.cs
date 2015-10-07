using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AerolineaFrba.Validaciones;

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
            Validaciones.Validaciones.validarTextBox(textBox2, "Complete el nombre del rol");
            if (comboBox2.SelectedIndex == -1) { MessageBox.Show("Complete el username"); return; }

        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

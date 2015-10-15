using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Abm_Aeronave
{
    public partial class bajaFueraDeServicio : Form
    {
        String aeronave;
        public bajaFueraDeServicio(String unAeronave)
        {
            aeronave = unAeronave;
            InitializeComponent();
        }

        private void bajaFueraDeServicio_Load(object sender, EventArgs e)
        {

        }

        public void recibirFecha(DateTime unaFecha)
        {
            String fechaReinicioDeServicio = unaFecha.ToShortDateString();
            textBox1.Text = fechaReinicioDeServicio;
        }

   
        private void button3_Click(object sender, EventArgs e)
        {
            new calendario(this).Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

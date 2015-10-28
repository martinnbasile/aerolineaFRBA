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
    public partial class buscarAeronave : Form
    {
        public buscarAeronave()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Funcionalidades.Funcionalidades().Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
          // if (Validaciones.Validaciones.validarListBox(listBox1, "Selecciona una Aeronave a dar de baja por Vida Util"))
          //  {
          //  }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           // if (Validaciones.Validaciones.validarListBox(listBox1, "Selecciona una Aeronave a dar de baja por Fuera de Servicio"))
           // {
           //     String aeronaveSeleccionada = listBox1.Text;
           //     new bajaFueraDeServicio(aeronaveSeleccionada).Show();
           //     this.Close();
            // }
        }

        private void buscarAeronave_Load(object sender, EventArgs e)
        {
            
            ConexionALaBase.CargadorDeEstructuras.cargarDataGrid(dataGridView1, "select * from vista_aeronaves");
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

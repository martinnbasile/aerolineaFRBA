using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Devolucion
{
    public partial class seleccionarPasajesEncomiendas : Form
    {
        int idCliente;
        public seleccionarPasajesEncomiendas(int unIdCliente)
        {
            InitializeComponent();
            idCliente = unIdCliente;
        }

        private void seleccionarPasajesEncomiendas_Load(object sender, EventArgs e)
        {
            ConexionALaBase.CargadorDeEstructuras.cargarDataGrid(dataGridView1, "exec MM.pasajesCliente @idCliente=" + idCliente + "");
            ConexionALaBase.CargadorDeEstructuras.cargarDataGrid(dataGridView2, "exec MM.paquetesCliente @idCliente=" + idCliente + "");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Se cancelaran los pasajes y encomiendas seleccionados, ¿Desea continuar?'", "Aviso", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

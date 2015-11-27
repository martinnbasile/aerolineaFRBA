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
            if (estadoValido())
            {           //@codigoCompra int,@butaca int, @motivo varchar
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    int codigoDeCompraDePasaje = Convert.ToInt32(row.Cells["Codigo de compra"].Value.ToString());
                    int numeroDeButaca = Convert.ToInt32(row.Cells["Numero de butaca"].Value.ToString());
                    String motivo = textBox1.Text;
                    ConexionALaBase.Conexion.ejecutarNonQuery("exec MM.cancelarCompraPasaje @codigoCompra=" + codigoDeCompraDePasaje + ", @butaca=" + numeroDeButaca + ", @motivo='" + motivo + "'");              
                }
                foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                {
                    int codigoDeCompraDePaquete = Convert.ToInt32(row.Cells["Codigo de compra"].Value.ToString());
                    String motivo = textBox1.Text;
                    ConexionALaBase.Conexion.ejecutarNonQuery("exec MM.cancelarCompraPaquete @codigoCompra="+codigoDeCompraDePaquete+", @motivo='"+motivo+"'");
                }          
           }          
        }

        private bool estadoValido()
        {
            bool seleccionoAlgunPasaje = dataGridView1.SelectedRows.Count != 0;
            bool seleccionoAlgunPaquete = dataGridView2.SelectedRows.Count != 0;

            if (!(seleccionoAlgunPasaje || seleccionoAlgunPaquete))
            {
                MessageBox.Show("Seleccione por lo menos un pasaje o paquete para ser cancelado");
                return false;
            }

            if (seleccionoAlgunPasaje)
            {
                bool ingresoMotivoCancelacionPasajes = textBox1.Text.Length != 0;
                if (!ingresoMotivoCancelacionPasajes)
                {
                    MessageBox.Show("Ingrese el motivo por el cual quiere cancelar los pasajes seleccionados");
                    return false;
                }
            }
            if (seleccionoAlgunPaquete)
            {
                bool ingresoMotivoCancelacionPaquetes = textBox2.Text.Length != 0;
                if (!ingresoMotivoCancelacionPaquetes)
                {
                    MessageBox.Show("Ingrese el motivo por el cual quiere cancelar los paquetes seleccionados");
                    return false;

                }
            }
            return true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

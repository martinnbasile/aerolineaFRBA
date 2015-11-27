using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace AerolineaFrba.Devolucion
{
    public partial class seleccionarPasajesEncomiendas : Form
    {
        int codigoDeCompra;
        public seleccionarPasajesEncomiendas(int unCodigoDeCompra)
        {
            InitializeComponent();
            codigoDeCompra = unCodigoDeCompra;
        }

        private void seleccionarPasajesEncomiendas_Load(object sender, EventArgs e)
        {
            ConexionALaBase.CargadorDeEstructuras.cargarDataGrid(dataGridView1, "exec MM.vista_pasajes_cancelables @idCompra="+codigoDeCompra+"");
            ConexionALaBase.CargadorDeEstructuras.cargarDataGrid(dataGridView2, "exec MM.vista_paquetes_cancelables @idCompra="+codigoDeCompra+"");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (estadoValido())
            {
                String motivo = textBox1.Text;
                //ConexionALaBase.Conexion.ejecutarNonQuery("Begin transaction cancelacion");
                //ConexionALaBase.Conexion.ejecutarNonQuery("set transaction isolation level serializable");
                ConexionALaBase.Conexion.ejecutarNonQuery("mm.nuevaCancelacion @motivo='"+motivo+"',@codCompra="+codigoDeCompra+"");
                SqlCommand comando = ConexionALaBase.Conexion.getComando();
                comando.CommandText = "SELECT mm.ultimacancelacion()";
                int codigoDeCancelacion = (Int32) comando.ExecuteScalar();
                
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    int codigoDePasaje = Convert.ToInt32(row.Cells["Codigo de pasaje"].Value.ToString());
                    ConexionALaBase.Conexion.ejecutarNonQuery("exec mm.cancelacionPasaje @codPasaje="+codigoDePasaje+",@codCancelacion="+codigoDeCancelacion+"");              
                }
                foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                {
                    int codigoDePaquete = Convert.ToInt32(row.Cells["Codigo de paquete"].Value.ToString());
                    ConexionALaBase.Conexion.ejecutarNonQuery("exec mm.cancelacionPaquete @codPaquete="+codigoDePaquete+",@codCancelacion="+codigoDeCancelacion+"");
                }
               // ConexionALaBase.Conexion.ejecutarNonQuery("commit transaction cancelacion");
                MessageBox.Show("Se han cancelado satisfactoriamente los pasajes y paquetes seleccionados");
                new seleccionarPasajesEncomiendas(codigoDeCompra).Show();
                this.Close();
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

            bool ingresoMotivoCancelacion = textBox1.Text.Length != 0;
            if (!ingresoMotivoCancelacion)
            {
                MessageBox.Show("Ingrese el motivo de la cancelacion");
                return false;
            }
            
            return true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new ingresarCodigoDeCompra().Show();
            this.Close();
        }
    }
}

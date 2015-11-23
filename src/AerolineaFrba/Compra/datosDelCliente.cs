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
    public partial class datosDelCliente : Form
    {
        LaCompra compraRecibida;
        public datosDelCliente(LaCompra unaCompra)
        {
            InitializeComponent();
            compraRecibida = unaCompra;
        }

        private void datosDelCliente_Load(object sender, EventArgs e)
        
        {
            DataGridView unDataGrid = new DataGridView();

            try
            {
                ConexionALaBase.CargadorDeEstructuras.cargarDataGrid(unDataGrid, "Select * from mm.clientes where DNI=" + compraRecibida.dniCliente);
                //DataGridViewRow cliente = unDataGrid.sel
               // estaba = true;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //EL CLIENTE NO ESTABA, DEBE SER INGRESADO
                //ESstaba=false
            }
        }
    }
}

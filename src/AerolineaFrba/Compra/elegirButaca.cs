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
    public partial class elegirButaca : Form
    {
        LaCompra unaCompra;
        Pasajero unPasajero;
        public elegirButaca(LaCompra compraRecibida,Pasajero pasajeroRecibido)
        {
            InitializeComponent();
            unaCompra = compraRecibida;
            unPasajero = pasajeroRecibido;
        }

        private void elegirButaca_Load(object sender, EventArgs e)
        {
            ConexionALaBase.CargadorDeEstructuras.cargarDataGrid(unaCompra.comandoT,dataGridView1,"Select * from  mm.butacasDisponibles("+unaCompra.idViaje+")");
            button1.Text = "Confirmar";
            button2.Text = "Cancelar";
        }

        private bool validarSeleccionDeButaca()
        {
            if (Validaciones.Validaciones.validarDataGridView(dataGridView1,"Elija su butaca"))
            {
                return true;
            }
            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {//CANCELAR
            unaCompra.comandoT.Transaction.Rollback();
            new compra().Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {//CONFIRMA
            if (this.validarSeleccionDeButaca())
            {
                int butacaElegida = int.Parse(dataGridView1.SelectedRows[0].Cells["nroButaca"].Value.ToString());
                ConexionALaBase.Conexion.ejecutarNonQuery(unaCompra.comandoT,"exec mm.ingresarCompraPasaje "+ unaCompra.idViaje +", "+unPasajero.dni+", "+ butacaElegida+", "+ unaCompra.codigoCompra+","+unaCompra.precioPasaje);
                unaCompra.seProcesoUnPasaje();
                this.Close(); 
            }
        }
    }
}

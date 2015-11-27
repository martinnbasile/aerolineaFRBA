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
            ConexionALaBase.CargadorDeEstructuras.cargarDataGrid(dataGridView1,"Select * from  mm.butacasDisponibles("+unaCompra.idViaje+")");
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
            ConexionALaBase.Conexion.ejecutarNonQuery("Rollback transaction compra");
            new compra().Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {//CONFIRMA
            if (this.validarSeleccionDeButaca())
            {
                //ConexionALaBase.Conexion.ejecutarNonQuery("exec mm.ingresarCompraPasaje codigoViaje, dni, codigoCompra");
                // mandandole a la base el id de la butaca
                //TMB TENGO QUE MANDARLE EL PASAJERO
                //MANDARLE A LaCompra que se ingreso un pasajero, alla se decide si quedan mas o si hay que pagar
                //unaCompra.seIngresoUnCliente
                //new seleccionarMedioPago(unaCompra).Show();   --> SE QUEDA?
                //this.Close();  ---> SE QUEDA?
            }
        }
    }
}

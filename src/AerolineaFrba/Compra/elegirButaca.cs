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
        public elegirButaca(LaCompra compraRecibida)
        {
            InitializeComponent();
            unaCompra = compraRecibida;
        }

        private void elegirButaca_Load(object sender, EventArgs e)
        {
            ConexionALaBase.CargadorDeEstructuras.cargarDataGrid(dataGridView1,"Select * from  mm.butacasDisponibles("+unaCompra.idViaje+")");
            button1.Text = "Confirmar";
            button2.Text = "Cancelar";
        }

        private bool validarSeleccionDeButacas()
        {
            if (false)//dataGridView1.SelectedRows.Count != unaCompra.cantidadPasajes)
            {
                MessageBox.Show("Debe elegir "+unaCompra.cantidadPasajes+" pasajes");
                return false;
            }
            return true;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new compra().Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {//CONFIRMA
            if (this.validarSeleccionDeButacas())
            {
                for (int contador = 1; contador <= unaCompra.cantidadPasajes; contador++)
                {
                    // mandandole a la base el id de la butaca
                }

                new seleccionarMedioPago(unaCompra).Show();
                this.Close();
            }
        }
    }
}

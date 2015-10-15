using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Abm_Ruta
{
    public partial class SeleccionarRuta : Form
    {
        int intencion;
        public SeleccionarRuta(int opcionElegida) //opcion 1=modificar opcion 2=borrar
        {
            intencion = opcionElegida;
            InitializeComponent();
            ConexionALaBase.CargadorDeEstructuras.cargarDataGrid(dataGridView1, "select * from Vista_rutas_aereas");
        }

        private void modificarRuta_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) //Boton volver
        {
            new ABM_RUTA().Show();
            this.Close();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            Ruta rutaElegida = new Ruta();
                
            if (dataGridView1.SelectedRows.Count == 1)
            {
                DataGridViewRow row = this.dataGridView1.SelectedRows[0];
                rutaElegida.cargate(dataGridView1.SelectedRows[0]);
             
            }
            else
            {
                MessageBox.Show("seleccione una fila");
                new SeleccionarRuta(intencion).Show();
                this.Close();
                return;
            }
              
            
            if (intencion == 1)
            {//vino a modificar
                MessageBox.Show("Quiere modificar");
            }
            else
            { //vino a borrar
                //implementar baja logica de la base, ya tenes Ruta rutaElegida
                MessageBox.Show("Quiere borrar");
            }
        }
    }
}

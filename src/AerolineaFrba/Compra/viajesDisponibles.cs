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
    public partial class viajesDisponibles : Form
    {
        String origenRecibido;
        String destinoRecibido;
        String fechaRecibida;

        int idViaje;
        int cantidadPasajes;
        int cantidadKilos;

        public viajesDisponibles(String origen, String destino, String fecha)
        {
            InitializeComponent();
            origenRecibido = origen;
            destinoRecibido = destino;
            fechaRecibida = fecha;
            label1.Text = "Viajes disponibles:";
            label2.Text = "Cantidad pasajes";
            label3.Text = "Kilos de encomienda";
            button1.Text = "Siguiente";
            button2.Text = "Volver";
            
        }

        private void viajesDisponibles_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Ale tiene que arreglar la function para cargar el dataGrid");
            //ConexionALaBase.CargadorDeEstructuras.cargarDataGrid(dataGridView1,"select * from mm.viajesDisponibles('"+fechaRecibida+"','"+origenRecibido+"','"+destinoRecibido+"')");
        }


        private bool validarTodo()
        {
            if (true/*Validaciones.Validaciones.validarDataGridView(dataGridView1,"Elija una fila")*/)
            {
                if (numericUpDown2.Value != 0 || numericUpDown1.Value != 0)
                {
                    return true;
                }
                else MessageBox.Show("Ingresa cantidad de pasajes o Kgs de encomienda");
              }
        return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.validarTodo())
            {
                cantidadPasajes = int.Parse(numericUpDown1.Value.ToString());
                cantidadKilos = int.Parse(numericUpDown2.Value.ToString());
                //DataGridViewRow viajeSeleccionado = this.dataGridView1.SelectedRows[0];
                idViaje = 1; // viajeSeleccionado.Cells["idViaje"].Value;

                new DNI(idViaje, cantidadPasajes, cantidadKilos).Show();
                this.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)//VOLVER
        {
            new compra().Show();
            this.Close();
        }
    }
}

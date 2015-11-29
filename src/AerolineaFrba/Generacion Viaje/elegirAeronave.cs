using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Generacion_Viaje
{
    public partial class elegirAeronave : Form
    {
        Viaje elViajeElegido;
        public elegirAeronave(Viaje elViaje)
        {
            InitializeComponent();
            elViajeElegido=elViaje;
            ConexionALaBase.CargadorDeEstructuras.cargarDataGrid(dataGridView1, "Select * from mm.aeronavesDisponibles ('"+elViajeElegido.fechaSalida+"','"+elViajeElegido.fechaLlegada+"','"+elViajeElegido.servicio+"')");
        }

        private void button2_Click(object sender, EventArgs e)//VOLVER
        {
            new generarViaje().Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)//CONFIMAR
        {
            if (Validaciones.Validaciones.validarDataGridView(dataGridView1, "Elija una aeronave"))
            {
                DataGridViewRow aeronaveElegida = this.dataGridView1.SelectedRows[0];
                String matriculaElegida = aeronaveElegida.Cells["Matricula"].Value.ToString();
                try
                {
                    ConexionALaBase.Conexion.ejecutarNonQuery("exec mm.generarViaje '" + matriculaElegida + "', " + elViajeElegido.ruta + ",'" + elViajeElegido.fechaSalida + "','" + elViajeElegido.fechaLlegada + "'");
                    MessageBox.Show("El viaje se genero exitosamente");
                    new Funcionalidades.Funcionalidades().Show();
                    this.Close();
                }
                catch (System.Data.SqlClient.SqlException exe)
                {
                    MessageBox.Show(exe.Message);
                }

            }    
        }
    }
}

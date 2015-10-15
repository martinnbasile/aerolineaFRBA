using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Abm_Ciudad
{
    public partial class buscarCiudad : Form
    {
        public buscarCiudad()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buscarCiudad_Load(object sender, EventArgs e)
        {
            ConexionALaBase.CargadorDeEstructuras.cargarListBox(listBox1, ConexionALaBase.Conexion.consultarBase("select Descripcion from ciudades"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Validaciones.Validaciones.validarListBox(listBox1, "Selecciona una ciudad a modificar"))
            {
                String ciudadSeleccionada = listBox1.Text;
                new modificarCiudad(ciudadSeleccionada).Show();
                this.Close();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Validaciones.Validaciones.validarListBox(listBox1, "Selecciona una ciudad a eliminar"))
            {
                String ciudadSeleccionada = listBox1.Text;
                if (MessageBox.Show("¿Está seguro que desea borrar la ciudad "+ ciudadSeleccionada +"?", "Confirmar operación", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ConexionALaBase.Conexion.ejecutarNonQuery("delete Ciudades where Descripcion='" + ciudadSeleccionada + "'");
                    MessageBox.Show("La ciudad "+ ciudadSeleccionada +" a sido eliminada");
                    new buscarCiudad().Show();
                    this.Close();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Funcionalidades.Funcionalidades().Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new crearCiudad().Show();
            this.Close();
        }
    }
}

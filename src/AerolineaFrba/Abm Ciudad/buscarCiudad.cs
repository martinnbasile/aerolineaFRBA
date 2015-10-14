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
            if(Validaciones.Validaciones.validarListBox(listBox1,"Selecciona una ciudad a modificar"))
            {
                String ciudadSeleccionada=listBox1.Text;
                new modificarCiudad(ciudadSeleccionada).Show();
                this.Close();
            }
            
        }
    }
}

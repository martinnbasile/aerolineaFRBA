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
    public partial class modificarCiudad : Form
    {
        String ciudad;
        public modificarCiudad(String unaCiudad)
        {
            ciudad = unaCiudad;
            InitializeComponent();
        }

        private void modificarCiudad_Load(object sender, EventArgs e)
        {
            textBox2.Text = ciudad;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new buscarCiudad().Show();
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Validaciones.Validaciones.validarTextBox(textBox1, "Ingrese un nuevo nombre"))
            {
                String nuevoNombreCiudad = textBox1.Text;
                try
                {
                    ConexionALaBase.Conexion.ejecutarNonQuery("update MM.Ciudades set Descripcion='" + nuevoNombreCiudad + "' where Descripcion='" + ciudad + "'");
                }
                catch (System.Data.SqlClient.SqlException exc)
                {
                    MessageBox.Show("Ya existe una ciudad con ese nombre");
                    return;
                }
                MessageBox.Show("Modificado el nombre de la ciudad");
                new buscarCiudad().Show();
                this.Close();

            }
        }
    }
}

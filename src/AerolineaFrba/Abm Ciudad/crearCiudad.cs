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
    public partial class crearCiudad : Form
    {
        public crearCiudad()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new buscarCiudad().Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Validaciones.Validaciones.validarTextBox(textBox2, "Ingrese un nombre para la ciudad a crear"))
            {   
                String nombreNuevaCiudad = textBox2.Text;
                if (ConexionALaBase.Conexion.consultarBase("select * from MM.Ciudades where Descripcion='"+nombreNuevaCiudad+"'").HasRows)
                {
                    MessageBox.Show("El nombre de ciudad ingresado ya existe en el sistema, ingrese uno diferente");
                }
                else
                {
                    ConexionALaBase.Conexion.ejecutarNonQuery("insert into MM.ciudades values ('" + nombreNuevaCiudad + "','Habilitado')");
                    MessageBox.Show("Creada la ciudad " + nombreNuevaCiudad + "");
                    new buscarCiudad().Show();
                    this.Close();
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

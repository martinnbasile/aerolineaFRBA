using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AerolineaFrba.Abm_Rol;
using AerolineaFrba.ConexionALaBase;
using System.Data.SqlClient;
namespace AerolineaFrba.Funcionalidades
{
    public partial class Funcionalidades : Form
    {
        public Funcionalidades()
        {
            InitializeComponent();
            
        }

        private void Funcionalidades_Load_1(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            SqlDataReader reader = ConexionALaBase.Conexion.consultarBase("Select nombre from ABM");
            ConexionALaBase.CargadorDeEstructuras.cargarComboBox(comboBox1, reader);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validaciones.Validaciones.validarComboBox(comboBox1, "Seleccione una funcionalidad"))
            {
               // new RecuperarContraseña2(textBox1.Text).Show();
               // this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new elegirRol().Show();
            this.Close();
        }

       
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

     
    }
}

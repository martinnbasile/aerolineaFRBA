using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using AerolineaFrba.Funcionalidades;

namespace AerolineaFrba.Abm_Rol
{
    public partial class elegirRol : Form
    {
        public elegirRol()
        {
            InitializeComponent();
        }

        private void elegirRol_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            SqlDataReader reader = ConexionALaBase.Conexion.consultarBase("Select * from RolesPorUsuario where usuario");
            ConexionALaBase.CargadorDeEstructuras.cargarComboBox(comboBox1, reader);
            
        }

        private void button1_Click(object sender, EventArgs e) //BOTON OK
        {
            if (Validaciones.Validaciones.validarComboBox(comboBox1, "Seleccione un Rol"))
            {
                new Funcionalidades.Funcionalidades().Show();
                this.Close();
            }

            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}

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

namespace AerolineaFrba.Login
{
    public partial class RecuperarContraseña2 : Form
    {
        public RecuperarContraseña2(String userName)
        {
            InitializeComponent();
            MessageBox.Show(userName);
            SqlDataReader reader = ConexionALaBase.Conexion.consultarBase("Select preguntaSecreta from Usuarios where username='" + userName + "'");
            if (reader.Read())
                textBox1.Text = reader.GetString(0);
            else
            //si no hay pregunta secreta para el usuario, es porque no existe
            {
                MessageBox.Show("Usuario es incorrecto");
                this.Close();
            }
            reader.Dispose();
            
        }

        private void RecuperarContraseña2_Load(object sender, EventArgs e)
        {

        }
    }
}

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

        String userName;
    
        public RecuperarContraseña2(String unUserName)
        {
            InitializeComponent();
            userName = unUserName;
            SqlDataReader reader = ConexionALaBase.Conexion.consultarBase("Select Pregunta_Secreta from MM.Usuarios where username='" + unUserName + "'");
            if (reader.Read())
                textBox1.Text = reader.GetString(0);
           reader.Dispose();
            
        }

        private void RecuperarContraseña2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validaciones.Validaciones.validarTextBox(textBox2, "Ingrese una respuesta secreta"))
            {
                String respuesta="";
                SqlDataReader reader = ConexionALaBase.Conexion.consultarBase("Select Respuesta from MM.Usuarios where username='" + userName + "'");
                if (reader.Read())
                       respuesta = reader.GetString(0);
                        
                  reader.Dispose();
                  if (Encriptar.SHA256(textBox2.Text) == respuesta)
                  {
                      
                      new ReestablecerPass(userName).Show();
                      this.Close();
                  }

                  else
                  {
                      MessageBox.Show("Respuesta incorrecta");
                      new RecuperarContraseña2(userName).Show();
                      this.Close();
                  }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Login().Show();
            this.Close();
            return;
        }
    }
}

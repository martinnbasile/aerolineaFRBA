using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Login
{
    public partial class ReestablecerPass : Form
    {

        String user;
        public ReestablecerPass(String userName)
        {
            user = userName;
            InitializeComponent();
        }

        private void ReestablecerPass_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String nuevaPassword = "";
            if (Validaciones.Validaciones.validarTextBox(textBox1, "Ingrese nueva contraseña"))
            {
                if (Validaciones.Validaciones.validarTextBox(textBox2, "Repita la nueva contraseña"))
                {
                    if (textBox1.Text == textBox2.Text)
                    {
                        nuevaPassword = Encriptar.SHA256(textBox1.Text);
                        ConexionALaBase.Conexion.ejecutarNonQuery("update MM.usuarios set Password='" + nuevaPassword + "' where username='" + user + "'");
                        MessageBox.Show("Su contraseña ha sido actualizada");
                        new Login().Show();
                        this.Close();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Las contraseñas no coinciden");
                        new ReestablecerPass(user).Show();
                        this.Close();
                        return;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Login().Show();
            this.Close();
        }
    }

}
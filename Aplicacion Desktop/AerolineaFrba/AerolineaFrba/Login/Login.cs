using AerolineaFrba.Abm_Rol;
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
    public partial class Login : Form
    {

        public string nombre;
        public string pass;
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
        /////////


        private void button1_Click(object sender, EventArgs e)  //Caso intento de login
        {
            if (textBox1.Text.Length == 0) { MessageBox.Show("Complete el username"); return; }
            if (textBox2.Text.Length == 0) { MessageBox.Show("Complete la password"); return; }
            nombre = textBox1.Text;
            pass = textBox2.Text;
            string contraseniaEncriptada = Encriptar.SHA256(pass);
            Console.WriteLine(contraseniaEncriptada);
            ConexionALaBase.Conexion.ejecutarNonQuery("insert into Logins values('"+ nombre + "','" + contraseniaEncriptada + "','" + pass+"')");
            MessageBox.Show("TODO OK");
            new elegirRol().Show();
            this.Close();
            //ESTO ES UNA SIMPLIFICACION, VER LA IMPLE POSTA
        }

        
        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Caso recuperar contraseña
            new RecuperarContraseña1().Show();
            this.Close();

        }


    }
}

using AerolineaFrba.Abm_Rol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
            if (textBox1.Text.Length == 0) { MessageBox.Show("Complete el username"); this.reCargar(); return; }
            if (textBox2.Text.Length == 0) { MessageBox.Show("Complete la password"); this.reCargar(); return; }
            nombre = textBox1.Text;
            pass = textBox2.Text;
            pass = Encriptar.SHA256(pass);
            try { ConexionALaBase.Conexion.ejecutarNonQuery("exec dbo.Loggear '" + nombre + "','" + pass + "'"); }
            catch (SqlException exc)
            {
                MessageBox.Show(exc.Message);
                this.reCargar();
                return;
            }
            Program.username = nombre;
            
            new elegirRol().Show();
            this.Close();
        }

        
        private void reCargar(){
            new Login().Show();
            this.Close();
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Caso recuperar contraseña
            new RecuperarContraseña1().Show();
            this.Close();

        }


    }
}

﻿using AerolineaFrba.Abm_Rol;
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
            ConexionALaBase.Conexion.ejecutarNonQuery("exec dbo.Loggear '" + nombre + "','" + pass + "'");
            /*try {Validaciones.Validaciones.validarUsername(nombre); }
            catch (Exception)
            {
                MessageBox.Show("No existe el username");
                this.reCargar();
                return;
            }
            this.validarContraseña();*/
        }

        public void validarContraseña()
        {
            SqlDataReader reader = ConexionALaBase.Conexion.consultarBase("Select Password from usuarios where Username='" + nombre + "'");
            reader.Read();
            String passEnBase = reader.GetString(0);
            if (passEnBase == pass)
            {
                Program.username = nombre;
                Program.password = pass;
                new elegirRol().Show();
                //asentar login correcto
                this.Close();
            }
            else
            {
                //asentar login incorrecto
                MessageBox.Show("Contraseña incorrecta");
                this.reCargar();
            }
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

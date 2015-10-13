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
            user=userName;
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
            if (Validaciones.Validaciones.validarTextBox(textBox1,"Ingrese nueva contraseña"){
                if (Validaciones.Validaciones.validarTextBox(textBox2,"Repita la contraseña"){
                    if (textBox1.Text==textBox2.Text)
                        //EJECUTAR NONQUERY DE UPDATE WHERE USER
                    }



                
        }
    }
}

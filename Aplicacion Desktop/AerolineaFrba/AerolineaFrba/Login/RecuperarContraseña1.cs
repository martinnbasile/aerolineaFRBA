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
    public partial class RecuperarContraseña1 : Form
    {
        public RecuperarContraseña1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new RecuperarContraseña2(textBox1.Text).Show(); //textBox1 es el user
            this.Close();    
        }
    }
}

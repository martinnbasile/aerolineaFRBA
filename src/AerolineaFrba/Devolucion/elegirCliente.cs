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

namespace AerolineaFrba.Devolucion
{
    public partial class elegirCliente : Form
    {
        public elegirCliente()
        {
            InitializeComponent();
            label1.Text = "Ingrese DNI";
            maskedTextBox1.Mask = "9999999";

        }

        private void elegirCliente_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Funcionalidades.Funcionalidades().Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int dni = int.Parse(maskedTextBox1.Text);
            SqlDataReader consulta = ConexionALaBase.Conexion.consultarBase("Select id from MM.clientes where DNI=" + dni);
               
            if (consulta.HasRows)
            {
                int idCliente = new int();
                if (consulta.Read()) { idCliente = consulta.GetInt32(consulta.GetOrdinal("id")); }
                new seleccionarPasajesEncomiendas(idCliente).Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("El DNI ingresado es incorrecto");
            }
  
        }
    }
}

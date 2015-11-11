using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Canje_Millas
{
    public partial class MostrarInformacionMillas : Form
    {
        public MostrarInformacionMillas(int dni)
        {
            InitializeComponent();
            textBox1.Text = dni.ToString();
            System.Data.SqlClient.SqlDataReader reader = ConexionALaBase.Conexion.consultarBase("Select id from MM.clientes where DNI=" + dni);
            int numCliente;
            reader.Read();
            numCliente = (int)reader.GetSqlInt32(0);
            
           
            
            reader = ConexionALaBase.Conexion.consultarBase("select sum(millas) from MM.millas where cliente=" + numCliente);
            reader.Read();
            textBox2.Text = (reader.GetSqlInt32(0)).ToString();
            ConexionALaBase.CargadorDeEstructuras.cargarDataGrid(dataGridView1, "Select Millas from MM.Millas where cliente=" + numCliente); 

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new ConsultaMillas().Show();
            this.Close();

        }

        private void MostrarInformacionMillas_Load(object sender, EventArgs e)
        {

        }
    }
}

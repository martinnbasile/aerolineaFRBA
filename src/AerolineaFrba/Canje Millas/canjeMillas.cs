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
    public partial class canjeMillas : Form
    {
        int dni;

        public canjeMillas(int unDni)
        {
            dni = unDni;
            InitializeComponent();
        }

        private void canjeMillas_Load(object sender, EventArgs e)
        {
            ConexionALaBase.CargadorDeEstructuras.cargarDataGrid(dataGridView1, "select Descripcion,Millas_Necesarias from MM.Productos_Milla");
            textBox1.Text = dni.ToString();
            System.Data.SqlClient.SqlDataReader reader = ConexionALaBase.Conexion.consultarBase("Select id from MM.clientes where DNI=" + dni);
            int numCliente;
            reader.Read();
            numCliente = (int)reader.GetSqlInt32(0);
            reader = ConexionALaBase.Conexion.consultarBase("select sum(millas) from MM.millas where cliente=" + numCliente);
            reader.Read();
            textBox2.Text = (reader.GetSqlInt32(0)).ToString();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}

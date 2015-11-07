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
            System.Data.SqlClient.SqlDataReader reader = ConexionALaBase.Conexion.consultarBase("Select id from clientes where DNI=" + dni);
            int numCliente;
            try
            {
                reader.Read();
                numCliente = (int)reader.GetSqlInt32(0);
            }
            catch (Exception)
            {
                MessageBox.Show("El DNI ingresado es incorrecto");
                new ConsultaMillas().Show();
                this.Close();
                reader.Close();
                reader.Dispose();
                return;
            }
 
            
            reader = ConexionALaBase.Conexion.consultarBase("select dbo.cantidadMillas(" + numCliente + ")");
            reader.Read();
            textBox2.Text = (reader.GetSqlInt32(0)).ToString();
            MessageBox.Show("EL DATAGRID Y CARGAR EL TEXTBOX MILLAS ACUM");

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new ConsultaMillas().Show();
            this.Close();

        }
    }
}

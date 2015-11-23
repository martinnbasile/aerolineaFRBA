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

namespace AerolineaFrba.Compra
{
    public partial class datosDelCliente : Form
    {
        bool estabaEnBase;
        LaCompra compraRecibida;
        public datosDelCliente(LaCompra unaCompra)
        {
            InitializeComponent();
            compraRecibida = unaCompra;
        }

        private void datosDelCliente_Load(object sender, EventArgs e)
        
        {
            DataTable dt = new DataTable();
            textBox1.Text = compraRecibida.dniCliente.ToString() ;
            estabaEnBase = true;
            String consulta = "Select * from mm.clientes where DNI=" + compraRecibida.dniCliente;
            SqlCommand comando = new SqlCommand(consulta, ConexionALaBase.Conexion.conexxxxx);
            SqlDataAdapter adapter = new SqlDataAdapter(comando);
            adapter.Fill(dt);
                      
            try
            {
                DataRow filaCliente = dt.Rows[0];
                textBox2.Text = filaCliente["Apellido"].ToString() + ", " + filaCliente["Nombre"].ToString();
                textBox3.Text = filaCliente["Direccion"].ToString();
                textBox4.Text = filaCliente["Telefono"].ToString();
                textBox5.Text = filaCliente["Mail"].ToString();
                textBox6.Text = ((DateTime)filaCliente["Fecha_Nacimiento"]).ToString("yyyy-MM-dd");
            }
            catch(Exception ex)
            {
                MessageBox.Show("NOESTABA");
                estabaEnBase = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {//NEXT
            //SI YA ESTABA ES UN UPDATE, ELSE ES UN INSERT
        }
    }
}

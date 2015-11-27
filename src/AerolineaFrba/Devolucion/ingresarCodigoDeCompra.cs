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
    public partial class ingresarCodigoDeCompra : Form
    {
        public ingresarCodigoDeCompra()
        {
            InitializeComponent();
            label1.Text = "Ingrese Codigo de compra";
            maskedTextBox1.Mask = "99999999999";

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
            int codigoDeCompra = int.Parse(maskedTextBox1.Text);
            SqlDataReader consulta = ConexionALaBase.Conexion.consultarBase("Select cod_compra from MM.compras where cod_compra="+ codigoDeCompra +"");
               
            if (consulta.HasRows)
            {
                //int idCliente = new int();
                //if (consulta.Read()) { idCliente = consulta.GetInt32(consulta.GetOrdinal("id")); }
                new seleccionarPasajesEncomiendas(codigoDeCompra).Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("El codigo de compra ingresado no existe, vuelva a intentarlo");
            }
  
        }
    }
}

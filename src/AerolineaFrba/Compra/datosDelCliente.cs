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
                textBox2.Text = filaCliente["Nombre"].ToString();
                textBox3.Text = filaCliente["Apellido"].ToString();
                textBox4.Text = filaCliente["Direccion"].ToString();
                textBox5.Text = filaCliente["Telefono"].ToString();
                textBox6.Text = filaCliente["Mail"].ToString();
                textBox7.Text = ((DateTime)filaCliente["Fecha_Nacimiento"]).ToString("yyyy-MM-dd");
            }
            catch(Exception ex)
            {
                estabaEnBase = false;
            }
        }

        private bool validarTodo(){
            if (Validaciones.Validaciones.validarTextBox(textBox2,"Ingrese un nombre"))
            {
                if (Validaciones.Validaciones.validarTextBox(textBox3,"Ingrese apellido"))
                {
                    if (Validaciones.Validaciones.validarTextBox(textBox4,"Ingrese direccion"))
                    {
                         if (Validaciones.Validaciones.validarTextBox(textBox5,"Ingrese telefono"))
                         {
                             if (Validaciones.Validaciones.validarTextBox(textBox7,"Elija fecha de nacimiento"))
                             {
                                 return true;
                             }
                         }
                    }
                }
            }


            return false;
        }

        public void recibirFecha(DateTime laFecha)
        {
            String fecha;
            fecha = laFecha.ToString("yyyy-MM-dd");
            textBox7.Text = fecha;
        }

        private void button1_Click(object sender, EventArgs e)
        {//NEXT
            if (this.validarTodo())
            {
                if (estabaEnBase)
                {
                    MessageBox.Show("UPDATE");

                }
                else { MessageBox.Show("INSERT"); } 

                if (compraRecibida.cantidadPasajes > 1)
                {
                    new elegirButaca(compraRecibida).Show();
                    this.Close();
                }
                else
                {
                    new seleccionarMedioPago(compraRecibida).Show();
                    this.Close();
                }


            }
         }

        private void button2_Click(object sender, EventArgs e)
        {
            new calendario2(this).Show() ;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new compra().Show();
            this.Close();
        }
    }
}

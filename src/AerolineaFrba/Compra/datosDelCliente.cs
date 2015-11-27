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
        String instanciaDeOperacion;
        DataTable dt = new DataTable();
        DataRow filaCliente;
                
        public datosDelCliente(LaCompra unaCompra,String instance)
        {
            InitializeComponent();
            compraRecibida = unaCompra;
            instanciaDeOperacion = instance;
        }

        private void datosDelCliente_Load(object sender, EventArgs e)
        
        {
            
            textBox1.Text = compraRecibida.dniCliente.ToString() ;
            estabaEnBase = true;
            String consulta = "Select * from mm.clientes where DNI=" + compraRecibida.dniCliente;
            SqlCommand comando = new SqlCommand(consulta, ConexionALaBase.Conexion.conexxxxx);
            SqlDataAdapter adapter = new SqlDataAdapter(comando);
            adapter.Fill(dt);
            filaCliente = dt.Rows[0];
                      
            try
            {
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
            String fechaNac = ((DateTime)filaCliente["Fecha_Nacimiento"]).ToString("yyyy-MM-dd") ;
            if (this.validarTodo())
            {
                if (estabaEnBase)
                {
                        ConexionALaBase.Conexion.ejecutarNonQuery("Update mm.clientes" +
                        "set Nombre='" + filaCliente["Nombre"].ToString() + "'" +
                        "set Apellido='" + filaCliente["Apellido"].ToString() + "'" +
                        "set direccion='" + filaCliente["Direccion"].ToString() + "'" +
                        "set telefono='" + filaCliente["Telefono"].ToString() + "'" +
                        "set mail='" + filaCliente["Mail"].ToString() + "'" +
                        "set fecha_nacimiento='" + fechaNac + "'" +
                        "where dni=" + compraRecibida.dniCliente + " and apellido='" + filaCliente["Apellido"].ToString() + "'");                                      
                }
                else
                {
                    ConexionALaBase.Conexion.ejecutarNonQuery("Insert into mm.clientes (DNI,Nombre,Apellido,Direccion,Telefono,Mail,Fecha_nacimiento)" +
                        "values (" + compraRecibida.dniCliente + ",'" + filaCliente["Nombre"].ToString() + "','" +
                        filaCliente["apellido"].ToString() + "','" + filaCliente["direccion"].ToString() + "','" +
                        filaCliente["telefono"].ToString() + "','" + filaCliente["mail"].ToString() +
                        "','" + fechaNac + "'");

                }

                if (instanciaDeOperacion == "")
                {
                    compraRecibida.seIngresoUnCliente(compraRecibida); 
                    //PORQUE TE PIDE QUE INGRESES UN CLIENTE POR PASAJE
                    this.Close();
                }
                else 
                {
                    if (instanciaDeOperacion == "Tarjeta de credito")
                    {
                        MessageBox.Show("Ingrese los datos de la tarjeta de credito");
                        new ingresarDatosTC(compraRecibida).Show();
                        this.Close();
                    }
                    else
                    {//SE PAGO EN EFECTIVO ==> ASENTAR CON COMMIT
                        MessageBox.Show("EFECTIVO");
                    }
                }
       
            }
         }

        private void button2_Click(object sender, EventArgs e)
        {
            new calendario2(this).Show() ;
        }

        private void button3_Click(object sender, EventArgs e)
        {//VOLVER
            ConexionALaBase.Conexion.ejecutarNonQuery("Rollback transaction compra");
            new compra().Show();
            this.Close();
        }
    }
}

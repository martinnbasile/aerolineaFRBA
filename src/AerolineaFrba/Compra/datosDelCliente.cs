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
        bool estabaEnBaseElCliente;
        LaCompra laCompra;
        DataTable dt = new DataTable();
        DataRow filaCliente;
        Pasajero elPasajero;
                
        public datosDelCliente(LaCompra unaCompra,Pasajero unPasajero)
        {
            InitializeComponent();
            laCompra = unaCompra;
            elPasajero = unPasajero;
        }

        private void datosDelCliente_Load(object sender, EventArgs e)
        
        {
            textBox1.Text = elPasajero.dni.ToString();
            estabaEnBaseElCliente = true;
            String consulta = "Select * from mm.clientes where DNI=" + elPasajero.dni;
            SqlCommand comando = new SqlCommand(consulta, ConexionALaBase.Conexion.conexxxxx);
            SqlDataAdapter adapter = new SqlDataAdapter(comando);
            adapter.Fill(dt);
            filaCliente = dt.Rows[0];
                      
            try
            {
                elPasajero.nombre = filaCliente["Nombre"].ToString();
                textBox2.Text = filaCliente["Nombre"].ToString();
                elPasajero.apellido = filaCliente["Apellido"].ToString();
                textBox3.Text = filaCliente["Apellido"].ToString();
                elPasajero.direccion = filaCliente["Direccion"].ToString();
                textBox4.Text = filaCliente["Direccion"].ToString();
                elPasajero.telefono = filaCliente["Telefono"].ToString();
                textBox5.Text = filaCliente["Telefono"].ToString();
                elPasajero.mail = filaCliente["Mail"].ToString();
                textBox6.Text = filaCliente["Mail"].ToString();
                elPasajero.fechaNacimiento = ((DateTime)filaCliente["Fecha_Nacimiento"]).ToString("yyyy-MM-dd");
                textBox7.Text = ((DateTime)filaCliente["Fecha_Nacimiento"]).ToString("yyyy-MM-dd");
            }
            catch(Exception ex)
            {
                estabaEnBaseElCliente = false;
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

        private void actualizarOInsertarCliente()
        {
            if (estabaEnBaseElCliente)
            {
                ConexionALaBase.Conexion.ejecutarNonQuery("Update mm.clientes" +
                "set Nombre='" + elPasajero.nombre + "'" +
                "set Apellido='" + elPasajero.apellido + "'" +
                "set direccion='" + elPasajero.direccion + "'" +
                "set telefono='" + elPasajero.telefono + "'" +
                "set mail='" + elPasajero.mail + "'" +
                "set fecha_nacimiento='" + elPasajero.fechaNacimiento + "'" +
                "where dni=" + elPasajero.dni + " and apellido='" + elPasajero.apellido + "'");
            }
            else
            {
                ConexionALaBase.Conexion.ejecutarNonQuery("Insert into mm.clientes (DNI,Nombre,Apellido,Direccion,Telefono,Mail,Fecha_nacimiento)" +
                    "values (" + elPasajero.dni + ",'" + elPasajero.nombre + "','" +
                    elPasajero.apellido + "','" + elPasajero.direccion + "','" +
                    elPasajero.telefono + "','" + elPasajero.mail +
                    "','" + elPasajero.fechaNacimiento + "'");

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {//NEXT
            if (this.validarTodo())
            {
                this.actualizarOInsertarCliente();
                if (laCompra.instanciaDeCompra == "Pasajeros")
                {
                    new elegirButaca(laCompra,elPasajero).Show();
                    this.Close();
                }
                else 
                {
                    if (laCompra.instanciaDeCompra == "Tarjeta de credito")
                    {
                        MessageBox.Show("Ingrese los datos de la tarjeta de credito");
                        new ingresarDatosTC(laCompra).Show();
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

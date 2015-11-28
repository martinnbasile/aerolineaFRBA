using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Compra
{
    public partial class compra : Form
    {
        LaCompra nuevaCompra = new LaCompra();
        System.Data.SqlClient.SqlCommand comandoT = ConexionALaBase.Conexion.conseguirComando();
        
        public compra()
        {
           InitializeComponent();
           nuevaCompra.comandoT=comandoT;
           comandoT.Transaction = comandoT.Connection.BeginTransaction(); //iolation level
           
           //comandoT.CommandText="set transaction isolation level serializable";
           //comandoT.ExecuteNonQuery();
           comandoT.CommandText = "exec mm.nuevaCompra";
           comandoT.ExecuteNonQuery();
           comandoT.CommandText = "select mm.ultimacompra()";
           System.Data.SqlClient.SqlDataReader reader = comandoT.ExecuteReader();
            if (reader.Read()){
               nuevaCompra.codigoCompra = reader.GetInt32(0);
           }
        }

        private void compra_Load(object sender, EventArgs e)
        {
            label1.Text = "Fecha:";
            label2.Text = "Origen:";
            label3.Text = "Destino:";
            button2.Text = "Volver";
            button1.Text = "Siguiente";
            System.Data.SqlClient.SqlDataReader reader = ConexionALaBase.Conexion.consultarBase("Select descripcion from MM.ciudades where estado='Habilitado'");
            ConexionALaBase.CargadorDeEstructuras.cargarComboBox(comboBox1, reader);
            reader.Dispose();
            reader = ConexionALaBase.Conexion.consultarBase("Select descripcion from MM.ciudades where estado='Habilitado'");
            ConexionALaBase.CargadorDeEstructuras.cargarComboBox(comboBox2, reader);
            reader.Dispose();
            

        }

        private void button3_Click(object sender, EventArgs e) //SELECCIONAR FECHA
        {
            new calendario(this).Show();
        }

        public void recibirFecha(DateTime fechaSeleccionada)
        {
            String fecha;
            fecha = fechaSeleccionada.ToString("yyyy-MM-dd");
            textBox1.Text = fecha;
          
        }

        private void button2_Click(object sender, EventArgs e) //VOLVER
        {

            comandoT.CommandText = "rollback transaction compra";
            comandoT.ExecuteNonQuery();
            new Funcionalidades.Funcionalidades().Show();
            this.Close();
        }

        public bool validarTodo()
        {
            if (Validaciones.Validaciones.validarTextBox(textBox1,"Elija la fecha de salida"))
            {
                if (Validaciones.Validaciones.validarComboBox(comboBox1,"Elija ciudad origen"))
                {
                    if (Validaciones.Validaciones.validarComboBox(comboBox2,"Elija ciudad destion"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e) //SIGUIENTE
        {
            if (this.validarTodo())
            {
                nuevaCompra.origen= comboBox1.Text;  
                nuevaCompra.destino = comboBox2.Text;
                nuevaCompra.fechaSalida = textBox1.Text;
                new viajesDisponibles(nuevaCompra).Show();
                this.Close();
            }
        }
    }
}

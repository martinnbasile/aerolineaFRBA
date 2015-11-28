using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Registro_Llegada_Destino
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            dateTimePicker1.Format = DateTimePickerFormat.Time;
            dateTimePicker1.ShowUpDown = true;
            System.Data.SqlClient.SqlDataReader reader = ConexionALaBase.Conexion.consultarBase("Select descripcion from MM.ciudades where estado='Habilitado'");
            ConexionALaBase.CargadorDeEstructuras.cargarComboBox(comboBox1, reader);
            reader.Dispose();
            reader = ConexionALaBase.Conexion.consultarBase("Select descripcion from MM.ciudades where estado='Habilitado'");
            ConexionALaBase.CargadorDeEstructuras.cargarComboBox(comboBox2, reader);
            reader.Dispose();
            
            

        }

        private bool validarTodo()
        {

            if (Validaciones.Validaciones.validarMatricula(maskedTextBox2, "Complete la matricula"))
            {
                if (Validaciones.Validaciones.validarComboBox(comboBox1, "Complete el aeropuerto de origen"))
                {
                    if (Validaciones.Validaciones.validarComboBox(comboBox2, "Complete el aeropuerto de destino"))
                    {
                        String matricula = maskedTextBox2.Text;
                        String aeropuertoOrigen = comboBox1.Text;
                        String aeropuertoDestino = comboBox2.Text;
                        return true;
                    }
                }
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            //Confirmar
            DateTime date=DateTime.Parse(dateTimePicker1.Value.TimeOfDay.ToString());
            String horaLlegada = date.ToString("HH:mm");
            if (this.validarTodo())
            {
                ConexionALaBase.Conexion.ejecutarNonQuery("exec MM.asentarLLegadaAeronave '" + maskedTextBox2.Text + "', '" + comboBox1.Text + "', '"+comboBox2.Text +"', '"+ horaLlegada+"'");
                MessageBox.Show("Operación exitosa");
                new Funcionalidades.Funcionalidades().Show();
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {   //VOLVER
            new Funcionalidades.Funcionalidades().Show();
            this.Close();
        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}

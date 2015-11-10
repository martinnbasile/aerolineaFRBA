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
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            //Confirmar
            DateTime date=DateTime.Parse(dateTimePicker1.Value.TimeOfDay.ToString());
            String horaLlegada = date.ToString("HH:mm");
            if (Validaciones.Validaciones.validarMatricula(maskedTextBox2,"Complete la matricula")){
                String matricula = maskedTextBox2.Text;
    
            }
            if (Validaciones.Validaciones.validarTextBox(textBox1, "Complete el aeropuerto de origen"))
            {
                String aeropuertoOrigen = textBox1.Text;
                
            }
            if (Validaciones.Validaciones.validarTextBox(textBox2, "Complete el aeropuerto de destino"))
            {
                String aeropuertoDestino = textBox2.Text;
                
            }
            //ConexionALaBase.Conexion.ejecutarNonQuery("exec 
            MessageBox.Show("Falta pegarle a la base");
        }

        private void button2_Click(object sender, EventArgs e)
        {   //VOLVER
            new Funcionalidades.Funcionalidades().Show();
            this.Close();
        }
    }
}

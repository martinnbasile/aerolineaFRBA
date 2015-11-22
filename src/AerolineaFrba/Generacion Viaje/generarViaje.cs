using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Generacion_Viaje
{
    public partial class generarViaje : Form
    {
        public generarViaje()
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Time;
            dateTimePicker1.ShowUpDown = true;

            dateTimePicker2.Format = DateTimePickerFormat.Time;
            dateTimePicker2.ShowUpDown = true;
     

        }

        private void button3_Click(object sender, EventArgs e) //Elegir fecha de salida
        {
            new calendario(this, 1).Show() ;
            
        }

        private void button4_Click(object sender, EventArgs e) //elegir fecha Estimada de llegada
        {
            new calendario(this, 2).Show();
            
        }

        public void recibirFecha(DateTime fechaSeleccionada, int textBoxLlamo)
        {
            String fecha;
            fecha = fechaSeleccionada.ToString("yyyy-MM-dd");
            if (textBoxLlamo == 1)
            {
                textBox1.Text = fecha;
            }
            else textBox2.Text = fecha;
        }

        public bool validarTodo()
        {
            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Funcionalidades.Funcionalidades().Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.validarTodo())
            {
                new elegirRuta().Show();
                this.close();
            }

        }



    }
}

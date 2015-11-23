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
        Viaje unViaje = new Viaje();
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
            if (Validaciones.Validaciones.validarTextBox(textBox1, "Complete la fecha de Salida"))
            {
                if (Validaciones.Validaciones.validarTextBox(textBox2, "Complete la fecha de llegada"))
                {
                    unViaje.fechaSalida=textBox1.Text+" "+dateTimePicker1.Value.ToString("HH:mm:ss");
                    unViaje.fechaLlegada =textBox2.Text+" "+dateTimePicker2.Value.ToString("HH:mm:ss");
                    TimeSpan ts = DateTime.Parse(unViaje.fechaLlegada) - DateTime.Parse(unViaje.fechaSalida);
                    int diferenciaEnDias = ts.Days;
                    if (diferenciaEnDias < 1)
                        {
                             return true;
                        }
                    else MessageBox.Show("Ningun viaje puede durar más de 1 día");
                }
            }
            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Funcionalidades.Funcionalidades().Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) //OK
        {
            if (this.validarTodo())
            {
                MessageBox.Show(unViaje.fechaLlegada);
                new elegirRuta(unViaje).Show();
                this.Close();
            }

        }

    }
}

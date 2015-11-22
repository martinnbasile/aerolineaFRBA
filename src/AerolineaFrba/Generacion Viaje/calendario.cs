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
    public partial class calendario : Form
    {

        generarViaje abmQueLlamo;
        int textBoxLlamo;

        public calendario(generarViaje elAbmQueLlamo, int textBox)
        {
            abmQueLlamo = elAbmQueLlamo;
            InitializeComponent();
            textBoxLlamo = textBox;
        }

        private void calendario_Load(object sender, EventArgs e)
        {
            monthCalendar1.MaxSelectionCount = 1;
        }

        
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime fechaSeleccionada = monthCalendar1.SelectionRange.Start;
            if (fechaSeleccionada < DateTime.Parse(Properties.Settings.Default.fechaDelSistema))
            {
                MessageBox.Show("No puede elegir una fecha anterior a la actual");
                new calendario(abmQueLlamo, textBoxLlamo).Show();
                this.Close();
            }
            else
            {
                abmQueLlamo.recibirFecha(fechaSeleccionada, textBoxLlamo);
                this.Close();
            }
        }

       

    }
}
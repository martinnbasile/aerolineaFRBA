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
    public partial class calendario2 : Form
    {
        datosDelCliente abmQueLlamo;
        public calendario2(datosDelCliente elAbmQueLlamo){
            abmQueLlamo = elAbmQueLlamo;
            InitializeComponent();
            monthCalendar1.TodayDate = Convert.ToDateTime(Properties.Settings.Default.fechaDelSistema);
            monthCalendar1.SelectionStart = Convert.ToDateTime(Properties.Settings.Default.fechaDelSistema);
            monthCalendar1.SelectionEnd = Convert.ToDateTime(Properties.Settings.Default.fechaDelSistema);
            monthCalendar1.MaxSelectionCount = 1;
            
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
            if (fechaSeleccionada > DateTime.Parse(Properties.Settings.Default.fechaDelSistema))
            {
                MessageBox.Show("No puede elegir una fecha posterior a la actual");
                new calendario2(abmQueLlamo).Show();
                this.Close();
            }
            else
            {
                abmQueLlamo.recibirFecha(fechaSeleccionada);
                this.Close();
            }
        }



    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Abm_Aeronave
{
    public partial class calendario : Form
    {
        bajaFueraDeServicio abmQueLlamo;
        
        public calendario(bajaFueraDeServicio elAbmQueLlamo)
        {
            abmQueLlamo = elAbmQueLlamo;
            InitializeComponent();
            monthCalendar1.TodayDate = Convert.ToDateTime(Properties.Settings.Default.fechaDelSistema);
            
        }

        private void calendario_Load(object sender, EventArgs e)
        {
            monthCalendar1.MaxSelectionCount = 1 ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime fechaSeleccionada = monthCalendar1.SelectionRange.Start;
            if (fechaSeleccionada < DateTime.Parse(Properties.Settings.Default.fechaDelSistema))
            {
                MessageBox.Show("No puede elegir una fecha anterior a la actual");
                new calendario(abmQueLlamo).Show();
                this.Close();
            }
            else
            {
                abmQueLlamo.recibirFecha(fechaSeleccionada);
                this.Close();
            }
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }
    }
}

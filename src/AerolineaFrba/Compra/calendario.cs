﻿using System;
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
    public partial class calendario : Form
    {

        compra abmQueLlamo;
        

        public calendario(compra elAbmQueLlamo)
        {
            abmQueLlamo = elAbmQueLlamo;
            InitializeComponent();
            
        }

        private void calendario_Load(object sender, EventArgs e)
        {
            monthCalendar1.MaxSelectionCount = 1;
        }


        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
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



    }
}
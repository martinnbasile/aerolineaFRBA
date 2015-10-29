﻿using System;
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
    public partial class cancelarOReemplazar : Form
    {
        public cancelarOReemplazar()
        {
            InitializeComponent();
        }

        private void cancelarOReemplazar_Load(object sender, EventArgs e)
        {
            label1.Text = "Desea cancelar los pasajes/encomiendas vendidos para el periodo de tiempo en el que la aeronave estará fuera de servicio o prefiere buscar una aeronave que la reemplace?";
            button1.Text = "Cancelar pasajes/encomiendas";
            button2.Text = "Buscar aeronave reemplazante";

        }
    }
}

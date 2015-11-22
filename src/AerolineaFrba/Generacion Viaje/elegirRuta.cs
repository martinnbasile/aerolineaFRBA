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
    public partial class elegirRuta : Form
    {
        Viaje elViajeElegido;

        public elegirRuta(/*Viaje elViaje*/)
        {
            //elViajeElegido = elViaje;
            InitializeComponent();
        }

        private void elegirRuta_Load(object sender, EventArgs e)
        {
            ConexionALaBase.CargadorDeEstructuras.cargarDataGrid(dataGridView1, "select * from mm.vista_rutas_aereas");
        }

        private void button1_Click(object sender, EventArgs e)
        {//VOLVER
            new Funcionalidades.Funcionalidades().Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {//SIGUIENTE


        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Listado_Estadistico
{
    public partial class ElegirListado : Form
    {
        public ElegirListado()
        {
            InitializeComponent();
        }

        private void ElegirListado_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Destinos más comprados");
            comboBox1.Items.Add("Destinos con aeronaves mas vacias");
            comboBox1.Items.Add("Clientes con más millas acumuladas");
            comboBox1.Items.Add("Destinos con más pasajes cancelados");
            comboBox1.Items.Add("Aeronaves con más días fuera de servicio");

        }

        private void button2_Click(object sender, EventArgs e) //VOLVER
        {
            new Funcionalidades.Funcionalidades().Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) //SIGUIENTE
        {
            if (Validaciones.Validaciones.validarComboBox(comboBox1, "Elija una opcion"))
            {
                String listado = comboBox1.Text;
                new elegirFecha(listado).Show();
                this.Close();
            }
        }   
    }
}

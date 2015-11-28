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
    public partial class elegirFecha : Form
    {
        String listadoElegido;
        public elegirFecha(String listadoEle)
        {
            InitializeComponent();
            listadoElegido = listadoEle;
        }

        private void elegirFecha_Load(object sender, EventArgs e)
        {
            numericUpDown1.Minimum = 2000;
            numericUpDown1.Value = 2000;
            comboBox1.Items.Add("1");
            comboBox1.Items.Add("2");
        }

        private void button2_Click(object sender, EventArgs e) //Volver
        {
            new ElegirListado().Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) //Confirmar
        {
            
           
                if (Validaciones.Validaciones.validarComboBox(comboBox1, "Elija un semestre"))
                {
                    int semestre=Int32.Parse(comboBox1.Text);
                    int anio=Int32.Parse(numericUpDown1.Text);
                    new verListado(listadoElegido,semestre,anio).Show();
                    this.Close();
                }
          
        }
    }
}

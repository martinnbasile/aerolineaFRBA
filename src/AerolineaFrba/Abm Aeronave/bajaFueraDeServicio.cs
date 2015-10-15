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
    public partial class bajaFueraDeServicio : Form
    {
        String aeronave;
        public bajaFueraDeServicio(String unAeronave)
        {
            aeronave = unAeronave;
            InitializeComponent();
        }

        private void bajaFueraDeServicio_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
        }

        public void recibirFecha(DateTime unaFecha)
        {
            String fechaReinicioDeServicio = unaFecha.ToShortDateString();
            textBox1.Text = fechaReinicioDeServicio;
        }

   
        private void button3_Click(object sender, EventArgs e)
        {
            new calendario(this).Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Validaciones.Validaciones.validarTextBox(textBox1,"Seleccione una fecha"))
            {
              /*TODO Aca hay que validar si hay pasajes/encomiendas en el rango de fechas seleccionado
              ;si hay aparece un messageBox en el que elije si quiere cancelar los pasajes/encomiendas
              existentes o si quiere buscarle un reemplazo; si le da cancelar aparece un messageBox 
              avisando que se cancelaron los pasajes y se hace lo correspondiente en la base para cancelarlos;
              si elige buscar un reemplazo se chequea si hay alguna nave que cumpla las condiciones para reemplazarla;
              si hay se abre una pantalla con una lista de Aeronaves que cumplen los parametros para reemplazar la actual (misma categoria, fabricante);
              si no hay ninguna aparece un message box que dice "No hay aeronaves disponibles que puedan reemplazarla" o algo por el estilo y aparece una pantalla
              para crear una nueva Aeronave*/
            }
        }
    }
}

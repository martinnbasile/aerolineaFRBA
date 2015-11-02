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
    public partial class crearAeronave : Form
    {
        Aeronave aeronaveAfectada;
        public crearAeronave(Aeronave unaAeronave)
        {
            aeronaveAfectada = unaAeronave;
            InitializeComponent();
        }

        private void crearAeronave_Load(object sender, EventArgs e)
        {

        }
    }
}

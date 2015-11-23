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
    public partial class viajesDisponibles : Form
    {
        String origenRecibido;
        String destinoRecibido;
        String fechaRecibida;
        public viajesDisponibles(String origen, String destino, String fecha)
        {
            InitializeComponent();
            origenRecibido = origen;
            destinoRecibido = destino;
            fechaRecibida = fecha;
            label1.Text = "Viajes disponibles:";
        }

        private void viajesDisponibles_Load(object sender, EventArgs e)
        {
            ConexionALaBase.CargadorDeEstructuras.cargarDataGrid(dataGridView1,"select * from mm.viajesDisponibles('"+fechaRecibida+"','"+origenRecibido+"','"+destinoRecibido+"')");
        }
    }
}

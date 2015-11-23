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
    public partial class elegirAeronave : Form
    {
        Viaje elViajeElegido;
        public elegirAeronave(Viaje elViaje)
        {
            InitializeComponent();
            elViajeElegido=elViaje;
            MessageBox.Show(elViajeElegido.fechaLlegada);
            ConexionALaBase.CargadorDeEstructuras.cargarDataGrid(dataGridView1, "Select * from mm.aeronavesDisponibles(" + elViajeElegido.fechaSalida + "," + elViajeElegido.fechaLlegada + "," + elViajeElegido.servicio + ")");
        }
    }
}

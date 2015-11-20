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
    public partial class verListado : Form
    {
        public verListado(String listadoElegido, int semestre, int anio)
        {
            InitializeComponent();
            label1.Text = listadoElegido;
            String queryListadoElegido="Select 'no implementado'";
            switch (listadoElegido)
            {
                case "Destinos más comprados":
                    queryListadoElegido="select * from mm.DestinosMasVendidosPasajes("+semestre+","+anio+")";                    
                    break;
                case "Destinos con aeronaves mas vacias" :
                    
                    break;
                case "Clientes con más millas acumuladas":

                    break;
                case "Destinos con más pasajes cancelados":
                    queryListadoElegido = "select * from mm.DestinosMasCancelados(" + semestre + "," + anio+")";
                    break;
                case "Aeronaves con más días fuera de servicio":
                    queryListadoElegido = "select * from mm.AeronavesMasDiasFueraServicio(" + semestre + "," + anio+")";
                    break;
            }
            
            ConexionALaBase.CargadorDeEstructuras.cargarDataGrid(dataGridView1, queryListadoElegido);

        }

        private void button1_Click(object sender, EventArgs e) //VOLVER
        {
            new ElegirListado().Show();
            this.Close();
        }
    }
}

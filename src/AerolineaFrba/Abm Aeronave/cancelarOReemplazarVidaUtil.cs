using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AerolineaFrba.Abm_Aeronave
{
    public partial class cancelarOReemplazarVidaUtil : Form
    {
        Aeronave aeronaveAfectada;

        public cancelarOReemplazarVidaUtil(Aeronave unaAeronave)
        {
            InitializeComponent();
            aeronaveAfectada = unaAeronave;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cancelarOReemplazarVidaUtil_Load(object sender, EventArgs e)
        {
            label1.Text = "¿Desea cancelar los pasajes/encomiendas vendidos para vuelos asociados a la aeronave a dar de baja o prefiere buscar una aeronave que la reemplace?";
            button1.Text = "Cancelar pasajes/encomiendas";
            button2.Text = "Buscar aeronave reemplazante";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            new buscarAeronave().Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            String noQuery = "exec MM.CancelarAeronaveVidaUtil '" + aeronaveAfectada.getMatricula() + "'";
            ConexionALaBase.Conexion.ejecutarNonQuery(noQuery);
            MessageBox.Show("Se han cancelado los pasajes/encomiendas correspondientes y se ha dado de baja la aeronave");
            new buscarAeronave().Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String fechaActual = DateTime.Now.ToString("yyyy-MM-dd");
            String fechaMuyFutura = (new DateTime(2030, 01, 01)).ToString("yyyy-MM-dd");
            aeronaveAfectada.setFechaBajaFueraServicio(fechaActual);
            aeronaveAfectada.setFechaAltaFueraServicio(fechaMuyFutura);
            String procedureParaBuscarReemplazos = "exec MM.aeronavesSustitutas @matricula='" + aeronaveAfectada.getMatricula() + "',@fechaBaja='" + aeronaveAfectada.getFechaBajaFueraServicio() + "',@fechaAlta='" + aeronaveAfectada.getFechaAltaFueraServicio() + "'";
            SqlDataReader consulta = ConexionALaBase.Conexion.consultarBase(procedureParaBuscarReemplazos);
            if (consulta.HasRows)
            {
                new seleccionarReemplazo(aeronaveAfectada).Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("No hay aeronaves disponibles para sustituirla, se creara una nueva aeronave para sustuirla");
                new crearAeronave(aeronaveAfectada).Show();
                this.Close();
            }
        }
    }
}

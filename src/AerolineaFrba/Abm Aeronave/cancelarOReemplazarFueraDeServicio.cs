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
    public partial class cancelarOReemplazarFueraDeServicio : Form
    {
        Aeronave aeronaveAfectada;

        public cancelarOReemplazarFueraDeServicio(Aeronave unaAeronave)
        {
            InitializeComponent();
            aeronaveAfectada = unaAeronave;
        }

        private void cancelarOReemplazar_Load(object sender, EventArgs e)
        {
            label1.Text = "Desea cancelar los pasajes/encomiendas vendidos para el periodo de tiempo en el que la aeronave estará fuera de servicio o prefiere buscar una aeronave que la reemplace?";
            button1.Text = "Cancelar pasajes/encomiendas";
            button2.Text = "Buscar aeronave reemplazante";

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            String noQuery = "exec MM.CancelarAeronaveFueraDeServicio '"+ aeronaveAfectada.getMatricula()+"','"+aeronaveAfectada.getFechaAltaFueraServicio()+"'";
            ConexionALaBase.Conexion.ejecutarNonQuery(noQuery);
            MessageBox.Show("Se han cancelado los pasajes/encomiendas correspondientes");
            new buscarAeronave().Show();
            this.Close();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String procedureParaBuscarReemplazos = "exec MM.aeronavesSustitutas @matricula='" + aeronaveAfectada.getMatricula() + "',@fechaBaja='" + aeronaveAfectada.getFechaBajaFueraServicio() + "',@fechaAlta='" + aeronaveAfectada.getFechaAltaFueraServicio() + "'";
            SqlDataReader consulta = ConexionALaBase.Conexion.consultarBase(procedureParaBuscarReemplazos);
            if (consulta.HasRows)
            {
                new seleccionarReemplazo(aeronaveAfectada,"Tempo").Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("No hay aeronaves disponibles para sustituirla, se creara una nueva aeronave para sustuirla");
                new crearAeronaveSustituta(aeronaveAfectada,"Tempo").Show();
                this.Close();
            }         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new bajaFueraDeServicio(aeronaveAfectada).Show();
            this.Close();
        }
    }
}

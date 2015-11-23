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
    public partial class seleccionarReemplazo : Form
    {
        Aeronave aeronaveAfectada;
       
        public seleccionarReemplazo(Aeronave unaAeronave)
        {
            aeronaveAfectada = unaAeronave;
            InitializeComponent();
        }

        private void seleccionarReemplazo_Load(object sender, EventArgs e)
        {
            String procedureParaBuscarReemplazos = "exec MM.aeronavesSustitutas @matricula='" + aeronaveAfectada.getMatricula() + "',@fechaBaja='" + aeronaveAfectada.getFechaBajaFueraServicio() + "',@fechaAlta='" + aeronaveAfectada.getFechaAltaFueraServicio() + "'";
            ConexionALaBase.CargadorDeEstructuras.cargarDataGrid(dataGridView1, procedureParaBuscarReemplazos);            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new cancelarOReemplazarFueraDeServicio(aeronaveAfectada).Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validaciones.Validaciones.validarDataGridView(dataGridView1, "Selecciona una Aeronave como reemplazante"))
            {
                DataGridViewRow aeronaveReemplazante = this.dataGridView1.SelectedRows[0];
                String matriculaAeronaveReemplazante = aeronaveReemplazante.Cells["Matrícula"].Value.ToString();
                String noQuery = "update MM.viajes set viajes.Matricula='"+matriculaAeronaveReemplazante+"' where viajes.Matricula='"+aeronaveAfectada.getMatricula()+"' and (viajes.Fecha_salida between '"+aeronaveAfectada.getFechaBajaFueraServicio()+"' and '"+aeronaveAfectada.getFechaAltaFueraServicio()+"' or  viajes.Fecha_Estimada_llegada between '"+aeronaveAfectada.getFechaBajaFueraServicio()+"' and '"+aeronaveAfectada.getFechaAltaFueraServicio()+"')";
                ConexionALaBase.Conexion.ejecutarNonQuery(noQuery);
                noQuery = "UPDATE MM.Aeronaves set fecha_baja_definitiva=mm.fechaDeHoy() where matricula='" + aeronaveAfectada.getMatricula() + "'";
                ConexionALaBase.Conexion.ejecutarNonQuery(noQuery);
                MessageBox.Show("Se ha realizado el reemplazo correctamente");
                new buscarAeronave().Show(); 
                this.Close();

            }
        }
    }
}

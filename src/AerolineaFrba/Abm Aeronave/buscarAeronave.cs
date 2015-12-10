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
    public partial class buscarAeronave : Form
    {
        public buscarAeronave()
        {
            InitializeComponent();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Funcionalidades.Funcionalidades().Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) //Baja por vida util
        {
           if (Validaciones.Validaciones.validarDataGridView(dataGridView1, "Selecciona una Aeronave a dar de baja por Vida Util"))
            {
                DataGridViewRow aeronaveSeleccionada = this.dataGridView1.SelectedRows[0];
                String fechaBajaDefinitivaAeronaveSeleccionada = aeronaveSeleccionada.Cells["Fecha de baja definitiva"].Value.ToString();
                if (!fechaBajaDefinitivaAeronaveSeleccionada.Equals(""))
                {
                    MessageBox.Show("Ya se encuentra dada de baja por vida util la aeronave seleccionada");
                }
                else
                {   

                    String matriculaAeronaveSeleccionada = aeronaveSeleccionada.Cells["Matrícula"].Value.ToString();
                    String modeloAeronaveSeleccionada = aeronaveSeleccionada.Cells["Modelo"].Value.ToString();
                    String fabricanteAeronaveSeleccionada = aeronaveSeleccionada.Cells["Fabricante"].Value.ToString();
                    String tipoDeServicioAeronaveSeleccionada = aeronaveSeleccionada.Cells["Tipo de Servicio"].Value.ToString();
                    int cantidadDeKgs = Convert.ToInt32(aeronaveSeleccionada.Cells["Cantidad de Kgs disponibles para realizar encomiendas"].Value.ToString());
                    Aeronave unaAeronave = new Aeronave();
                    unaAeronave.setMatricula(matriculaAeronaveSeleccionada);
                    unaAeronave.setModelo(modeloAeronaveSeleccionada);
                    unaAeronave.setFabricante(fabricanteAeronaveSeleccionada);
                    unaAeronave.setTipoDeServicio(tipoDeServicioAeronaveSeleccionada);
                    unaAeronave.setCantidadKgs(cantidadDeKgs);

                    if (!ConexionALaBase.Conexion.consultarBase("select * from MM.Viajes where estado='habilitado' and fecha_salida > mm.fechaDeHoy() and Matricula ='" + unaAeronave.getMatricula() + "'").HasRows)
                    {
                        String noQuery = "exec MM.CancelarAeronaveVidaUtil '" + unaAeronave.getMatricula() + "'";
                        ConexionALaBase.Conexion.ejecutarNonQuery(noQuery);
                        MessageBox.Show("Se ha dado de baja la aeronave (no tenia pasajes, encomiendas o viajes asociados)");
                        new buscarAeronave().Show();
                        this.Close();
                    }
                    else
                    {
                        new cancelarOReemplazarVidaUtil(unaAeronave).Show();
                        this.Close();
                    }

               }
           }
        }

        private void button3_Click(object sender, EventArgs e) //FUERA DE SERVICIO
        {
           if (Validaciones.Validaciones.validarDataGridView(dataGridView1, "Selecciona una Aeronave a dar de baja por Fuera de Servicio"))
            {  
                
               DataGridViewRow aeronaveSeleccionada = this.dataGridView1.SelectedRows[0];
               String fechaBajaDefinitivaAeronaveSeleccionada = aeronaveSeleccionada.Cells["Fecha de baja definitiva"].Value.ToString();
               String fechaBajaTemporalAeronaveSeleccionada = aeronaveSeleccionada.Cells["Fecha de fuera de servicio"].Value.ToString();
               String fechaAltaTemporalAeronaveSeleccionada = aeronaveSeleccionada.Cells["Fecha de reinicio de servicio"].Value.ToString();

               if (!fechaBajaDefinitivaAeronaveSeleccionada.Equals("")||(!fechaBajaTemporalAeronaveSeleccionada.Equals("") &&
                   DateTime.Parse(Properties.Settings.Default.fechaDelSistema)>DateTime.Parse(fechaBajaTemporalAeronaveSeleccionada) && DateTime.Parse(fechaAltaTemporalAeronaveSeleccionada)<DateTime.Parse(Properties.Settings.Default.fechaDelSistema)))
               {
                    MessageBox.Show("Ya se encuentra fuera de servicio la aeronave seleccionada");
               }
               else{
                   String matriculaAeronaveSeleccionada = aeronaveSeleccionada.Cells["Matrícula"].Value.ToString();
                   String modeloAeronaveSeleccionada = aeronaveSeleccionada.Cells["Modelo"].Value.ToString();
                   String fabricanteAeronaveSeleccionada = aeronaveSeleccionada.Cells["Fabricante"].Value.ToString();
                   String tipoDeServicioAeronaveSeleccionada = aeronaveSeleccionada.Cells["Tipo de Servicio"].Value.ToString();
                   int cantidadDeKgs = Convert.ToInt32(aeronaveSeleccionada.Cells["Cantidad de Kgs disponibles para realizar encomiendas"].Value.ToString());
                   Aeronave unaAeronave = new Aeronave();
                   unaAeronave.setMatricula(matriculaAeronaveSeleccionada);
                   unaAeronave.setModelo(modeloAeronaveSeleccionada);
                   unaAeronave.setFabricante(fabricanteAeronaveSeleccionada);
                   unaAeronave.setTipoDeServicio(tipoDeServicioAeronaveSeleccionada);
                   unaAeronave.setCantidadKgs(cantidadDeKgs);
   
                   new bajaFueraDeServicio(unaAeronave).Show();
                   this.Close();
               }    
            }
        }

        private void buscarAeronave_Load(object sender, EventArgs e)
        {
            ConexionALaBase.CargadorDeEstructuras.cargarDataGrid(dataGridView1, "select * from MM.vista_aeronaves");
          
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new crearAeronave().Show();
            this.Close();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Validaciones.Validaciones.validarDataGridView(dataGridView1, "Selecciona una Aeronave para ser modificada"))
            {
                DataGridViewRow aeronaveSeleccionada = this.dataGridView1.SelectedRows[0];
                String matriculaAeronaveSeleccionada = aeronaveSeleccionada.Cells["Matrícula"].Value.ToString();
                String modeloAeronaveSeleccionada = aeronaveSeleccionada.Cells["Modelo"].Value.ToString();
                String fabricanteAeronaveSeleccionada = aeronaveSeleccionada.Cells["Fabricante"].Value.ToString();
                String tipoDeServicioAeronaveSeleccionada = aeronaveSeleccionada.Cells["Tipo de Servicio"].Value.ToString();
                int cantidadDeKgs = Convert.ToInt32(aeronaveSeleccionada.Cells["Cantidad de Kgs disponibles para realizar encomiendas"].Value.ToString());
                int cantidadDePisos = Convert.ToInt32(aeronaveSeleccionada.Cells["Cantidad de pisos"].Value.ToString());
                int cantidadDeAsientos = Convert.ToInt32(aeronaveSeleccionada.Cells["Cantidad de asientos"].Value.ToString());
                Aeronave unaAeronave = new Aeronave();
                unaAeronave.setMatricula(matriculaAeronaveSeleccionada);
                unaAeronave.setModelo(modeloAeronaveSeleccionada);
                unaAeronave.setFabricante(fabricanteAeronaveSeleccionada);
                unaAeronave.setTipoDeServicio(tipoDeServicioAeronaveSeleccionada);
                unaAeronave.setCantidadKgs(cantidadDeKgs);
                unaAeronave.setCantidadDePisos(cantidadDePisos);
                unaAeronave.setCantidadButacas(cantidadDeAsientos);
                
                System.Data.SqlClient.SqlDataReader reader = ConexionALaBase.Conexion.consultarBase("Select count(*) from mm.viajes where estado='habilitado' and Matricula='" + matriculaAeronaveSeleccionada + "'");
                int cantidadViajes = 0;
                if (reader.Read())
                {
                     cantidadViajes = reader.GetInt32(0);
                }
                if (cantidadViajes == 0)
                {
                    new modificarAeronave(unaAeronave).Show();
                    this.Close();
                }
                else MessageBox.Show("No puede modificar una aeronave que tenga viajes asociados"); 
                
            }
        }
    }
}

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

        private void button2_Click(object sender, EventArgs e)
        {
           if (Validaciones.Validaciones.validarDataGridView(dataGridView1, "Selecciona una Aeronave a dar de baja por Vida Util"))
            {
                DataGridViewRow aeronaveSeleccionada = this.dataGridView1.SelectedRows[0];
                String dadaDeBajaPorVidaUtilAeronaveSeleccionada = aeronaveSeleccionada.Cells["Baja por vida util"].Value.ToString();
                if (dadaDeBajaPorVidaUtilAeronaveSeleccionada.Equals("SI"))
                {
                    MessageBox.Show("Ya se encuentra dada de baja por vida util la aeronave seleccionada");
                }
                else
                {   
                    String matriculaAeronaveSeleccionada = aeronaveSeleccionada.Cells["Matrícula"].Value.ToString();
                    String modeloAeronaveSeleccionada = aeronaveSeleccionada.Cells["Modelo"].Value.ToString();
                    String fabricanteAeronaveSeleccionada = aeronaveSeleccionada.Cells["Fabricante"].Value.ToString();
                    String tipoDeServicioAeronaveSeleccionada = aeronaveSeleccionada.Cells["Tipo de Servicio"].Value.ToString();
                    Aeronave unaAeronave = new Aeronave();
                    unaAeronave.setMatricula(matriculaAeronaveSeleccionada);
                    unaAeronave.setModelo(modeloAeronaveSeleccionada);
                    unaAeronave.setFabricante(fabricanteAeronaveSeleccionada);
                    unaAeronave.setTipoDeServicio(tipoDeServicioAeronaveSeleccionada);
                    
                    new cancelarOReemplazarVidaUtil(unaAeronave).Show();
                    this.Close();
                    /*
                    String matriculaAeronaveSeleccionada = aeronaveSeleccionada.Cells["Matrícula"].Value.ToString();
                    String noQueryDelete = "delete from viajes where viajes.Matricula='" + matriculaAeronaveSeleccionada + "'";
                    String noQueryUpdate = "UPDATE Aeronaves set Baja_Vida_Util='SI',Fecha_Baja_Definitiva=GETDATE() where matricula='" + matriculaAeronaveSeleccionada + "'";
                    ConexionALaBase.Conexion.ejecutarNonQuery(noQueryUpdate);
                    ConexionALaBase.Conexion.ejecutarNonQuery(noQueryDelete);// Comentado hasta que se desarrolle el trigger necesario, sin el trigger rompe al ejecutarse.
                    MessageBox.Show("Se ha dado de baja por fin de la vida util la aeronave seleccionada, todos los vuelos,pasajes y encomiendas asociados a la misma han sido cancelados");
                    new buscarAeronave().Show();
                    this.Close();
                    */ 
               }
           }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           if (Validaciones.Validaciones.validarDataGridView(dataGridView1, "Selecciona una Aeronave a dar de baja por Fuera de Servicio"))
            {  
                
               DataGridViewRow aeronaveSeleccionada = this.dataGridView1.SelectedRows[0];
               String estadoServicioAeronaveSeleccionada = aeronaveSeleccionada.Cells["Baja por fuera de servicio"].Value.ToString();
               if(estadoServicioAeronaveSeleccionada.Equals("SI")){
                    MessageBox.Show("Ya se encuentra fuera de servicio la aeronave seleccionada");
               }else{
                   String matriculaAeronaveSeleccionada = aeronaveSeleccionada.Cells["Matrícula"].Value.ToString();
                   String modeloAeronaveSeleccionada = aeronaveSeleccionada.Cells["Modelo"].Value.ToString();
                   String fabricanteAeronaveSeleccionada = aeronaveSeleccionada.Cells["Fabricante"].Value.ToString();
                   String tipoDeServicioAeronaveSeleccionada = aeronaveSeleccionada.Cells["Tipo de Servicio"].Value.ToString();
                   Aeronave unaAeronave = new Aeronave();
                   unaAeronave.setMatricula(matriculaAeronaveSeleccionada);
                   unaAeronave.setModelo(modeloAeronaveSeleccionada);
                   unaAeronave.setFabricante(fabricanteAeronaveSeleccionada);
                   unaAeronave.setTipoDeServicio(tipoDeServicioAeronaveSeleccionada);
   
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
    }
}

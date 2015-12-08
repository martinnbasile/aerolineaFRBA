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
    public partial class crearAeronaveSustituta : Form
    {
        String deQueBajaVengo;
        Aeronave aeronaveAReemplazar;
        public crearAeronaveSustituta(Aeronave unaAeronave, String tipoDeBaja)
        {
            
            aeronaveAReemplazar = unaAeronave;
            InitializeComponent();
            deQueBajaVengo = tipoDeBaja;
        }

        private void crearAeronave_Load(object sender, EventArgs e)
        {
            ConexionALaBase.CargadorDeEstructuras.cargarDataGrid(dataGridView1,"select * from MM.vista_modelos where Fabricante='"+aeronaveAReemplazar.getFabricante()+"' and [Tipo de servicio]='"+aeronaveAReemplazar.getTipoDeServicio()+"' and [Cantidad de Kgs disponibles para realizar encomiendas]="+aeronaveAReemplazar.getCantidadKgs()+"");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (deQueBajaVengo=="Tempo")
            {
                new cancelarOReemplazarFueraDeServicio(aeronaveAReemplazar).Show();
            }
            else 
            {
            new cancelarOReemplazarVidaUtil(aeronaveAReemplazar).Show();
            }
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (estaCompleto())
            {

                
                String nuevaAeronaveMatricula = maskedTextBox2.Text;
        

                SqlDataReader consulta = ConexionALaBase.Conexion.consultarBase("select modelo from MM.Aeronaves where matricula='"+aeronaveAReemplazar.getMatricula()+"'");
                int idModelo = new int();
                if (consulta.Read()) { idModelo = consulta.GetInt32(consulta.GetOrdinal("modelo")); }

                String queryValidarMatricula = "select * from MM.Aeronaves where Matricula='" + nuevaAeronaveMatricula + "'";
                SqlDataReader consultaValidarMatricula = ConexionALaBase.Conexion.consultarBase(queryValidarMatricula);
                if (consultaValidarMatricula.HasRows)
                {
                    MessageBox.Show("Ya existe una aeronave con la matrícula elegida, ingrese una matrícula diferente");
                }
                else
                {

                    String noQueryCrearAeronave = "exec MM.crearAeronave @matricula='" + nuevaAeronaveMatricula + "',@id_Modelo=" + idModelo + "";
                    ConexionALaBase.Conexion.ejecutarNonQuery(noQueryCrearAeronave);
                    String noQueryActualizarViajes = "update MM.viajes set MM.viajes.Matricula='" + nuevaAeronaveMatricula + "' where MM.viajes.Matricula='" + aeronaveAReemplazar.getMatricula() + "' and (MM.viajes.Fecha_salida between '" + aeronaveAReemplazar.getFechaBajaFueraServicio() + "' and '" + aeronaveAReemplazar.getFechaAltaFueraServicio() + "' or  MM.viajes.Fecha_Estimada_llegada between '" + aeronaveAReemplazar.getFechaBajaFueraServicio() + "' and '" + aeronaveAReemplazar.getFechaAltaFueraServicio() + "')";
                    ConexionALaBase.Conexion.ejecutarNonQuery(noQueryActualizarViajes);
                    String noQueryBaja;
                    if(deQueBajaVengo=="Tempo")noQueryBaja="UPDATE MM.Aeronaves set fecha_baja_fuera_servicio=mm.fechaDeHoy(),fecha_alta_fuera_servicio='"+aeronaveAReemplazar.getFechaAltaFueraServicio()+"' where matricula='" + aeronaveAReemplazar.getMatricula() + "'";
                    else noQueryBaja = "UPDATE MM.Aeronaves set fecha_baja_definitiva=mm.fechaDeHoy() where matricula='" + aeronaveAReemplazar.getMatricula() + "'";
                    ConexionALaBase.Conexion.ejecutarNonQuery(noQueryBaja);
                    MessageBox.Show("La aeronave a sido dada de baja y se ha asignado la aeronave creada para que la reemplace en los vuelos correspondientes");
                    new buscarAeronave().Show();
                    this.Close();
                }
            }
        }

        private bool estaCompleto()
        {
             if (Validaciones.Validaciones.validarMatricula(maskedTextBox2, "Completar la matrícula"))
                    {
                        return true;
                    }
                  
            return false;
        }

        private void maskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
          
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

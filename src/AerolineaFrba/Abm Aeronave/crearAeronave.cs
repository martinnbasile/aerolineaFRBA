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
    public partial class crearAeronave : Form
    {
        public crearAeronave()
        {
            InitializeComponent();
        }

        private void crearAeronave_Load(object sender, EventArgs e)
        {

            ConexionALaBase.CargadorDeEstructuras.cargarDataGrid(dataGridView1, "select * from MM.vista_modelos");
            //ConexionALaBase.CargadorDeEstructuras.cargarComboBox(comboBox2, ConexionALaBase.Conexion.consultarBase("Select descripcion from MM.Fabricantes"));
            //ConexionALaBase.CargadorDeEstructuras.cargarComboBox(comboBox1, ConexionALaBase.Conexion.consultarBase("select Descripcion from MM.Tipos_Servicio"));
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private bool estaCompleto()
        {
            if (Validaciones.Validaciones.validarMatricula(maskedTextBox2, "Completar la matrícula"))
            {
                if (Validaciones.Validaciones.validarDataGridView(dataGridView1, "Seleccione un modelo"))
                {
                    return true;
                }
            }
            return false;
        }
        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new buscarAeronave().Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            if (estaCompleto())
            {
               
                DataGridViewRow modeloSeleccionado = this.dataGridView1.SelectedRows[0];
                String nuevaAeronaveMatricula = maskedTextBox2.Text;
                String nuevaAeronaveFabricante = modeloSeleccionado.Cells["Fabricante"].Value.ToString();
                String nuevaAeronaveTipoDeServicio = modeloSeleccionado.Cells["Tipo de servicio"].Value.ToString();
                int nuevaAeronaveCantidadDeKgs = Convert.ToInt32(modeloSeleccionado.Cells["Cantidad de Kgs disponibles para realizar encomiendas"].Value.ToString());

                              
                SqlDataReader consulta = ConexionALaBase.Conexion.consultarBase("select id from MM.Tipos_Servicio where Descripcion='" + nuevaAeronaveTipoDeServicio + "'");
                int idTipoServicio = new int();
                if (consulta.Read()) { idTipoServicio = consulta.GetInt32(consulta.GetOrdinal("id")); }
                consulta = ConexionALaBase.Conexion.consultarBase("select id from MM.Fabricantes where Descripcion='" + nuevaAeronaveFabricante + "'");
                int idFabricante = new int();
                if (consulta.Read()) { idFabricante = consulta.GetInt32(consulta.GetOrdinal("id")); }
                consulta = ConexionALaBase.Conexion.consultarBase("select id from MM.modeloAvion where fabricante="+idFabricante+" and tipoServicio="+idTipoServicio+" and Kg="+nuevaAeronaveCantidadDeKgs+"");
                int idModelo = new int();
                if (consulta.Read()) { idModelo = consulta.GetInt32(consulta.GetOrdinal("id")); }


                String queryValidarMatricula = "select * from MM.Aeronaves where Matricula='" + nuevaAeronaveMatricula + "'";
                SqlDataReader consultaValidarMatricula = ConexionALaBase.Conexion.consultarBase(queryValidarMatricula);
                if (consultaValidarMatricula.HasRows)
                {
                    MessageBox.Show("Ya existe una aeronave con la matrícula elegida, ingrese una matrícula diferente");
                }
                else
                {
                    String noQueryCrearAeronave = "exec MM.crearAeronave @matricula='"+nuevaAeronaveMatricula+"',@id_Modelo="+idModelo+"";
                    ConexionALaBase.Conexion.ejecutarNonQuery(noQueryCrearAeronave);
                    MessageBox.Show("Se ha creado la Aeronave satisfactoriamente");
                    new buscarAeronave().Show();
                    this.Close();
                }
           
            }
             
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            new crearModelo("crearAeronave").Show();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

    }
}

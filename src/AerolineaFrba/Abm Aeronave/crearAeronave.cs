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
          
            ConexionALaBase.CargadorDeEstructuras.cargarComboBox(comboBox2, ConexionALaBase.Conexion.consultarBase("Select descripcion from MM.Fabricantes"));
            ConexionALaBase.CargadorDeEstructuras.cargarComboBox(comboBox1, ConexionALaBase.Conexion.consultarBase("select Descripcion from MM.Tipos_Servicio"));
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
                if (Validaciones.Validaciones.validarMaskedTextBox(maskedTextBox1, "Completar el modelo"))
                {
                    if (Validaciones.Validaciones.validarComboBox(comboBox2, "Seleccione un Fabricante"))
                    {
                        if (Validaciones.Validaciones.validarComboBox(comboBox1, "Seleccione un Tipo de Servicio"))
                        {
                            if (Validaciones.Validaciones.validarNumericUpDown(numericUpDown1, "Completar la cantidad de butacas con un valor mayor a cero"))
                            {
                                if (Validaciones.Validaciones.validarNumericUpDown(numericUpDown2, "Completar la cantidad de kilogramos con un valor mayor a cero"))
                                {
                                    return true;
                                }
                            }
                        }
                    }
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
                Aeronave nuevaAeronave = new Aeronave();
                String nuevaAeronaveMatricula = maskedTextBox2.Text;
                String nuevaAeronaveModelo = maskedTextBox1.Text;
                String nuevaAeronaveFabricante = comboBox2.SelectedItem.ToString();
                String nuevaAeronaveTipoDeServicio = comboBox1.SelectedItem.ToString();
                int nuevaAeronaveCantidadDeButacas = Convert.ToInt32(numericUpDown1.Value);
                int nuevaAeronaveCantidadKgs = Convert.ToInt32(numericUpDown2.Value);
                nuevaAeronave.setMatricula(nuevaAeronaveMatricula);
                nuevaAeronave.setModelo(nuevaAeronaveModelo);
                nuevaAeronave.setFabricante(nuevaAeronaveFabricante);
                nuevaAeronave.setTipoDeServicio(nuevaAeronaveTipoDeServicio);
                nuevaAeronave.setCantidadButacas(nuevaAeronaveCantidadDeButacas);
                nuevaAeronave.setCantidadKgs(nuevaAeronaveCantidadKgs);
                
                SqlDataReader consulta = ConexionALaBase.Conexion.consultarBase("select id from MM.Tipos_Servicio where Descripcion='" + nuevaAeronave.getTipoDeServicio() + "'");
                int idTipoServicio = new int();
                if (consulta.Read()) { idTipoServicio = consulta.GetInt32(consulta.GetOrdinal("id")); }
                consulta = ConexionALaBase.Conexion.consultarBase("select id from MM.Fabricantes where Descripcion='" + nuevaAeronave.getFabricante() + "'");
                int idFabricante = new int();
                if (consulta.Read()) { idFabricante = consulta.GetInt32(consulta.GetOrdinal("id")); }

                String queryValidarMatricula = "select * from MM.Aeronaves where Matricula='" + nuevaAeronave.getMatricula() + "'";
                SqlDataReader consultaValidarMatricula = ConexionALaBase.Conexion.consultarBase(queryValidarMatricula);
                if (consultaValidarMatricula.HasRows)
                {
                    MessageBox.Show("Ya existe una aeronave con la matrícula elegida, ingrese una matrícula diferente");
                }
                else
                {
                    String fechaActual = DateTime.Now.ToString("yyyy-MM-dd");
                    ConexionALaBase.Conexion.ejecutarNonQuery("INSERT INTO MM.Aeronaves (matricula,Fecha_alta,Modelo,Fabricante,Tipo_Servicio,Cantidad_Butacas,Cantidad_Kg) VALUES ('" + nuevaAeronave.getMatricula() + "','" + fechaActual + "','" + nuevaAeronave.getModelo() + "','" + idFabricante + "'," + idTipoServicio + "," + nuevaAeronave.getCantidadButacas() + "," + nuevaAeronave.getCantidadKgs() + ")");
                    MessageBox.Show("Se ha creado la Aeronave satisfactoriamente");
                    new buscarAeronave().Show();
                    this.Close();
                }
            }
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

    }
}

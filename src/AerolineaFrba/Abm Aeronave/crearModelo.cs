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
    public partial class crearModelo : Form
    {
        public crearModelo()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new crearAeronave().Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (estaCompleto())
            {
                if (ConexionALaBase.Conexion.consultarBase("Select * from MM.vista_modelos where Modelo='" + textBox2.Text + "'").HasRows)
                {
                    MessageBox.Show("Ya existe un modelo con el nombre ingresado, ingrese uno diferente");
                }
                else
                {   String nuevoModeloDescripcion = textBox2.Text;
                    String nuevoModeloFabricante = comboBox2.SelectedItem.ToString();
                    String nuevoModeloTipoDeServicio = comboBox1.SelectedItem.ToString();
                    int cantidadDeKilogramosParaEncomiendas = Convert.ToInt32(numericUpDown2.Value);
                    int cantidadDePisos = Convert.ToInt32 (numericUpDown1.Value);
                    int cantidadDeButacasPorPiso = Convert.ToInt32(numericUpDown3.Value);
                    
  

                    SqlDataReader consulta = ConexionALaBase.Conexion.consultarBase("select id from MM.Tipos_Servicio where Descripcion='" +nuevoModeloTipoDeServicio+ "'");
                    int idTipoServicio = new int();
                    if (consulta.Read()) { idTipoServicio = consulta.GetInt32(consulta.GetOrdinal("id")); }
                    consulta = ConexionALaBase.Conexion.consultarBase("select id from MM.Fabricantes where Descripcion='" + nuevoModeloFabricante + "'");
                    int idFabricante = new int();
                    if (consulta.Read()) { idFabricante = consulta.GetInt32(consulta.GetOrdinal("id")); }

                     String noQueryCrearModelo = "exec MM.crearModeloAvion @modeloDescripcion='" + nuevoModeloDescripcion + "',@Kg =" + cantidadDeKilogramosParaEncomiendas + ",@fabricante=" + idFabricante + ",@tipoServicio=" + idTipoServicio + ",@cantPisos=" + cantidadDePisos + ", @cantButacasPiso=" + cantidadDeButacasPorPiso + "";
                     ConexionALaBase.Conexion.ejecutarNonQuery(noQueryCrearModelo);
                     MessageBox.Show("Se ha creado el modelo satisfactoriamente");
                     new crearAeronave().Show();
                     this.Close();
                }
            }
        }

        private bool estaCompleto()
        {
            if (Validaciones.Validaciones.validarTextBox(textBox2, "Ingrese un nombre para el modelo"))
            {
                if (Validaciones.Validaciones.validarComboBox(comboBox2, "Seleccione un fabricante"))
                {
                    if (Validaciones.Validaciones.validarComboBox(comboBox1, "Seleccione un tipo de servicio"))
                    {
                        if (Validaciones.Validaciones.validarNumericUpDown(numericUpDown2, "Ingrese la cantidad de Kgs para encomiendas"))
                        {
                            if (Validaciones.Validaciones.validarNumericUpDown(numericUpDown1, "Ingrese la cantidad de pisos"))
                            {
                                if (Validaciones.Validaciones.validarNumericUpDown(numericUpDown3, "Ingrese la cantidad de butacas por piso"))
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void crearModelo_Load(object sender, EventArgs e)
        {
            ConexionALaBase.CargadorDeEstructuras.cargarComboBox(comboBox2, ConexionALaBase.Conexion.consultarBase("select Descripcion from MM.Fabricantes"));
            ConexionALaBase.CargadorDeEstructuras.cargarComboBox(comboBox1, ConexionALaBase.Conexion.consultarBase("select Descripcion from MM.Tipos_Servicio"));
        }
    }
}

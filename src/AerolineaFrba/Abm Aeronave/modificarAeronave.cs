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
    public partial class modificarAeronave : Form
    {
        Aeronave aeronaveAModificar;
        public modificarAeronave(Aeronave unaAeronave)
        {
            aeronaveAModificar = unaAeronave;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new buscarAeronave().Show();
            this.Close();
        }

        private void modificarAeronave_Load(object sender, EventArgs e)
        {
            maskedTextBox2.Text = aeronaveAModificar.getMatricula();
            textModelo.Text = aeronaveAModificar.getModelo();
            textFabricante.Text = aeronaveAModificar.getFabricante();
            textTS.Text = aeronaveAModificar.getTipoDeServicio();
            textKg.Text = aeronaveAModificar.getCantidadKgs().ToString();
            textPisos.Text = aeronaveAModificar.getCantidadDePisos().ToString();
            textButacas.Text = aeronaveAModificar.getCantidadButacas().ToString();
            ConexionALaBase.CargadorDeEstructuras.cargarDataGrid(dataGridView1, "select * from MM.vista_modelos");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(estaCompleto())
            {
                bool seleccionoUnModelo = dataGridView1.SelectedRows.Count == 1;
                bool ingresoUnaMatricula = Validaciones.Validaciones.validarMatricula(maskedTextBox2, "Ingrese la matricula con el formato correcto");
                if (seleccionoUnModelo)
                {
                    DataGridViewRow modeloSeleccionado = this.dataGridView1.SelectedRows[0];
                    int idModeloSeleccionado = Convert.ToInt32(modeloSeleccionado.Cells["id"].Value.ToString());
                    ConexionALaBase.Conexion.ejecutarNonQuery("update mm.aeronaves set modelo=" + idModeloSeleccionado + " where matricula='" + aeronaveAModificar.getMatricula() + "'");
                }
                if (ingresoUnaMatricula)
                {
                    String nuevaMatricula = maskedTextBox2.Text;
                    ConexionALaBase.Conexion.ejecutarNonQuery("update mm.aeronaves set matricula='" + nuevaMatricula + "' where matricula='" + aeronaveAModificar.getMatricula() + "'");
                }
                MessageBox.Show("Se ha modificado la aeronave exitosamente");
                new buscarAeronave().Show();
                this.Close();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            new crearModelo("modificarAeronave",aeronaveAModificar).Show();
            this.Close();
        }

        public bool estaCompleto()
        {
            bool noSeleccionoUnModelo = dataGridView1.SelectedRows.Count != 1;
            bool noIngresoUnaMatricula = maskedTextBox2.Text.Length < 7;
            if(noSeleccionoUnModelo && noIngresoUnaMatricula)
            {
                MessageBox.Show("Seleccione un modelo o ingrese una matrícula para guardar los cambios");
                return false;
            }
            return true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}

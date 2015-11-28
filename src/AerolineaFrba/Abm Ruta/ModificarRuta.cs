using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Abm_Ruta
{
    public partial class ModificarRuta : Form
    {
        int idRutaE;
        public ModificarRuta(Abm_Ruta.Ruta unaRuta, int idRuta)
        {
            InitializeComponent();
            System.Data.SqlClient.SqlDataReader reader = ConexionALaBase.Conexion.consultarBase("Select descripcion from MM.ciudades where estado='Habilitado'");
            ConexionALaBase.CargadorDeEstructuras.cargarComboBox(comboBox1, reader);
            reader.Dispose();
            reader = ConexionALaBase.Conexion.consultarBase("Select descripcion from MM.ciudades where estado='Habilitado'");
            ConexionALaBase.CargadorDeEstructuras.cargarComboBox(comboBox2, reader);
            reader.Dispose();
            reader = ConexionALaBase.Conexion.consultarBase("Select descripcion from MM.Tipos_Servicio");
            ConexionALaBase.CargadorDeEstructuras.cargarComboBox(comboBox3, reader);
            reader.Dispose();
            idRutaE = idRuta;

            comboBox1.Text = unaRuta.getOrigen();
            comboBox2.Text = unaRuta.getDestino();
            comboBox3.Text = unaRuta.getServicio();
            maskedTextBox1.Text = unaRuta.getPrecioBase();
            maskedTextBox2.Text = unaRuta.getPrecioEncomienda();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Abm_Ruta.ABM_RUTA().Show();
            this.Close();
        }

        private bool validarTodo()
        {
            if (Validaciones.Validaciones.validarComboBox(comboBox2, "Completar ciudad de destino"))
            {
                if (Validaciones.Validaciones.validarComboBox(comboBox1, "Completar ciudad de Origen"))
                {
                    if (Validaciones.Validaciones.validarComboBox(comboBox3, "Completar el tipo de servicio"))
                    {
                        if (Validaciones.Validaciones.validarMaskedTextBox(maskedTextBox1, "Completar precio base"))
                        {
                            if (Validaciones.Validaciones.validarMaskedTextBox(maskedTextBox2, "Completar precio base de encomienda"))
                            {
                                if (comboBox1.Text != comboBox2.Text)
                                {
                                    return true;
                                }
                                else MessageBox.Show("No puede ser la misma la ciudad origen y destino");
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)//COnfirmada la actualizacion
        {
            if (this.validarTodo())
            {
                String nuevoOrigen = comboBox1.SelectedItem.ToString();
                String nuevoDestino = comboBox2.SelectedItem.ToString();
                String nuevoServicio = comboBox3.SelectedItem.ToString();
                int nuevoPrecioBase = int.Parse(maskedTextBox1.Text);
                int nuevoPrecioEncomienda = int.Parse(maskedTextBox2.Text);
                ConexionALaBase.Conexion.ejecutarNonQuery("execute mm.actualizarRuta " + idRutaE + ", '" + nuevoDestino + "', '" + nuevoOrigen + "', '" + nuevoServicio + "', " + nuevoPrecioBase + ", " + nuevoPrecioEncomienda);
                MessageBox.Show("Se actualizo la ruta correctamente");
                new ABM_RUTA().Show();
                this.Close();
            }
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}

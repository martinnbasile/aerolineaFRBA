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
    public partial class NuevaRuta : Form
    {

        public NuevaRuta()
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Abm_Ruta.ABM_RUTA().Show();
            this.Close();
            return;
        }

        private bool validarTodo(){
            if (Validaciones.Validaciones.validarComboBox(comboBox2,"Completar ciudad de destino")){
                if (Validaciones.Validaciones.validarComboBox(comboBox1,"Completar ciudad de Origen")){
                    if (Validaciones.Validaciones.validarComboBox(comboBox3,"Completar el tipo de servicio")){
                        if (Validaciones.Validaciones.validarTextBox(textBox1,"Completar precio base")){
                            if (Validaciones.Validaciones.validarTextBox(textBox2, "Completar precio base de encomienda"))
                            {
                                if (Validaciones.Validaciones.validarFloatTextBox(textBox2, "El valor debe precio base encomienda debe ser un float separado por punto"))
                                {
                                    if (Validaciones.Validaciones.validarFloatTextBox(textBox1, "El valor debe precio base pasaje debe ser un float separado por punto"))
                                    {
                                        if (comboBox2.Text != comboBox1.Text)
                                        {
                                            return true;
                                        }

                                        else MessageBox.Show("La ciudade de origen y de destino no puede ser la misma");
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            
            return false;
        }


        private void button1_Click(object sender, EventArgs e) //BOTON CONFIRMAR
        {
            //Falta implementar la insercion en la base
            if (this.validarTodo())
            {
                String ciudadDestino = comboBox2.Text;
                String ciudadOrigen = comboBox1.Text;
                String servicio = comboBox3.Text;
                String precioBase = textBox1.Text;
                String precioBaseEncomienda = textBox2.Text;


                ConexionALaBase.Conexion.ejecutarNonQuery("exec mm.crearRuta '"+ ciudadDestino +"', '"+ ciudadOrigen+"', '"+ servicio+"', "+ precioBase +", "+precioBaseEncomienda);
                MessageBox.Show("Operacion exitosa");
                new ABM_RUTA().Show();
                this.Close();

            }

            
           
        }

        private void NuevaRuta_Load(object sender, EventArgs e)
        {

        }
    }
}

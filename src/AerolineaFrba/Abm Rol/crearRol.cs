using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AerolineaFrba.Validaciones;

namespace AerolineaFrba.Abm_Rol
{
    public partial class crearRol : Form
    {
        public crearRol()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new buscarRol().Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 0) { MessageBox.Show("Complete el nombre"); return; }
            if (comboBox2.SelectedIndex == -1) { MessageBox.Show("Complete el estado"); return; }
            if (listBox1.Items.Count == 0) { MessageBox.Show("Agregue como mínimo una funcionalidad al rol"); return; }
            if (ConexionALaBase.Conexion.consultarBase("select * from MM.Roles where Descripcion='" + textBox2.Text + "'").HasRows)
            {
                MessageBox.Show("Ya existe un Rol con el nombre ingresado, ingrese uno diferente");
                return;
            }
            else
            {
                String noQueryCrearRol = "INSERT INTO MM.Roles (Descripcion,Estado) VALUES ('" + textBox2.Text + "','" + comboBox2.SelectedItem.ToString() + "')";
                ConexionALaBase.Conexion.ejecutarNonQuery(noQueryCrearRol);

                System.Data.SqlClient.SqlCommand comando = ConexionALaBase.Conexion.getComando();

                String noQueryCrearTablaTemporalFuncionalidades = "Create table #tablaTemporal (funcionalidad varchar(70))";
                comando.CommandText = noQueryCrearTablaTemporalFuncionalidades;
                comando.ExecuteNonQuery();
                foreach (Object unaFuncionalidad in listBox1.Items)
                {

                    String funcionalidad = unaFuncionalidad.ToString();
                    String noQueryInsertEnTablaTemporal = "insert into #tablaTemporal values('" + funcionalidad + "')";
                    comando.CommandText = noQueryInsertEnTablaTemporal;
                    comando.ExecuteNonQuery();
                }
                String noQueryActualizarFuncionalidades = "exec MM.agregarFuncionalidadesRol " + textBox2.Text + "";
                comando.CommandText = noQueryActualizarFuncionalidades;
                comando.ExecuteNonQuery();

                MessageBox.Show("Se ha creado el rol exitosamente");
                new buscarRol().Show();
                this.Close();
            }
           
            
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void crearRol_Load(object sender, EventArgs e)
        {

            String queryFuncionalidadesNoDisponibles = "select Descripcion from MM.Funcionalidades";
            ConexionALaBase.CargadorDeEstructuras.cargarListBox(listBox2, ConexionALaBase.Conexion.consultarBase(queryFuncionalidadesNoDisponibles));

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validaciones.Validaciones.validarListBox(listBox2, "Seleccione una funcionalidad a agregar"))
            {
                listBox1.Items.Add(listBox2.SelectedItem);
                listBox2.Items.Remove(listBox2.SelectedItem);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Validaciones.Validaciones.validarListBox(listBox1, "Seleccione una funcionalidad a quitar"))
            {
                listBox2.Items.Add(listBox1.SelectedItem);
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        }
    }
}

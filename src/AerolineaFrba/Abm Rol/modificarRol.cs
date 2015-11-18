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

namespace AerolineaFrba.Abm_Rol
{
    public partial class modificarRol : Form
    {
        String rolModificado;
           
        public modificarRol(String unRol)
        {
            rolModificado=unRol;
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void modificarRol_Load(object sender, EventArgs e)
        {
            textBox2.Text = rolModificado;
            textBox2.ReadOnly = true;
            SqlDataReader reader = ConexionALaBase.Conexion.consultarBase("select Estado from MM.Roles where Descripcion='" + rolModificado + "'");
            if (reader.Read())
            {
                textBox3.Text=reader.GetString(0);
                textBox3.ReadOnly = true;
            }
            ConexionALaBase.CargadorDeEstructuras.cargarListBox(listBox1,ConexionALaBase.Conexion.consultarBase("Select * from MM.funcionalidadPorRol where Rol = '"+rolModificado+"'"));
            String queryFuncionalidadesNoDisponibles= "select f.Descripcion from MM.Funcionalidades f LEFT JOIN MM.funcionalidadPorRol fr ON f.Descripcion=fr.Descripcion and fr.Rol='"+rolModificado+"' WHERE fr.Rol is NULL";
            ConexionALaBase.CargadorDeEstructuras.cargarListBox(listBox2, ConexionALaBase.Conexion.consultarBase(queryFuncionalidadesNoDisponibles));

        }

        private void button4_Click(object sender, EventArgs e)
        {
            new buscarRol().Show();
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Validaciones.Validaciones.validarListBox(listBox1,"Seleccione una funcionalidad a quitar"))
            {
                listBox2.Items.Add(listBox1.SelectedItem);
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validaciones.Validaciones.validarListBox(listBox2, "Seleccione una funcionalidad a agregar"))
            {
                listBox1.Items.Add(listBox2.SelectedItem);
                listBox2.Items.Remove(listBox2.SelectedItem);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Falta el update en la base");
            new buscarRol();
            this.Close();
        }
    }
}

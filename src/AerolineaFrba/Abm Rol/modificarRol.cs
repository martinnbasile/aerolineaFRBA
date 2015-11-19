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
            if ((textBox1.Text.Length != 0) && (ConexionALaBase.Conexion.consultarBase("select * from MM.Roles where Descripcion='" + textBox1.Text + "'").HasRows))
            {
                MessageBox.Show("Ya existe un Rol con el nombre ingresado, ingrese uno diferente");
            }
            else
            {
                System.Data.SqlClient.SqlCommand comando = ConexionALaBase.Conexion.getComando();
                
                String noQueryCrearTablaTemporalFuncionalidades = "Create table #tablaTemporal (funcionalidad varchar(70))";
                comando.CommandText = noQueryCrearTablaTemporalFuncionalidades;
                comando.ExecuteNonQuery();
                foreach (Object unaFuncionalidad in listBox1.Items)
                {

                    String funcionalidad = unaFuncionalidad.ToString();
                    String noQueryInsertEnTablaTemporal = "insert into #tablaTemporal values('"+funcionalidad+"')";
                    comando.CommandText = noQueryInsertEnTablaTemporal;
                    comando.ExecuteNonQuery();
                }
                String noQueryActualizarFuncionalidades = "exec MM.agregarFuncionalidadesRol " + rolModificado + "";
                comando.CommandText = noQueryActualizarFuncionalidades;
                comando.ExecuteNonQuery();

                if (comboBox1.SelectedIndex > -1)
                {
                    String noQuery = "exec MM.darDeBajaRol @rol='" + rolModificado + "'";
                    ConexionALaBase.Conexion.ejecutarNonQuery(noQuery);
                }

                if (textBox1.Text.Length != 0)  //SE HACE ULTIMO PORQUE CAMBIAR EL NOMBRE DEL ROL ANTES HACE QUE LOS NOQUERY DE ARRIBA SE HAGAN MAL
                {
                    String noQueryCambiarNombreRol = "update MM.Roles set Descripcion='" + textBox1.Text + "' where Descripcion='" + rolModificado + "'";
                    ConexionALaBase.Conexion.ejecutarNonQuery(noQueryCambiarNombreRol);
                }

                if (Program.rol.Equals(rolModificado) && (comboBox1.SelectedIndex > -1) && comboBox1.SelectedItem.ToString().Equals("Deshabilitado"))
                {
                    MessageBox.Show("Se han guardado los cambios. El rol desactivado es el que estaba utilizandose, seleccione un nuevo Rol");
                    new elegirRol().Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Se han guardado los cambios");
                    new Funcionalidades.Funcionalidades().Show();
                    this.Close();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

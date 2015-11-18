using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Abm_Rol
{
    public partial class buscarRol : Form
    {
        string rol;

        public buscarRol()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//nuevo
        {
            new crearRol().Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Validaciones.Validaciones.validarListBox(listBox1,"Seleccione un rol"))
            {
                rol = listBox1.SelectedItem.ToString();
                new modificarRol(rol).Show();
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)//eliminar
        {
            if (Validaciones.Validaciones.validarListBox(listBox1, "Seleccione un rol"))
            {
                rol = listBox1.SelectedItem.ToString();
                String noQuery = "exec MM.darDeBajaRol @rol='" + rol + "'";
                ConexionALaBase.Conexion.ejecutarNonQuery(noQuery);
                MessageBox.Show("Se ha deshabilitado el rol " + rol + "");
                if (Program.rol.Equals(rol))
                {
                    MessageBox.Show("El rol desactivado es el que estaba utilizandose, seleccione un nuevo Rol");
                    new elegirRol().Show();
                    this.Close();
                }
                else
                {
                    new buscarRol().Show();
                    this.Close();
                }
            }
        }

        private void buscarRol_Load(object sender, EventArgs e)
        {
            ConexionALaBase.CargadorDeEstructuras.cargarListBox(listBox1, ConexionALaBase.Conexion.consultarBase("select Descripcion from MM.Roles"));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Funcionalidades.Funcionalidades().Show();
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

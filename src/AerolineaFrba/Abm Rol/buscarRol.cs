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
            }else{
                MessageBox.Show("Para modificar un rol debe seleccionar primero uno de la lista");
            }
        }

        private void button3_Click(object sender, EventArgs e)//eliminar
        {
            if (Validaciones.Validaciones.validarListBox(listBox1, "Seleccione un rol"))
            {
                rol = listBox1.SelectedItem.ToString();
                //Aca hay que hacer lo que corresponda para eliminarlo de la base y que asigne true a success en caso de tener exito y false en caso contrario
                bool success = true;    // esta asignacion va a borrarse despues, es solo para probar
                if (success)
                {
                    MessageBox.Show("Se ha eliminado el rol " + rol);
                }
                else
                {
                    MessageBox.Show("No se ha podido eliminar el rol " + rol);
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

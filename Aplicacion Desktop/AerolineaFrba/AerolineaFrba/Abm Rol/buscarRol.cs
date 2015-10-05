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

        private void button1_Click(object sender, EventArgs e)
        {
            new crearRol().Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                rol = listBox1.SelectedItem.ToString();
                new modificarRol(rol).Show();
            }else{
                MessageBox.Show("Para modificar un rol debe seleccionar primero uno de la lista");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null){
                rol = listBox1.SelectedItem.ToString();
                //Aca hay que hacer lo que corresponda para eliminarlo de la base y que asigne true a success en caso de tener exito y false en caso contrario
                bool success = true;    // esta asignacion va a borrarse despues, es solo para probar
                if (success){
                    MessageBox.Show("Se ha eliminado el rol "+ rol);
                }
                    MessageBox.Show("No se ha podido eliminar el rol "+ rol);
            }
            else
            {
                MessageBox.Show("Para eliminar un rol debe seleccionar primero uno de la lista");
            }

        }

        private void buscarRol_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}

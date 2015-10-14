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
    public partial class ABM_RUTA : Form
    {
        public ABM_RUTA()
        {
            InitializeComponent();
            System.Data.SqlClient.SqlDataReader reader = ConexionALaBase.Conexion.consultarBase("Select descripcion from ciudades");
            ConexionALaBase.CargadorDeEstructuras.cargarComboBox(comboBox1, reader);
            reader.Dispose();
            reader = ConexionALaBase.Conexion.consultarBase("Select descripcion from ciudades");
            ConexionALaBase.CargadorDeEstructuras.cargarComboBox(comboBox2, reader);
            reader.Dispose();
            reader = ConexionALaBase.Conexion.consultarBase("Select descripcion from Tipos_Servicio");
            ConexionALaBase.CargadorDeEstructuras.cargarComboBox(comboBox2, reader);
            reader.Dispose();                               
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Funcionalidades.Funcionalidades(Program.rol).Show();
            this.Close();
            return;
        }
    }
}

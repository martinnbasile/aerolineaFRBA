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
using AerolineaFrba.ConexionALaBase;

namespace AerolineaFrba.Abm_Aeronave
{
    public partial class bajaFueraDeServicio : Form
    {
        Aeronave aeronaveAfectada;
        String fechaReinicioDeServicio;
        public bajaFueraDeServicio(Aeronave unaAeronave)
        {
            aeronaveAfectada=unaAeronave;
            InitializeComponent();
        }

        private void bajaFueraDeServicio_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
        }

        public void recibirFecha(DateTime unaFecha)
        {
            fechaReinicioDeServicio = unaFecha.ToString("yyyy-MM-dd");
            textBox1.Text = fechaReinicioDeServicio;
        }

   
        private void button3_Click(object sender, EventArgs e)
        {
            new calendario(this).Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Validaciones.Validaciones.validarTextBox(textBox1,"Seleccione una fecha"))
            {
                String fechaActual = DateTime.Now.ToString("yyyy-MM-dd");
                String queryConsulta = "SELECT * FROM Viajes WHERE Fecha_salida BETWEEN '" + fechaActual + "'  AND '" + fechaReinicioDeServicio + "' AND Matricula='" + aeronaveAfectada.getMatricula() + "' ";
                SqlDataReader consulta = ConexionALaBase.Conexion.consultarBase(queryConsulta);
                if (consulta.HasRows)
                {
                    
                    aeronaveAfectada.setFechaBajaFueraServicio(fechaActual);
                    aeronaveAfectada.setFechaAltaFueraServicio(fechaReinicioDeServicio);
                    new cancelarOReemplazarFueraDeServicio(aeronaveAfectada).Show();
                    this.Close();
                }
                else
                {
                    string queryParaMandarFueraDeServicio = "UPDATE Aeronaves set Baja_Fuera_Servicio='SI',Fecha_Fuera_Servicio='" + fechaActual + "',Fecha_Reinicio_Servicio='" + fechaReinicioDeServicio + "' where matricula='" + aeronaveAfectada.getMatricula() + "'";
                    ConexionALaBase.Conexion.ejecutarNonQuery(queryParaMandarFueraDeServicio);
                    MessageBox.Show("Se ha pasado la aeronave a estado fuera de servicio entre la fecha "+fechaActual+" y la fecha "+fechaReinicioDeServicio+" ");
                    new buscarAeronave().Show();
                    this.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new buscarAeronave().Show();
            this.Close();
        }
    }
}

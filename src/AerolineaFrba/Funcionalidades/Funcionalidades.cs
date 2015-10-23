using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AerolineaFrba.Abm_Rol;
using AerolineaFrba.ConexionALaBase;
using System.Data.SqlClient;
using AerolineaFrba.Abm_Ciudad;
namespace AerolineaFrba.Funcionalidades
{
    public partial class Funcionalidades : Form
    {
        public Funcionalidades()
        {
            String rolElegido = Program.rol;
            InitializeComponent();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            //TODO cambiar el select cuando se cree e inicialice la tabla Rol_Funcionalidades
            //SqlDataReader reader = ConexionALaBase.Conexion.consultarBase("select Descripcion from funcionalidadPorRol where Rol='"+rolElegido+"'");
            SqlDataReader reader = ConexionALaBase.Conexion.consultarBase("select Descripcion from funcionalidades");
            ConexionALaBase.CargadorDeEstructuras.cargarComboBox(comboBox1, reader);
            
        }

        private void Funcionalidades_Load_1(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validaciones.Validaciones.validarComboBox(comboBox1, "Seleccione una funcionalidad"))
            {
                String abmSeleccionada = comboBox1.Text;
                switch (abmSeleccionada)
                {
                    case "ABM ROL":
                        new buscarRol().Show();
                        this.Close();
                        break;
                    case "CANJEAR MILLAS":
                        MessageBox.Show("Todavia no implementado");
                        break;
                    case "COMPRAS":
                        MessageBox.Show("Todavia no implementado");
                        break;
                    case "DEVOLUCIONES Y CANCELACIONES":
                        MessageBox.Show("Todavia no implementado");
                        break;
                    case "GENERAR VIAJE":
                        MessageBox.Show("Todavia no implementado");
                        break;
                    case "LISTADO ESTADISTICO":
                        MessageBox.Show("Todavia no implementado");
                        break;
                    case "REGISTRAR LLEGADA A DESTINO":
                        MessageBox.Show("Todavia no implementado");
                        break;
                    case "CONSULTAR MILLAS":
                        new Canje_Millas.ConsultaMillas().Show();
                        this.Close();
                        break;
                    case "ABM CIUDADES":
                        new buscarCiudad().Show();
                        this.Close();
                        break;
                    case "ABM RUTAS":
                        new Abm_Ruta.ABM_RUTA().Show();
                        this.Close();
                        break;
                    case "ABM AERONAVES":
                        new Abm_Aeronave.buscarAeronave().Show();
                        this.Close();
                        break;
                    default:
                        MessageBox.Show("Funcionalidad aun no disponible");
                        break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Program.rol == "Administrador")
            {
                new elegirRol().Show();
                this.Close();
            }
            else MessageBox.Show("Opción invalida");
        }

       
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

     
    }
}

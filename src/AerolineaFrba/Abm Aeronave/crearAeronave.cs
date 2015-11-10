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

namespace AerolineaFrba.Abm_Aeronave
{
    public partial class crearAeronave : Form
    {
        Aeronave aeronaveAReemplazar;
        public crearAeronave(Aeronave unaAeronave)
        {
            aeronaveAReemplazar = unaAeronave;
            InitializeComponent();
        }

        private void crearAeronave_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new cancelarOReemplazarVidaUtil(aeronaveAReemplazar).Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Aeronave nuevaAeronave = new Aeronave();
            String nuevaAeronaveMatricula = maskedTextBox3.Text;
            int nuevaAeronaveCantidadDeButacas = int.Parse(maskedTextBox1.Text);
            int nuevaAeronaveCantidadKgs = int.Parse(maskedTextBox2.Text);
            nuevaAeronave.setMatricula(nuevaAeronaveMatricula);
            nuevaAeronave.setCantidadButacas(nuevaAeronaveCantidadDeButacas);
            nuevaAeronave.setCantidadKgs(nuevaAeronaveCantidadKgs);
            nuevaAeronave.setModelo(aeronaveAReemplazar.getModelo());
            nuevaAeronave.setFabricante(aeronaveAReemplazar.getFabricante());
            nuevaAeronave.setTipoDeServicio(aeronaveAReemplazar.getTipoDeServicio());
            SqlDataReader consulta = ConexionALaBase.Conexion.consultarBase("select id from Tipos_Servicio where Descripcion='" + nuevaAeronave.getTipoDeServicio() + "'");
            int idTipoServicio = consulta.GetInt32(0);
            String queryValidarMatricula = "select * from Aeronaves where Matricula='" + nuevaAeronave.getMatricula() + "'";
            SqlDataReader consultaValidarMatricula = ConexionALaBase.Conexion.consultarBase(queryValidarMatricula);
            if (consultaValidarMatricula.HasRows)
            {
                MessageBox.Show("Ya existe una aeronave con la matrícula elegida, ingrese una matrícula diferente");
            }
            else
            {
                ConexionALaBase.Conexion.ejecutarNonQuery("INSERT INTO Aeronaves (matricula,Modelo,Fabricante,Tipo_Servicio,Cantidad_Butacas,Cantidad_Kg) VALUES ('" + nuevaAeronave.getMatricula() + "','" + nuevaAeronave.getModelo() + "','" + nuevaAeronave.getFabricante() + "'," + idTipoServicio + "," + nuevaAeronave.getCantidadButacas() + "," + nuevaAeronave.getCantidadKgs() + ")");
                String noQueryActualizarViajes = "update viajes set viajes.Matricula='" + nuevaAeronave.getMatricula() + "' where viajes.Matricula='" + aeronaveAReemplazar.getMatricula() + "' and (viajes.Fecha_salida between '" + aeronaveAReemplazar.getFechaBajaFueraServicio() + "' and '" + aeronaveAReemplazar.getFechaAltaFueraServicio() + "' or  viajes.Fecha_Estimada_llegada between '" + aeronaveAReemplazar.getFechaBajaFueraServicio() + "' and '" + aeronaveAReemplazar.getFechaAltaFueraServicio() + "')";
                ConexionALaBase.Conexion.ejecutarNonQuery(noQueryActualizarViajes);
                MessageBox.Show("La aeronave a sido dada de baja y se ha asignado la aeronave creada para que la reemplace en los vuelos correspondientes");
                new buscarAeronave();
                this.Close();
            }


            
            
        }

        private void maskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}

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
            if (estaCompleto())
            {

                Aeronave nuevaAeronave = new Aeronave();
                String nuevaAeronaveMatricula = maskedTextBox2.Text;
                int nuevaAeronaveCantidadDeButacas = Convert.ToInt32(numericUpDown1.Value);
                int nuevaAeronaveCantidadKgs = Convert.ToInt32(numericUpDown2.Value);
                nuevaAeronave.setMatricula(nuevaAeronaveMatricula);
                nuevaAeronave.setCantidadButacas(nuevaAeronaveCantidadDeButacas);
                nuevaAeronave.setCantidadKgs(nuevaAeronaveCantidadKgs);
                nuevaAeronave.setModelo(aeronaveAReemplazar.getModelo());
                nuevaAeronave.setFabricante(aeronaveAReemplazar.getFabricante());
                nuevaAeronave.setTipoDeServicio(aeronaveAReemplazar.getTipoDeServicio());
                SqlDataReader consulta = ConexionALaBase.Conexion.consultarBase("select id from MM.Tipos_Servicio where Descripcion='" + nuevaAeronave.getTipoDeServicio() + "'");
                int idTipoServicio = new int();
                if (consulta.Read()) { idTipoServicio = consulta.GetInt32(consulta.GetOrdinal("id")); }
                consulta = ConexionALaBase.Conexion.consultarBase("select id from MM.Fabricantes where Descripcion='" + nuevaAeronave.getFabricante() + "'");
                int idFabricante = new int();
                if (consulta.Read()) { idFabricante = consulta.GetInt32(consulta.GetOrdinal("id")); }
                
                String queryValidarMatricula = "select * from MM.Aeronaves where Matricula='" + nuevaAeronave.getMatricula() + "'";
                SqlDataReader consultaValidarMatricula = ConexionALaBase.Conexion.consultarBase(queryValidarMatricula);
                if (consultaValidarMatricula.HasRows)
                {
                    MessageBox.Show("Ya existe una aeronave con la matrícula elegida, ingrese una matrícula diferente");
                }
                else
                {
                    String fechaActual = DateTime.Now.ToString("yyyy-MM-dd");
                    ConexionALaBase.Conexion.ejecutarNonQuery("INSERT INTO MM.Aeronaves (matricula,Fecha_alta,Modelo,Fabricante,Tipo_Servicio,Cantidad_Butacas,Cantidad_Kg) VALUES ('" + nuevaAeronave.getMatricula() + "','"+fechaActual+"','" + nuevaAeronave.getModelo() + "','" + idFabricante + "'," + idTipoServicio + "," + nuevaAeronave.getCantidadButacas() + "," + nuevaAeronave.getCantidadKgs() + ")");
                    String noQueryActualizarViajes = "update MM.viajes set MM.viajes.Matricula='" + nuevaAeronave.getMatricula() + "' where MM.viajes.Matricula='" + aeronaveAReemplazar.getMatricula() + "' and (MM.viajes.Fecha_salida between '" + aeronaveAReemplazar.getFechaBajaFueraServicio() + "' and '" + aeronaveAReemplazar.getFechaAltaFueraServicio() + "' or  MM.viajes.Fecha_Estimada_llegada between '" + aeronaveAReemplazar.getFechaBajaFueraServicio() + "' and '" + aeronaveAReemplazar.getFechaAltaFueraServicio() + "')";
                    ConexionALaBase.Conexion.ejecutarNonQuery(noQueryActualizarViajes);
                    MessageBox.Show("La aeronave a sido dada de baja y se ha asignado la aeronave creada para que la reemplace en los vuelos correspondientes");
                    new buscarAeronave().Show();
                    this.Close();
                }
            }
        }

        private bool estaCompleto()
        {
            if (Validaciones.Validaciones.validarNumericUpDown(numericUpDown1, "Completar la cantidad de butacas con un valor mayor a cero"))
            {
                if (Validaciones.Validaciones.validarNumericUpDown(numericUpDown2, "Completar la cantidad de kilogramos con un valor mayor a cero"))
                {
                    if (Validaciones.Validaciones.validarMaskedTextBox(maskedTextBox2, "Completar la matrícula"))
                    {
                        return true;
                    }
                }
            }  
            return false;
        }

        private void maskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
          
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}

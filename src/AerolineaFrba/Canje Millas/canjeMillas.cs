using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Canje_Millas
{
    public partial class canjeMillas : Form
    {
        int dni;
        int millasDisponibles;
        int numCliente;

        public canjeMillas(int unDni)
        {
            dni = unDni;
            InitializeComponent();
        }

        private void canjeMillas_Load(object sender, EventArgs e)
        {
            ConexionALaBase.CargadorDeEstructuras.cargarDataGrid(dataGridView1, "select Descripcion,Millas_Necesarias as 'Precio en millas',cantidad from MM.Productos_Milla");
            textBox1.Text = dni.ToString();
            System.Data.SqlClient.SqlDataReader reader = ConexionALaBase.Conexion.consultarBase("Select id from MM.clientes where DNI=" + dni);
            reader.Read();
            numCliente = (int)reader.GetSqlInt32(0);
            reader = ConexionALaBase.Conexion.consultarBase("select sum(millas) from MM.millas where cliente=" + numCliente);
            reader.Read();
            millasDisponibles = (int)reader.GetSqlInt32(0);
            textBox2.Text = millasDisponibles.ToString();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new ingresarDniCanjeMillas().Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Validaciones.Validaciones.validarDataGridView(dataGridView1, "Seleccione un producto"))
            {
                if (Validaciones.Validaciones.validarNumericUpDown(numericUpDown1, "Seleccione una cantidad de productos a canjear"))
                {
                    DataGridViewRow productoSeleccionado = this.dataGridView1.SelectedRows[0];
                    String descripcionProducto = productoSeleccionado.Cells["Descripcion"].Value.ToString();
                    int precioEnMillasPorUnidad = int.Parse(productoSeleccionado.Cells["Precio en millas"].Value.ToString());
                    int cantidadQueQuiereCanjear = Convert.ToInt32(numericUpDown1.Value);
                    if ((cantidadQueQuiereCanjear * precioEnMillasPorUnidad) > millasDisponibles)
                    {
                        MessageBox.Show("No tiene las millas suficientes para realizar ese canje");
                    }
                    else
                    {
                        try
                        {
                            String noQuery = "exec MM.registrarCanje @numCliente=" + numCliente + ",@cantidad=" + numericUpDown1.Value + ",@descripcion='" + descripcionProducto + "'";
                            ConexionALaBase.Conexion.ejecutarNonQuery(noQuery);
                            new ingresarDniCanjeMillas().Show();
                            this.Close();
                        }
                        catch (System.Data.SqlClient.SqlException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                }
            }
        }
    }
}
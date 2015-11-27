﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Compra
{
    public partial class viajesDisponibles : Form
    {
        String origenRecibido;
        String destinoRecibido;
        String fechaRecibida;

        LaCompra unaCompra = new LaCompra();
        int cantidadPasajes;

        public viajesDisponibles(String origen, String destino, String fecha)
        {
            InitializeComponent();
            origenRecibido = origen;
            destinoRecibido = destino;
            fechaRecibida = fecha;
            label1.Text = "Viajes disponibles:";
            label2.Text = "Cantidad pasajes";
            label3.Text = "Kilos de encomienda";
            button1.Text = "Siguiente";
            button2.Text = "Volver";
            
        }

        private void viajesDisponibles_Load(object sender, EventArgs e)
        {
           ConexionALaBase.CargadorDeEstructuras.cargarDataGrid(dataGridView1,"select * from mm.viajesDisponibles('"+fechaRecibida+"','"+origenRecibido+"','"+destinoRecibido+"')");
        }


        private bool validarTodo()
        {
            
            DataGridViewRow viajeSeleccionado = this.dataGridView1.SelectedRows[0];
            if (Validaciones.Validaciones.validarDataGridView(dataGridView1,"Elija una fila"))
            {
                if (numericUpDown2.Value != 0 || numericUpDown1.Value != 0)
                {
                    if (numericUpDown1.Value < int.Parse(viajeSeleccionado.Cells["butacasLibres"].Value.ToString()))
                    {
                        if (numericUpDown2.Value < int.Parse(viajeSeleccionado.Cells["kgLibres"].Value.ToString()))
                        {
                            return true;
                        }
                        else MessageBox.Show("No hay espacio suficiente para su encomienda en el viaje");
                    }
                    else MessageBox.Show("No hay suficientes butacas en el viaje");
                }
                else MessageBox.Show("Ingresa cantidad de pasajes o Kgs de encomienda");
              }
        return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.validarTodo())
            {
                
                unaCompra.cantidadPasajes = int.Parse(numericUpDown1.Value.ToString());
                unaCompra.cantidadKgs = int.Parse(numericUpDown2.Value.ToString());
                DataGridViewRow viajeSeleccionado = this.dataGridView1.SelectedRows[0];
                int.Parse(viajeSeleccionado.Cells["idViaje"].Value.ToString());

                MessageBox.Show("A continuación, ingrese los datos de los pasajeros");
                new DNI(unaCompra,"").Show();
                this.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)//VOLVER
        {
            ConexionALaBase.Conexion.ejecutarNonQuery("Rollback transaction compra");
            new compra().Show();
            this.Close();
        }

       
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            //String noQuery = "i
            
            
        }

        private void maskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}

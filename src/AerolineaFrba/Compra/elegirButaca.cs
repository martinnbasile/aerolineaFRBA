using System;
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
    public partial class elegirButaca : Form
    {
        LaCompra unaCompra;
        public elegirButaca(LaCompra compraRecibida)
        {
            InitializeComponent();
            unaCompra = compraRecibida;
        }

        private void elegirButaca_Load(object sender, EventArgs e)
        {
            ConexionALaBase.CargadorDeEstructuras.cargarDataGrid(dataGridView1,"Select * from  mm.butacasDisponibles("+unaCompra.idViaje+")");  
  
        }
    }
}

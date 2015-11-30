using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Abm_Ruta
{

    
    public class Ruta
    {
        int id;
        String origen;
        String destino;
        String servicio;
        int precioBase;
        int precioBaseEncomienda;
        public void cargate(DataGridViewRow unaFila){
            id=int.Parse(unaFila.Cells["Codigo"].Value.ToString());
            origen = unaFila.Cells["Ciudad origen"].Value.ToString();
            destino = unaFila.Cells["Ciudad destino"].Value.ToString();
            servicio = unaFila.Cells["Servicio"].Value.ToString();
            precioBase = (int)float.Parse(unaFila.Cells["Precio base"].Value.ToString());
            precioBaseEncomienda =(int)float.Parse(unaFila.Cells["Precio base encomienda"].Value.ToString());            
        }

        public string getOrigen()
        {
            return origen;
        }

        public string getDestino()
        {
            return destino;
        }

        public string getServicio()
        {
            return servicio;
        }

        public int getPrecioBase()
        {
            return precioBase;
        }
        public int getPrecioEncomienda()
        {
            return precioBaseEncomienda;
        }
    }
}

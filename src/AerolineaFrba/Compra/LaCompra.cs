using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AerolineaFrba.Compra
{
    public class LaCompra
    {
        public int idViaje;
        public int cantidadPasajes;
        public int cantidadKgs;
        public int dniCliente;
        private int pasajesProcesados = 0;


        public void seIngresoUnCliente(LaCompra unaCompra)
        {

            pasajesProcesados++;
            
            if (pasajesProcesados < cantidadPasajes)
            {
                new DNI(unaCompra).Show();
                
            }
            else
            {
                new elegirButaca(unaCompra).Show();
                
            }
        }
    }


}

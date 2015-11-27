using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AerolineaFrba.Compra
{
    public class LaCompra
    {
        public string instanciaDeCompra;
        public string origen;
        public string destino;
        public string fechaSalida;
        public int idViaje;
        public int codigoCompra;
        public int cantidadPasajes;
        public int cantidadKgs;
        public int dniCliente;
        private int pasajesProcesados = 0;
        private int sumaPrecio = 0;

        public void seProcesoUnPasaje(LaCompra unaCompra)
        {

            pasajesProcesados++;
            
            if (pasajesProcesados < cantidadPasajes)
            {
               // new DNI(unaCompra,"").Show();
                
            }
            else
            {
               // new elegirButaca(unaCompra, unPasajero).Show();
                
            }
        }
    }


}

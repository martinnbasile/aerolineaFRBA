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
        private int sumaPrecio;

         public LaCompra()
         {
            System.Data.SqlClient.SqlDataReader reader = ConexionALaBase.Conexion.consultarBase(
                "select precio_kg from mm.viajes V JOIN mm.rutas_aereas r on v.ruta=r.id"+
                "where v.id="+idViaje);
            int precioBaseEncomienda = reader.GetInt32(0);
            reader.Dispose();
            reader = ConexionALaBase.Conexion.consultarBase(
                "select precio_base from mm.viajes V JOIN mm.rutas_aereas r on v.ruta=r.id"+
                            "where v.id=" + idViaje);
            int precioPasaje = reader.GetInt32(0);
            sumaPrecio = precioBaseEncomienda * cantidadKgs+precioPasaje*cantidadPasajes;
         }
        
        
        public void seProcesoUnPasaje()
        {
            pasajesProcesados++;
            if (pasajesProcesados < cantidadPasajes)
            { //Todavia quedan pasajes por procesar
                new DNI(this).Show();
            }
            else
            { //YA HAY QUE PAGAR
                new seleccionarMedioPago(this).Show();
            }
        }
    }


}

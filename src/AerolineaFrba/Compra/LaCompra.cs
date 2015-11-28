using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AerolineaFrba.Compra
{
    public class LaCompra
    {
        public SqlCommand comandoT;
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
        private float precioTotal;

         public LaCompra()
         {
            
         }

         public void viajeElegido(int viajeElegido)
         {
             idViaje = viajeElegido;
             System.Data.SqlClient.SqlDataReader reader = ConexionALaBase.Conexion.consultarBase(
                "select precio_kg from mm.viajes v JOIN mm.rutas_aereas r on v.ruta=r.id where v.id=" + idViaje);
             reader.Read();
             float precioBaseEncomienda = float.Parse(reader.GetSqlValue(0).ToString());
             reader.Dispose();
             reader = ConexionALaBase.Conexion.consultarBase(
                 "select precio_base from mm.viajes v JOIN mm.rutas_aereas r on v.ruta=r.id where v.id=" + idViaje);
             reader.Read();
             float precioPasaje = float.Parse(reader.GetSqlValue(0).ToString());
             precioTotal = precioBaseEncomienda * cantidadKgs + precioPasaje * cantidadPasajes;
         }

         public float totalCompra()
         {
             return precioTotal;
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

        public void iniciarProcesamiento()
        {
            if (pasajesProcesados < cantidadPasajes)
            {
                new DNI(this).Show();
            }
            else //YA HAY QUE PAGAR 
            {
                new seleccionarMedioPago(this).Show();
            }

        }
    }


}

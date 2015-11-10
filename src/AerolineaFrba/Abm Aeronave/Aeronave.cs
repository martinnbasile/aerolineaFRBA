using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AerolineaFrba.Abm_Aeronave
{

    public class Aeronave
    {
        String matricula;
        String modelo;
        String fabricante;
        String tipoDeServicio;
        String fechaBajaFueraServicio;
        String fechaAltaFueraServicio;
        int cantidadButacas;
        int cantidadKgs;

        public string getMatricula()
        {
            return matricula;
        }
        public string getModelo()
        {
            return modelo;
        }
        public string getFabricante()
        {
            return fabricante;
        }
        public string getTipoDeServicio()
        {
            return tipoDeServicio;
        }
        public string getFechaBajaFueraServicio()
        {
            return fechaBajaFueraServicio;
        }
        public string getFechaAltaFueraServicio()
        {
            return fechaAltaFueraServicio;
        }
        public int getCantidadButacas()
        {
            return cantidadButacas;
        }
        public int getCantidadKgs()
        {
            return cantidadKgs;
        }
        public void setMatricula(String unaMatricula)
        {
             matricula=unaMatricula;
        }
        public void setModelo(String unModelo)
        {
            modelo=unModelo;
        }
        public void setFabricante(String unFabricante)
        {
            fabricante=unFabricante;
        }
        public void setTipoDeServicio(String unTipoDeServicio)
        {
            tipoDeServicio = unTipoDeServicio;
        }
        public void setFechaBajaFueraServicio(String unaFechaDeBaja)
        {
           fechaBajaFueraServicio= unaFechaDeBaja;
        }
        public void setFechaAltaFueraServicio(String unaFechaDeAlta)
        {
            fechaAltaFueraServicio=unaFechaDeAlta;
        }
        public void setCantidadButacas(int unaCantidad)
        {
            cantidadButacas=unaCantidad;
        }
        public void setCantidadKgs(int unaCantidad)
        {
            cantidadKgs = unaCantidad;
        }


    }
}

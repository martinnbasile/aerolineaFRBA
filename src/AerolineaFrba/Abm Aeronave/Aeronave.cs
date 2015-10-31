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
        String fechaBajaFueraServicio;
        String fechaAltaFueraServicio;

        public string getMatricula()
        {
            return matricula;
        }
        public string getFechaBajaFueraServicio()
        {
            return fechaBajaFueraServicio;
        }
        public string getFechaAltaFueraServicio()
        {
            return fechaAltaFueraServicio;
        }
        public void setMatricula(String unaMatricula)
        {
             matricula=unaMatricula;
        }
        public void setFechaBajaFueraServicio(String unaFechaDeBaja)
        {
           fechaBajaFueraServicio= unaFechaDeBaja;
        }
        public void setFechaAltaFueraServicio(String unaFechaDeAlta)
        {
            fechaAltaFueraServicio=unaFechaDeAlta;
        }


    }
}
